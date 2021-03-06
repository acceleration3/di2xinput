#include "stdafx.h"
#include "ControllerManager.h"
#include "Exports.h"

#define DEBUG_CONSOLE

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
		case DLL_PROCESS_ATTACH:
			
			#ifdef DEBUG_CONSOLE
			if (AllocConsole())
			{
				std::freopen("CONOUT$", "wt", stdout);
				std::freopen("CONIN$", "rt", stdin);
				SetConsoleTitle(L"Debug Console");
				std::ios::sync_with_stdio(1);
			}
			#endif
			DisableThreadLibraryCalls(hModule);

			ControllerManager::Init();

			break;

		case DLL_PROCESS_DETACH:
			break;
    }
    return TRUE;
}

