#pragma once

#include "stdafx.h"

class DIBinding
{
public:
	
	typedef enum
	{
		BUTTON_BINDING = 1,
		POV_BINDING,
		AXIS_BINDING
	} BINDING_TYPE;

	DIBinding(BINDING_TYPE type) { this->type = type; };
	~DIBinding() = default;

	virtual void Update(const DIJOYSTATE2& joystate) = 0;

	BINDING_TYPE type;
};

class DIButtonBinding : public DIBinding
{

public:
	DIButtonBinding(int index, bool bKeyboard = false)
		: DIBinding(DIBinding::BINDING_TYPE::BUTTON_BINDING), index(index), bKeyboard(bKeyboard)
	{
	}

	virtual void Update(const DIJOYSTATE2& joystate)
	{
		if (bKeyboard)
		{
			index &= ~0x100;
			int vkCode = MapVirtualKey(index, MAPVK_VSC_TO_VK);
			state = GetAsyncKeyState(vkCode) != 0;
		}
		else
		{
			state = joystate.rgbButtons[index] != 0;
		}
	}

	bool GetState() { return state; };

private:
	bool state;
	int index;
	bool bKeyboard;
};

class DIPovBinding : public DIBinding
{	

public:
	typedef enum
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}POV_DIRECTION;

	POV_DIRECTION direction;

	DIPovBinding(int index, POV_DIRECTION direction) 
		: DIBinding(DIBinding::BINDING_TYPE::POV_BINDING), index(index), direction(direction)
	{
	}

	~DIPovBinding() = default;

	bool GetState() { return state; };

	virtual void Update(const DIJOYSTATE2& joystate)
	{
		const int value = joystate.rgdwPOV[index];
		
		if (value == -1)
		{
			state = false;
			return;
		}
		
		const int angleDiv = ((value + 2250) / 4500) % 8;
		
		switch (direction)
		{
			case POV_DIRECTION::UP: 
				state = (angleDiv == 0 || angleDiv == 1 || angleDiv == 7);
				break;
			case POV_DIRECTION::RIGHT:
				state = (angleDiv == 1 || angleDiv == 2 || angleDiv == 3);
				break;
			case POV_DIRECTION::DOWN:
				state = (angleDiv == 3 || angleDiv == 4 || angleDiv == 5);
				break;
			case POV_DIRECTION::LEFT:
				state = (angleDiv == 5 || angleDiv == 6 || angleDiv == 7);
				break;
		}
	}

private:
	int index;
	bool state;
};

class DIAxisBinding : public DIBinding
{
public:

	int index;

	DIAxisBinding(int index, bool isNegative)
		: DIBinding(DIBinding::BINDING_TYPE::AXIS_BINDING), index(index), isNegative(isNegative)
	{
	}

	virtual void Update(const DIJOYSTATE2& joystate)
	{
		switch (index)
		{
			case 0: value = joystate.lX; break;
			case 1: value = joystate.lY; break;
			case 2: value = joystate.lZ; break;
			case 3: value = joystate.lRx; break;
			case 4: value = joystate.lRy; break;
			case 5: value = joystate.lRz; break;
		}
	}

	int GetValue()
	{
		return value;
	}

	bool IsNegative()
	{
		return isNegative;
	}

private:
	int value;
	bool isNegative;
};