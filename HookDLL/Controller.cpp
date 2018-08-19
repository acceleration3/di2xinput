#include "stdafx.h"
#include "Controller.h"

Controller::Controller() : type(DEVICE_TYPE::NONE), deviceGuid("") {}

void Controller::SetDeviceType(DEVICE_TYPE type)
{
	this->type = type;
}

void Controller::SetDeviceGuid(std::string guid)
{
	this->deviceGuid = guid;
}

void Controller::SetMappings(const std::vector<uint16_t>& mappings)
{
	
}

bool Controller::IsConnected()
{
	return type != DEVICE_TYPE::NONE;
}

XINPUT_GAMEPAD Controller::GetState()
{
	XINPUT_GAMEPAD state = { 0 };

	return state;
}
