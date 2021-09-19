#pragma once
#include "lib/json.hpp"

class ArtemisObject
{
public:
	virtual void Reset() = 0;
	virtual nlohmann::json GetJson() = 0;
};

