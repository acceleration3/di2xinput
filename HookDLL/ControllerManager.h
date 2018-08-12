#pragma once

#include "Controller.h"

#define MAX_CONTROLLERS 4

class ControllerManager
{
public:
	static void Init();
	static void SetMappingsFromBuffer(const std::vector<uint8_t>& buffer);
	static DWORD GetState(DWORD controllerIndex, XINPUT_STATE* pState);
	static DWORD GetCapabilities(DWORD controllerIndex, DWORD dwFlags, XINPUT_CAPABILITIES* pCapabilities);
private:
	static std::vector<std::unique_ptr<Controller>> controllers;
	static std::mutex stateLock;
};

