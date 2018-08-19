#pragma once

#include "stdafx.h"

class DIBinding
{
public:
	
	typedef enum
	{
		POV_BINDING,
		BUTTON_BINDING,
		KEY_BINDING,
		AXIS_BINDING
	} BINDING_TYPE;

	DIBinding(BINDING_TYPE type) { this->type = type; };
	~DIBinding();

	virtual short GetValue(const DIJOYSTATE& joystate);

protected:
	BINDING_TYPE type;

};

class DIPovBinding : private DIBinding
{	

public:
	typedef enum
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}POV_DIRECTION;

	DIPovBinding(int index, POV_DIRECTION direction) 
		: DIBinding(DIBinding::BINDING_TYPE::POV_BINDING), index(index), direction(direction)
	{
	}

	virtual short GetValue(const DIJOYSTATE& joystate)
	{
		int degrees = joystate.rgdwPOV[index] / 100;
		
		if (degrees == -1)
			return 0;

		switch (direction)
		{
			case POV_DIRECTION::UP:
				if ((degrees >= 293 && degrees <= 360) || (degrees >= 0 && degrees < 68))
					return 1;
				else
					return 0;
				break;
			case POV_DIRECTION::RIGHT:
				if ((degrees >= 23 && degrees < 158))
					return 1;
				else
					return 0;
			case POV_DIRECTION::DOWN:
				if ((degrees >= 113 && degrees <= 248))
					return 1;
				else
					return 0;
			case POV_DIRECTION::LEFT:
				if ((degrees >= 203 && degrees <= 338))
					return 1;
				else
					return 0;
		}
	}

private:
	POV_DIRECTION direction;
	int index;
};
