#pragma once
#include "lib/json.hpp"
#include "ArtemisObject.h"
#include "ArtemisColor.h"

class ArtemisTeam : ArtemisObject
{
public:
	std::string Name;
	int Goals;
	ArtemisColor PrimaryColor;
	ArtemisColor SecondaryColor;
	ArtemisColor FontColor;

	nlohmann::json GetJson();
	void Reset();
};