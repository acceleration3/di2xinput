#include "stdafx.h"
#include "Hook.h"

#include <mhook-lib/mhook.h>

Hook::_XInputGetState Hook::original_XInputGetState = nullptr;
Hook::_XInputGetCapabilities Hook::original_XInputGetCapabilities = nullptr;

std::vector<std::string> Hook::xinput_modules = {
	"xinput1_1",
	"xinput1_2",
	"xinput1_3",
	"xinput1_4",
	"xinput9_1_0"
};

DWORD WINAPI XInputGetState_hook(DWORD dwUserIndex, XINPUT_STATE* pState)
{
	std::cout << "Called XInputGetState, index " << dwUserIndex << std::endl;
	return Hook::original_XInputGetState(dwUserIndex, pState);
}

DWORD WINAPI XInputGetCapabilities_hook(DWORD dwUserIndex, DWORD dwFlags, XINPUT_CAPABILITIES* pCapabilities)
{
	std::cout << "Called XInputGetCapabilities, index " << dwUserIndex << ", flags " << std::hex << dwFlags << std::endl;
	return Hook::original_XInputGetCapabilities(dwUserIndex, dwFlags, pCapabilities);
}

void Hook::HookXInput()
{
	std::string loaded_module;

	for (auto& mod : xinput_modules)
	{
		if (GetModuleHandleA(mod.c_str()))
		{
			loaded_module = mod;
			std::cout << "XInput module name: " << loaded_module << std::endl;
			break;
		}
	}
		
	original_XInputGetState = (_XInputGetState)GetProcAddress(GetModuleHandleA(loaded_module.c_str()), "XInputGetState");
	original_XInputGetCapabilities = (_XInputGetCapabilities)GetProcAddress(GetModuleHandleA(loaded_module.c_str()), "XInputGetCapabilities");

	if (!Mhook_SetHook((PVOID*)&original_XInputGetState, XInputGetState_hook))
	{
		MessageBox(0, TEXT("Failed to hook XInputGetState."), TEXT("Error"), MB_OK | MB_ICONERROR);
		return;
	}

	if (!Mhook_SetHook((PVOID*)&original_XInputGetCapabilities, XInputGetCapabilities_hook))
	{
		MessageBox(0, TEXT("Failed to hook XInputGetCapabilities."), TEXT("Error"), MB_OK | MB_ICONERROR);
		return;
	}
}

void Hook::UnhookXInput()
{
}