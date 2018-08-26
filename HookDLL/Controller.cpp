#include "stdafx.h"
#include "Controller.h"

#include "DirectInput.h"

#define CLAMP(x, upper, lower) (min(upper, max(x, lower)))

std::vector<int> Controller::buttonFlags =
{
	XINPUT_GAMEPAD_DPAD_UP,
	XINPUT_GAMEPAD_DPAD_DOWN,
	XINPUT_GAMEPAD_DPAD_LEFT,
	XINPUT_GAMEPAD_DPAD_RIGHT,
	XINPUT_GAMEPAD_START,
	XINPUT_GAMEPAD_BACK,
	XINPUT_GAMEPAD_LEFT_THUMB,
	XINPUT_GAMEPAD_RIGHT_THUMB,
	XINPUT_GAMEPAD_LEFT_SHOULDER,
	XINPUT_GAMEPAD_RIGHT_SHOULDER,
	XINPUT_GAMEPAD_A,
	XINPUT_GAMEPAD_B,
	XINPUT_GAMEPAD_X,
	XINPUT_GAMEPAD_Y
};

Controller::Controller() : type(DEVICE_TYPE::NONE), productGUID(""), instanceGUID("") {}

void Controller::AssignDIDevice(const std::string& instanceGUID, const std::string& productGUID, DEVICE_TYPE type, const std::vector<uint16_t>& mappings)
{
	if (type == DEVICE_TYPE::NONE)
	{
		std::cout << "Controller unassigned." << std::endl;
		return;
	}

	if (type == DEVICE_TYPE::GAMEPAD)
	{
		std::cout << "Trying to find device with GUIDS " << instanceGUID << ", " << productGUID << std::endl;
		DIDevice = DirectInput::FindDevice(instanceGUID, productGUID);

		if (!DIDevice)
		{
			std::cout << "Couldn't assign device. Device GUIDs not found." << std::endl;
			return;
		}

		if (!DirectInput::InitializeDevice(DIDevice))
		{
			std::cout << "Failed to initialize device." << std::endl;
			return;
		}
		
		std::cout << "Device found." << std::endl;
		DIDevice->Unacquire();
	}

	this->instanceGUID = instanceGUID;
	this->productGUID = productGUID;
	this->type = type;

	for (int i = 0; i < mappings.size(); i++)
	{
		if (type == DEVICE_TYPE::GAMEPAD)
		{
			short mappingType = mappings[i] & 3;

			switch (mappingType)
			{
				case DIBinding::BINDING_TYPE::BUTTON_BINDING:
				{
					int index = (mappings[i] & 0x3C) >> 2;
					bindings[i] = std::unique_ptr<DIBinding>(new DIButtonBinding(index));
					std::cout << "BUTTON: index=" << index << std::endl;
				}
				break;

				case DIBinding::BINDING_TYPE::POV_BINDING:
				{
					int index = (mappings[i] & 0x3C) >> 2;
					int dir = (mappings[i] & 0xFFC0) >> 6;

					std::cout << "POV: index=" << index << ", dir=" << dir << std::endl;

					bindings[i] = std::unique_ptr<DIBinding>(new DIPovBinding(index, (DIPovBinding::POV_DIRECTION)dir));
				}
				break;

				case DIBinding::BINDING_TYPE::AXIS_BINDING:
				{
					int index = (mappings[i] & 0x3C) >> 2;
					bool isNegative = (mappings[i] & 0x40) >> 6;

					std::cout << "AXIS: index=" << index << ", negative=" << std::boolalpha << isNegative << std::endl;
					bindings[i] = std::unique_ptr<DIBinding>(new DIAxisBinding(index, isNegative));
				}
			}
		}
		else if(type == DEVICE_TYPE::KEYBOARD)
		{
			std::wcout << "Key binding: " << mappings[i] << std::endl;
			bindings[i] = std::unique_ptr<DIBinding>(new DIButtonBinding(mappings[i], true));
		}
	}
}

bool Controller::IsConnected()
{
	return type != DEVICE_TYPE::NONE;
}

HRESULT Controller::Acquire()
{
	if (type == DEVICE_TYPE::KEYBOARD)
		return true;

	if (!DIDevice)
		return false;

	return DIDevice->Acquire();
}

HRESULT Controller::Unacquire()
{
	if (type == DEVICE_TYPE::KEYBOARD)
		return true;

	if (!DIDevice)
		return false;

	return DIDevice->Unacquire();
}

XINPUT_GAMEPAD Controller::GetState()
{
	XINPUT_GAMEPAD XInputState = { 0 };
	DIJOYSTATE2 joystate = { 0 };

	if (type == DEVICE_TYPE::GAMEPAD)
	{
		HRESULT res = DIDevice->GetDeviceState(sizeof(joystate), &joystate);

		if (res == DIERR_INPUTLOST)
		{
			if (FAILED(DIDevice->Acquire()))
			{
				std::cout << "Failed to reacquire, returning last state instead." << std::endl;
				return lastState;
			}

			if(FAILED(DIDevice->GetDeviceState(sizeof(joystate), &joystate)))
			{
				std::cout << "Reacquired but failed to get state again. Returning last state." << std::endl;
				return lastState;
			}
		}
		else if(FAILED(res))
		{
			std::cout << "Failed to get state. HRESULT:" << std::hex << res << "(" << std::dec << res << ")" << std::endl;
			return lastState;
		}
	}

	for (auto& binding : bindings)
		if(binding)
			binding->Update(joystate);

	ANALOG_SUBVALUES subValues[2];

	for (int i = 0; i < bindings.size(); i++)
	{
		if (!bindings[i])
			continue;
	
		//XBOX Buttons
		if (i < 14)
		{
			switch (bindings[i]->type)
			{
				case DIBinding::POV_BINDING:
				case DIBinding::BUTTON_BINDING:
				{
					DIButtonBinding* button = dynamic_cast<DIButtonBinding*>(bindings[i].get());
					DIPovBinding* pov = dynamic_cast<DIPovBinding*>(bindings[i].get());

					if ((pov && pov->GetState()) || (button && button->GetState()))
						XInputState.wButtons |= buttonFlags[i];
				}
				break;
			}
		}
		//Triggers
		else if (i >= 14 && i < 16)
		{
			switch (bindings[i]->type)
			{
				case DIBinding::POV_BINDING:
				case DIBinding::BUTTON_BINDING:
				{
					DIButtonBinding* button = dynamic_cast<DIButtonBinding*>(bindings[i].get());
					DIPovBinding* pov = dynamic_cast<DIPovBinding*>(bindings[i].get());

					if ((pov && pov->GetState()) || (button && button->GetState()))
					{
						if (i == 14)
							XInputState.bLeftTrigger = 255;
						else
							XInputState.bRightTrigger = 255;
					}
				}
				break;
			}
		}
		//Analogs
		else
		{
			int direction = (i - 16) % 4;
			int analogIndex = (i - 16) / 4;

			switch (bindings[i]->type)
			{
				case DIBinding::BUTTON_BINDING:
				{
					DIButtonBinding* button = dynamic_cast<DIButtonBinding*>(bindings[i].get());

					if (direction == 0)
						subValues[analogIndex].positiveY = button->GetState() ? 32768 : 0;
					else if (direction == 1)
						subValues[analogIndex].negativeY = button->GetState() ? -32768 : 0;
					else if (direction == 2)
						subValues[analogIndex].negativeX = button->GetState() ? -32768 : 0;
					else if (direction == 3)
						subValues[analogIndex].positiveX = button->GetState() ? 32768 : 0;

				}
				break;

				case DIBinding::AXIS_BINDING:
				{
					DIAxisBinding* axis = dynamic_cast<DIAxisBinding*>(bindings[i].get());

					if (direction == 0)
						subValues[analogIndex].positiveY = axis->IsNegative() ? axis->GetValue() : axis->GetValue();
					else if(direction == 1)
						subValues[analogIndex].negativeY = axis->IsNegative() ? axis->GetValue() : -axis->GetValue();
					else if (direction == 2)
						subValues[analogIndex].negativeX = axis->IsNegative() ? axis->GetValue() : -axis->GetValue();
					else if (direction == 3)
						subValues[analogIndex].positiveX = axis->IsNegative() ? -axis->GetValue() : axis->GetValue();
				}
				break;
			}
		}
	}

	int LXValue = subValues[0].negativeX != 0 ? subValues[0].negativeX : subValues[0].positiveX;
	int LYValue = subValues[0].negativeY != 0 ? subValues[0].negativeY : subValues[0].positiveY;
	int RXValue = subValues[1].negativeX != 0 ? subValues[1].negativeX : subValues[1].positiveX;
	int RYValue = subValues[1].negativeY != 0 ? subValues[1].negativeY : subValues[1].positiveY;

	XInputState.sThumbLX = std::max<int>(INT16_MIN, std::min<int>(LXValue, INT16_MAX));
	XInputState.sThumbLY = std::max<int>(INT16_MIN, std::min<int>(LYValue, INT16_MAX));
	XInputState.sThumbRX = std::max<int>(INT16_MIN, std::min<int>(RXValue, INT16_MAX));
	XInputState.sThumbRY = std::max<int>(INT16_MIN, std::min<int>(RYValue, INT16_MAX));

	lastState = XInputState;

	return XInputState;
}
