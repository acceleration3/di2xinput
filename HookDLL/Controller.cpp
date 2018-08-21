#include "stdafx.h"
#include "Controller.h"

#include "DirectInput.h"

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

Controller::Controller() : type(DEVICE_TYPE::NONE), deviceGuid("") {}

void Controller::AssignDIDevice(const std::string& guid, DEVICE_TYPE type, const std::vector<uint16_t>& mappings)
{
	if (type == DEVICE_TYPE::NONE)
	{
		std::cout << "Controller unassigned." << std::endl;
		return;
	}

	if (type == DEVICE_TYPE::GAMEPAD)
	{
		DIDevice = DirectInput::FindDevice(guid);

		if (!DIDevice)
		{
			std::cout << "Couldn't assign device. Device GUID " << guid << " not found." << std::endl;
			return;
		}

		if (!DirectInput::InitializeDevice(DIDevice))
		{
			std::cout << "Failed to initialize device." << std::endl;
			return;
		}
	}

	this->deviceGuid = guid;
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
			}
		}
		else if(type == DEVICE_TYPE::KEYBOARD)
		{
			bindings[i] = std::unique_ptr<DIBinding>(new DIButtonBinding(mappings[i], true));
		}
	}
}

bool Controller::IsConnected()
{
	return type != DEVICE_TYPE::NONE;
}

bool Controller::Acquire()
{
	if (type == DEVICE_TYPE::KEYBOARD)
		return true;

	if (!DIDevice)
		return false;

	return SUCCEEDED(DIDevice->Acquire());
}

bool Controller::Unacquire()
{
	if (type == DEVICE_TYPE::KEYBOARD)
		return true;

	if (!DIDevice)
		return false;

	return SUCCEEDED(DIDevice->Unacquire());
}

XINPUT_GAMEPAD Controller::GetState()
{
	XINPUT_GAMEPAD state = { 0 };
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

	//XBOX Buttons conversion
	for (int i = 0; i < buttonFlags.size(); i++)
	{
		if (!bindings[i])
			continue;
	
		switch(bindings[i]->type)
		{
			case DIBinding::POV_BINDING:
			case DIBinding::BUTTON_BINDING:
			{
				DIButtonBinding* button = dynamic_cast<DIButtonBinding*>(bindings[i].get());
				DIPovBinding* pov = dynamic_cast<DIPovBinding*>(bindings[i].get());

				if (pov && pov->GetState())
					state.wButtons |= buttonFlags[i];
				else if (button && button->GetState())
					state.wButtons |= buttonFlags[i];
			}
			break;
		}
	}

	lastState = state;

	return state;
}
