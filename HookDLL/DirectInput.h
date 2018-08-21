#pragma once

class DirectInput
{
private:
	static LPDIRECTINPUT8 directInput;
	static std::map<std::string, LPDIRECTINPUTDEVICE8> devices;
	static HWND dummyWindow;
public:

	static LPDIRECTINPUT8 GetInstance() { return directInput; };
	static bool Init();
	static BOOL CALLBACK EnumJoysticksCallback(const DIDEVICEINSTANCE* instance, VOID* context);
	static void RefreshDeviceList();
	static LPDIRECTINPUTDEVICE8 FindDevice(std::string guid);
	static bool InitializeDevice(LPDIRECTINPUTDEVICE8 device);
};

