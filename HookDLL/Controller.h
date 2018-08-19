#pragma once

#include <Xinput.h>

class Controller
{

public:

	typedef enum
	{
		NONE,
		KEYBOARD,
		GAMEPAD
	} DEVICE_TYPE;

	Controller();
	~Controller() = default;

	void SetDeviceType(DEVICE_TYPE type);
	void SetDeviceGuid(std::string guid);
	void SetMappings(const std::vector<uint16_t>& mappings);
	bool IsConnected();

	XINPUT_GAMEPAD GetState();
	DEVICE_TYPE GetType() { return type; };

private:
	std::string deviceGuid;
	XINPUT_GAMEPAD gamepad;
	DEVICE_TYPE type;
};

