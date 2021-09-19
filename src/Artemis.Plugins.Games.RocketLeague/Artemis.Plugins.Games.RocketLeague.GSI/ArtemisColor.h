#pragma once
#include "lib/json.hpp"
#include "ArtemisObject.h"

class ArtemisColor : ArtemisObject
{
public:
	unsigned char Red;
	unsigned char Green;
	unsigned char Blue;

	nlohmann::json GetJson();
	void Reset();
	void SetValues(LinearColor source);
};

