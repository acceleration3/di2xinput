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
	configPath = configPath.substr(0, configPath.find_last_of(L'\\')) + L"\\di2xiprofile.bin";

	std::ifstream configFile(configPath, std::ios::binary | std::ios::ate);
	std::wcout << "Path: " << configPath << std::endl;

	if (!configFile || !configFile.good())
	{
		std::cout << "Failed to read config file di2xiprofile.bin." << std::endl;
		return;
	}

	std::ifstream::pos_type fileSize = configFile.tellg();

	std::wcout << "di2xiprofile.bin is " << fileSize << " bytes long." << std::endl;

	std::vector<char> buffer(fileSize);

	configFile.seekg(0, std::ios::beg);
	configFile.read(&buffer[0], fileSize);

	Controller::DEVICE_TYPE type;
	int guidLength1 = 0;
	int guidLength2 = 0;
	char* instanceGUID = nullptr;
	char* productGUID = nullptr;
	std::vector<uint16_t> mappings;
	int byteIndex = 0;

	mappings.resize(24);

	for (int i = 0; i < MAX_CONTROLLERS; i++)
	{
		type = static_cast<Controller::DEVICE_TYPE>(buffer[byteIndex]);
		std::memcpy(&guidLength1, &buffer[byteIndex + 1], sizeof(int));
		byteIndex += 5;

		instanceGUID = new char[guidLength1 + 1];
		std::memcpy(instanceGUID, &buffer[byteIndex], guidLength1);
		instanceGUID[guidLength1] = 0;
		byteIndex += guidLength1;

		std::memcpy(&guidLength2, &buffer[byteIndex], sizeof(int));
		byteIndex += 4;

		productGUID = new char[guidLength2 + 1];
		std::memcpy(productGUID, &buffer[byteIndex], guidLength2);
		productGUID[guidLength2] = 0;
		byteIndex += guidLength2;

		std::memcpy(&mappings[0], &buffer[byteIndex], sizeof(uint16_t) * 24);
		byteIndex += sizeof(uint16_t) * 24;

		controllers[i]->AssignDIDevice(instanceGUID, productGUID, type, mappings);
	}
}

bool ControllerManager::IsConnected(DWORD controllerIndex)
{
	return controllers[controllerIndex]->IsConnected();
}

DWORD ControllerManager::GetState(DWORD controllerIndex, XINPUT_STATE* pState)
{
	if (PollGamepad(controllerIndex, &pState->Gamepad))
	{
		pState->dwPacketNumber = GetTickCount();
		return ERROR_SUCCESS;
	}
	else
	{
		std::cout << "Failed to poll for gamepad state." << std::endl;
		return ERROR_DEVICE_UNREACHABLE;
	}
}

DWORD ControllerManager::GetCapabilities(DWORD controllerIndex, DWORD dwFlags, XINPUT_CAPABILITIES* pCapabilities)
{
	if (PollGamepad(controllerIndex, &pCapabilities->Gamepad))
	{
		pCapabilities->Type = XINPUT_DEVTYPE_GAMEPAD;
		pCapabilities->SubType = XINPUT_DEVSUBTYPE_GAMEPAD;
		return ERROR_SUCCESS;
	}
	else
	{
		std::cout << "Failed to poll for gamepad capabilities." << std::endl;
		return ERROR_DEVICE_UNREACHABLE;
	}
}

bool ControllerManager::PollGamepad(int index, XINPUT_GAMEPAD* gamepad)
{
	auto res = controllers[index]->Acquire();

	if (SUCCEEDED(res))
	{
		auto gamepadState = controllers[index]->GetState();
		std::memcpy(gamepad, &gamepadState, sizeof(XINPUT_GAMEPAD));
		res = controllers[index]->Unacquire();

		if (FAILED(res))
		{
			std::cout << "Failed to unacquire. HRESULT=0x" << std::hex << res << std::endl;
		}

		return true;
	}
	else
	{
		std::cout << "Failed to acquire. HRESULT=0x" << std::hex << res << std::endl;
		return false;
	}
}
