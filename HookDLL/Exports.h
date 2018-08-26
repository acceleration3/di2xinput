#pragma once

#include "stdafx.h"
#include "ControllerManager.h"

void WINAPI Export_XInputEnable(BOOL enable)
{
	std::cout << "XInputEnable." << std::endl;
}

DWORD WINAPI Export_XInputGetAudioDeviceIds(DWORD dwUserIndex, LPWSTR pRenderDeviceId, UINT* pRenderCount, LPWSTR pCaptureDeviceId, UINT* pCaptureCount)
{
	std::cout << "XInputGetAudioDeviceIds." << std::endl;
	return 0;
}

DWORD WINAPI Export_XInputGetDSoundAudioDeviceGuids(DWORD dwUserIndex, GUID* pDSoundRenderGuid, GUID* pDSoundCaptureGuid)
{
	std::cout << "XInputGetDSoundAudioDeviceGuids." << std::endl;
	return 0;
}

DWORD WINAPI Export_XInputGetBatteryInformation(DWORD dwUserIndex, BYTE devType, XINPUT_BATTERY_INFORMATION* pBatteryInformation)
{
	std::cout << "XInputGetBatteryInformation." << std::endl;

	if (devType == BATTERY_DEVTYPE_GAMEPAD && !ControllerManager::IsConnected(dwUserIndex))
		return ERROR_DEVICE_NOT_CONNECTED;
	
	pBatteryInformation->BatteryLevel = BATTERY_LEVEL_FULL;
	pBatteryInformation->BatteryType = BATTERY_TYPE_WIRED;

	return 0;
}

DWORD WINAPI Export_XInputGetKeystroke(DWORD dwUserIndex, DWORD dwReserved, PXINPUT_KEYSTROKE pKeystroke)
{
	std::cout << "XInputGetKeystroke." << std::endl;
	return 0;
}

DWORD WINAPI Export_XInputGetState(DWORD dwUserIndex, XINPUT_STATE* pState)
{
	if (!ControllerManager::IsConnected(dwUserIndex))
		return ERROR_DEVICE_NOT_CONNECTED;

	return ControllerManager::GetState(dwUserIndex, pState);
}

DWORD WINAPI Export_XInputGetStateEx(DWORD dwUserIndex, XINPUT_STATE* pState)
{
	std::cout << "XInputGetStateEx." << std::endl;
	return ControllerManager::GetState(dwUserIndex, pState);
}

DWORD WINAPI Export_XInputSetState(DWORD dwUserIndex, XINPUT_VIBRATION* pVibration)
{
	if (!ControllerManager::IsConnected(dwUserIndex))
		return ERROR_DEVICE_NOT_CONNECTED;

	return ERROR_SUCCESS;
}

DWORD WINAPI Export_XInputGetCapabilities(DWORD dwUserIndex, DWORD dwFlags, XINPUT_CAPABILITIES* pCapabilities)
{
	if (!ControllerManager::IsConnected(dwUserIndex))
		return ERROR_DEVICE_NOT_CONNECTED;

	return ControllerManager::GetCapabilities(dwUserIndex, dwFlags, pCapabilities);
}