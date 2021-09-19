#pragma once
#include "lib/json.hpp"
#include "ArtemisObject.h"

class ArtemisPlayer : ArtemisObject
{
public:
	int Team;
	int Score;
	int Goals;
	int Assists;
	int Saves;
	int Shots;
	int BallTouches;
	int CarTouches;
	int Demolishes;

	nlohmann::json GetJson();
	void Reset();
};

