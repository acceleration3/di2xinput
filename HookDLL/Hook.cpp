#include "stdafx.h"
#include "Hook.h"

#include <mhook-lib/mhook.h>
#include "ControllerManager.h"

Hook::_XInputGetState Hook::originalXInputGetState = nullptr;
Hook::_XInputGetCapabilities Hook::originalXInputGetCapabilities = nullptr;

std::vector<std::string> Hook::xinputModules = {
	"xinput1_1",
	"xinput1_2",
	"xinput1_3",
	"xinput1_4",
	"xinput9_1_0"
};

DWORD WINAPI XInputGetState_hook(DWORD dwUserIndex, XINPUT_STATE* pState)
{
	std::cout << "Called XInputGetState, index " << dwUserIndex << std::endl;
	return ControllerManager::GetState(dwUserIndex, pState);
}

DWORD WINAPI XInputGetCapabilities_hook(DWORD dwUserIndex, DWORD dwFlags, XINPUT_CAPABILITIES* pCapabilities)
{
	std::cout << "Called XInputGetCapabilities, index " << dwUserIndex << ", flags " << std::hex << dwFlags << std::endl;
	return ControllerManager::GetCapabilities(dwUserIndex, dwFlags, pCapabilities);
}

void Hook::HookXInput()
{
	std::string loadedModule;

	for (auto& mod : xinputModules)
	{
		if (GetModuleHandleA(mod.c_str()))
		{
			loadedModule = mod;
			std::cout << "XInput module name: " << loadedModule << std::endl;
			break;
		}
	}
		
	originalXInputGetState = (_XInputGetState)GetProcAddress(GetModuleHandleA(loadedModule.c_str()), "XInputGetState");
	originalXInputGetCapabilities = (_XInputGetCapabilities)GetProcAddress(GetModuleHandleA(loadedModule.c_str()), "XInputGetCapabilities");

	if (!Mhook_SetHook((PVOID*)&originalXInputGetState, XInputGetState_hook))
	{
		MessageBox(0, TEXT("Failed to hook XInputGetState."), TEXT("Error"), MB_OK | MB_ICONERROR);
		return;
	}

	if (!Mhook_SetHook((PVOID*)&originalXInputGetCapabilities, XInputGetCapabilities_hook))
	{
		MessageBox(0, TEXT("Failed to hook XInputGetCapabilities."), TEXT("Error"), MB_OK | MB_ICONERROR);
		return;
	}
}

void Hook::UnhookXInput()
{
}