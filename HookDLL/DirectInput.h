#pragma once

class DirectInput
{
private:
	static LPDIRECTINPUT8 directInput;
	static std::vector<std::tuple<std::string, std::string, LPDIRECTINPUTDEVICE8>> devices;
	static HWND dummyWindow;
	static LPDIRECTINPUTDEVICE8 currentDevice;
	static std::string GUIDToString(const uint64_t& data1, const uint16_t& data2, const uint16_t& data3, const unsigned char* data4);

public:

	static LPDIRECTINPUT8 GetInstance() { return directInput; };
	static bool Init();
	static void RefreshDeviceList();
	static LPDIRECTINPUTDEVICE8 FindDevice(std::string productGUID, std::string instanceGUID);
	static bool InitializeDevice(LPDIRECTINPUTDEVICE8 device);

	static BOOL CALLBACK EnumJoysticksCallback(const DIDEVICEINSTANCE* instance, VOID* context);
	static BOOL CALLBACK EnumObjectsCallback(const DIDEVICEOBJECTINSTANCE* pdidoi, VOID* pContext);

};

