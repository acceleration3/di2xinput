#pragma once

#include <Xinput.h>
#include "DIBinding.h"

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

	void AssignDIDevice(const std::string& guid, DEVICE_TYPE type, const std::vector<uint16_t>& mappings);
	bool IsConnected();
	bool Acquire();
	bool Unacquire();

	XINPUT_GAMEPAD GetState();
	DEVICE_TYPE GetType() { return type; };

private:
	std::array<std::unique_ptr<DIBinding>, 24> bindings;
	std::string deviceGuid;
	XINPUT_GAMEPAD gamepad;
	DEVICE_TYPE type;
	LPDIRECTINPUTDEVICE8 DIDevice;
	XINPUT_GAMEPAD lastState;

	static std::vector<int> buttonFlags;
};

