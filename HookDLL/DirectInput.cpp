#include "stdafx.h"
#include "DirectInput.h"

#include <dinput.h>

#include <sstream>
#include <iomanip>

LPDIRECTINPUT8 DirectInput::directInput;
std::map<std::string, LPDIRECTINPUTDEVICE8> DirectInput::devices;
HWND DirectInput::dummyWindow;

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

		for (int i = 0; i < 2; i++)
			guidString << std::setw(2) << +instance->guidProduct.Data4[i];

		guidString << "-";

		for(int i = 2; i < 8; i++)
			guidString << std::setw(2) << +instance->guidProduct.Data4[i];
			
		std::wcout << L"Found device: \"" << instance->tszProductName;
		std::cout << "\". GUID: " << guidString.str() << std::endl;

		devices.emplace(guidString.str(), device);

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
	const auto device = devices.find(guid);

	if (device != devices.end())
		return device->second;
	else
		return nullptr;
}

bool DirectInput::InitializeDevice(LPDIRECTINPUTDEVICE8 device)
{
	HRESULT res = device->SetDataFormat(&c_dfDIJoystick2);

	if (FAILED(res))
	{
		std::cout << "Failed to set data format. Error: 0x" << std::hex << res << std::endl;
		return false;
	}

	res = device->SetCooperativeLevel(dummyWindow, DISCL_BACKGROUND | DISCL_NONEXCLUSIVE);

	if (FAILED(res))
	{
		std::cout << "Failed to set cooperative level. Error: 0x" << std::hex << res << std::endl;
		return false;
	}

	return true;
}

bool DirectInput::Init()
{
	HRESULT res = DirectInput8Create(GetModuleHandle(NULL), 0x0800, IID_IDirectInput8, (LPVOID*)&directInput, nullptr);

	if (FAILED(res))
	{
		std::cout << "Failed to initialize DirectInput8." << std::endl;
		return false;
	}
		
	dummyWindow = CreateWindowEx(NULL, TEXT("Message"), TEXT("di2xinput"), WS_TILED, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND_MESSAGE, NULL, GetModuleHandle(NULL), NULL);

	if (!dummyWindow)
	{
		std::cout << "Failed to create a dummy window." << std::endl;
		return false;
	}

	RefreshDeviceList();
	return true;
}


