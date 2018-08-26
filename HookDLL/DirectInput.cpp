#include "stdafx.h"
#include "DirectInput.h"

#include <dinput.h>

#include <sstream>
#include <iomanip>

LPDIRECTINPUT8 DirectInput::directInput;
std::vector<std::tuple<std::string, std::string, LPDIRECTINPUTDEVICE8>> DirectInput::devices;
HWND DirectInput::dummyWindow;
LPDIRECTINPUTDEVICE8 DirectInput::currentDevice;

BOOL CALLBACK DirectInput::EnumObjectsCallback(const DIDEVICEOBJECTINSTANCE* pdidoi, VOID* pContext)
{
	DIPROPRANGE propRange;
	propRange.diph.dwSize = sizeof(DIPROPRANGE);
	propRange.diph.dwHeaderSize = sizeof(DIPROPHEADER);
	propRange.diph.dwHow = DIPH_BYID;
	propRange.diph.dwObj = pdidoi->dwType;
	propRange.lMin = -32768;
	propRange.lMax = +32768;

	if (FAILED(currentDevice->SetProperty(DIPROP_RANGE, &propRange.diph)))
	{
		std::cout << "Failed to set axis " << pdidoi->wDesignatorIndex << " range." << std::endl;
		return DIENUM_STOP;
	}

	return DIENUM_CONTINUE;
}

BOOL CALLBACK DirectInput::EnumJoysticksCallback(const DIDEVICEINSTANCE* instance, VOID* context)
{
	LPDIRECTINPUTDEVICE8 device;

	std::string instanceGUID = GUIDToString(instance->guidInstance.Data1, instance->guidInstance.Data2, instance->guidInstance.Data3, instance->guidInstance.Data4);
	std::string productGUID = GUIDToString(instance->guidProduct.Data1, instance->guidProduct.Data2, instance->guidProduct.Data3, instance->guidProduct.Data4);
	
	std::wcout << "Name: " << instance->tszProductName << "." << std::endl;
	std::cout << "Instance GUID: " << instanceGUID << "." << std::endl;
	std::cout << "Product GUID: " << productGUID << "." << std::endl;

	HRESULT res = directInput->CreateDevice(instance->guidInstance, &device, NULL);

	if (!FAILED(res))
	{
		currentDevice = device;

		if (FAILED(device->EnumObjects(EnumObjectsCallback, NULL, DIDFT_AXIS)))
		{
			std::cout << "Failed to enum controller objects." << std::endl;
			return DIENUM_CONTINUE;
		}

		devices.push_back(std::make_tuple(productGUID, instanceGUID, device));
		return DIENUM_CONTINUE;
	}

	return DIENUM_STOP;
}

void DirectInput::RefreshDeviceList()
{
	directInput->EnumDevices(DI8DEVCLASS_GAMECTRL, EnumJoysticksCallback, nullptr, DIEDFL_ATTACHEDONLY);
}

LPDIRECTINPUTDEVICE8 DirectInput::FindDevice(std::string instanceGUID, std::string productGUID)
{
	for (const auto& device : devices)
	{
		std::string currentProductGUID = std::get<0>(device);
		std::string currentinstanceGUID = std::get<1>(device);

		std::cout << "Checking against " << currentProductGUID << ", " << currentinstanceGUID << std::endl;

		if (currentProductGUID == productGUID || currentinstanceGUID == instanceGUID)
			return std::get<2>(device);
	}
		

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

std::string DirectInput::GUIDToString(const uint64_t& data1, const uint16_t& data2, const uint16_t& data3, const unsigned char* data4)
{
	std::stringstream guidString;

	guidString << std::hex << std::setfill('0') << std::setw(8) << data1 << "-";
	guidString << std::setw(4) << data2 << "-";
	guidString << std::setw(4) << data3 << "-";

	for (int i = 0; i < 2; i++)
		guidString << std::setw(2) << +data4[i];

	guidString << "-";

	for (int i = 2; i < 8; i++)
		guidString << std::setw(2) << +data4[i];

	return guidString.str();
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


