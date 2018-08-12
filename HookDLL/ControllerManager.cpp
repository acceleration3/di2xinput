#include "stdafx.h"
#include "ControllerManager.h"

std::random_device rd;
std::mt19937 e2(rd());
uint64_t packetNumber = 0;

std::vector<std::unique_ptr<Controller>> ControllerManager::controllers;
std::mutex ControllerManager::stateLock;

void ControllerManager::Init()
{
	for (int i = 0; i < MAX_CONTROLLERS; i++)
		controllers.push_back(std::make_unique<Controller>());
}

void ControllerManager::SetMappingsFromBuffer(const std::vector<uint8_t>& buffer)
{
	std::cout << "ControllerManager -> Setting mappings from IPC buffer." << std::endl;

	stateLock.lock();
	
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

		controllers[i]->SetDeviceGuid(std::string(guid));
		controllers[i]->SetDeviceType(type);
		controllers[i]->SetMappings(mappings);
	}

	stateLock.unlock();
}

DWORD ControllerManager::GetState(DWORD controllerIndex, XINPUT_STATE* pState)
{
	std::cout << "ControllerManager -> GetState (index=" << controllerIndex << ")" << std::endl;

	if(controllers[controllerIndex]->GetType() == Controller::DEVICE_TYPE::NONE)
		return ERROR_DEVICE_NOT_CONNECTED;

	stateLock.lock();

	stateLock.unlock();

	return ERROR_SUCCESS;
}

DWORD ControllerManager::GetCapabilities(DWORD controllerIndex, DWORD dwFlags, XINPUT_CAPABILITIES* pCapabilities)
{
	std::cout << "ControllerManager -> GetCapabilities (index=" << controllerIndex << ",flags=" << dwFlags << ")" << std::endl;

	if (controllers[controllerIndex]->GetType() == Controller::DEVICE_TYPE::NONE)
		return ERROR_DEVICE_NOT_CONNECTED;

	stateLock.lock();

	stateLock.unlock();

	return ERROR_SUCCESS;
}
