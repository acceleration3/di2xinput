#pragma once

#define DIRECTINPUT_VERSION 0x0800
#include <dinput.h>

class DirectInput
{
private:
	static LPDIRECTINPUT8 directInput;
	static std::map<std::string, LPDIRECTINPUTDEVICE8> devices;

public:
	static bool Init();
	static BOOL CALLBACK EnumJoysticksCallback(const DIDEVICEINSTANCE* instance, VOID* context);
	static void RefreshDeviceList();
	static LPDIRECTINPUTDEVICE8 FindDevice(std::string guid);
};

