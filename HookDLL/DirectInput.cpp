#include "stdafx.h"
#include "DirectInput.h"

#include <dinput.h>

#include <sstream>
#include <iomanip>

LPDIRECTINPUT8 DirectInput::directInput;
std::map<std::string, LPDIRECTINPUTDEVICE8> DirectInput::devices;

BOOL CALLBACK DirectInput::EnumJoysticksCallback(const DIDEVICEINSTANCE* instance, VOID* context)
{
	LPDIRECTINPUTDEVICE8 device;
	HRESULT res = directInput->CreateDevice(instance->guidInstance, &device, NULL);
	
	if (!FAILED(res))
	{
		std::stringstream guidString;

		guidString << std::hex << std::setfill('0') << std::setw(8) << instance->guidProduct.Data1 << "-";
		guidString << std::setw(4) << instance->guidProduct.Data2 << "-";
		guidString << std::setw(4) << instance->guidProduct.Data3 << "-";

		for(int i = 0; i < 8; i++)
			guidString << std::setw(2) << +instance->guidProduct.Data4[i];
			
		std::wcout << L"Found device: \"" << instance->tszProductName;
		std::cout << "\". GUID: " << guidString.str() << std::endl;

		return DIENUM_CONTINUE;
	}

	return DIENUM_STOP;
}

void DirectInput::RefreshDeviceList()
{
	directInput->EnumDevices(DI8DEVCLASS_GAMECTRL, EnumJoysticksCallback, nullptr, DIEDFL_ATTACHEDONLY);
}

LPDIRECTINPUTDEVICE8 DirectInput::FindDevice(std::string guid)
{
	const auto& device = devices.find(guid);

	if (device == devices.end())
		return nullptr;
	else
		return device->second;
}

bool DirectInput::Init()
{
	HRESULT res = DirectInput8Create(GetModuleHandle(NULL), 0x0800, IID_IDirectInput8, (LPVOID*)&directInput, nullptr);

	if (FAILED(res))
		return false;

	RefreshDeviceList();
	return true;
}


