#pragma once

#include <Xinput.h>

class Hook
{
public:

	typedef DWORD(WINAPI* _XInputGetState)
	(
		_In_  DWORD         dwUserIndex,  // Index of the gamer associated with the device
		_Out_ XINPUT_STATE* pState        // Receives the current state
	);

	typedef DWORD(WINAPI* _XInputGetCapabilities)
	(
		_In_  DWORD                dwUserIndex,   // Index of the gamer associated with the device
		_In_  DWORD                dwFlags,       // Input flags that identify the device type
		_Out_ XINPUT_CAPABILITIES* pCapabilities  // Receives the capabilities
	);

	static std::vector<std::string> xinput_modules;
	static _XInputGetState original_XInputGetState;
	static _XInputGetCapabilities original_XInputGetCapabilities;

	static void HookXInput();
	static void UnhookXInput();
};

