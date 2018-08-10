
#include "stdafx.h"
#include "Hook.h"
#include "IPC.h"

#define DEBUG_CONSOLE

void MainThread()
{
	#ifdef DEBUG_CONSOLE
	if (AllocConsole())
	{
		std::freopen("CONOUT$", "wt", stdout);
		std::freopen("CONIN$", "rt", stdin);
		SetConsoleTitle(L"Debug Console");
		std::ios::sync_with_stdio(1);
	}
	#endif
	
	Hook::HookXInput();
	IPC::Init();

	while (true)
	{
		//IPC::Poll();
		Sleep(4000);
	}
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
		case DLL_PROCESS_ATTACH:
			DisableThreadLibraryCalls(hModule);
			CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)MainThread, NULL, NULL, NULL);
			break;

		case DLL_PROCESS_DETACH:
			Hook::UnhookXInput();
			break;
    }
    return TRUE;
}

