#include "stdafx.h"
#include "ControllerManager.h"

#include <fstream>
#include "DirectInput.h"

std::vector<std::unique_ptr<Controller>> ControllerManager::controllers;

void ControllerManager::Init()
{
	if (!DirectInput::Init())
	{
		std::cout << "DirectInput failed to initialize." << std::endl;
		return;
	}

	std::cout << "DirectInput initialized." << std::endl;

	controllers.clear();

	for (int i = 0; i < 4; i++)
		controllers.emplace_back(std::make_unique<Controller>());

	wchar_t executablePath[MAX_PATH];
	GetModuleFileNameW(NULL, executablePath, MAX_PATH);

	std::wstring configPath(executablePath);
	configPath = configPath.substr(0, configPath.find_last_of(L'\\')) + L"\\config.bin";

	std::ifstream configFile(configPath, std::ios::binary | std::ios::ate);
	std::wcout << "Path: " << configPath << std::endl;

	if (!configFile || !configFile.good())
	{
		std::cout << "Failed to read config file config.bin." << std::endl;
		return;
	}

	std::ifstream::pos_type fileSize = configFile.tellg();

	std::wcout << "config.bin is " << fileSize << " bytes long." << std::endl;

	std::vector<char> buffer(fileSize);

	configFile.seekg(0, std::ios::beg);
	configFile.read(&buffer[0], fileSize);

	Controller::DEVICE_TYPE type;
	int guidLength = 0;
	char* guid = nullptr;
	std::vector<uint16_t> mappings;
	int byteIndex = 0;

	mappings.resize(24);

	for (int i = 0; i < MAX_CONTROLLERS; i++)
	{
		type = static_cast<Controller::DEVICE_TYPE>(buffer[byteIndex]);
		std::memcpy(&guidLength, &buffer[byteIndex + 1], sizeof(int));

		guid = new char[guidLength + 1];
		std::memcpy(guid, &buffer[byteIndex + 5], guidLength);
		guid[guidLength] = 0;

		byteIndex += 5 + guidLength;
		std::memcpy(&mappings[0], &buffer[byteIndex], sizeof(uint16_t) * 24);

		byteIndex += sizeof(uint16_t) * 24;

		controllers[i]->AssignDIDevice(std::string(guid), type, mappings);
	}
}

DWORD ControllerManager::GetState(DWORD controllerIndex, XINPUT_STATE* pState)
{
	if(!controllers[controllerIndex]->IsConnected())
		return ERROR_DEVICE_NOT_CONNECTED;

	if (controllers[controllerIndex]->Acquire())
	{
		auto gamepadState = controllers[controllerIndex]->GetState();

		std::memcpy(&pState->Gamepad, &gamepadState, sizeof(XINPUT_GAMEPAD));
		pState->dwPacketNumber = GetTickCount();

		if (!controllers[controllerIndex]->Unacquire())
		{
			std::cout << "Failed to unacquire." << std::endl;
		}
	}
	else
	{
		std::cout << "Failed to acquire." << std::endl;
	}

	return ERROR_SUCCESS;
}

DWORD ControllerManager::GetCapabilities(DWORD controllerIndex, DWORD dwFlags, XINPUT_CAPABILITIES* pCapabilities)
{
	if (!controllers[controllerIndex]->IsConnected())
		return ERROR_DEVICE_NOT_CONNECTED;

	pCapabilities->Type = XINPUT_DEVTYPE_GAMEPAD;
	pCapabilities->SubType = XINPUT_DEVSUBTYPE_GAMEPAD;

	return ERROR_SUCCESS;
}
