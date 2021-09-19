#pragma once
#include "lib/json.hpp"
#include "ArtemisObject.h"

class ArtemisCar : ArtemisObject
{
public:
	float Boost;
	float SpeedKph;
	bool SuperSonic;
	bool IsOnWall;
	bool IsOnGround;

	nlohmann::json GetJson();
	void Reset();
};

