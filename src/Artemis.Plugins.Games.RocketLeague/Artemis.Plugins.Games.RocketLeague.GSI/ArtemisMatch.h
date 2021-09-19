#pragma once
#include "lib/json.hpp"
#include "ArtemisTeam.h"
#include "ArtemisObject.h"

class ArtemisMatch : ArtemisObject
{
public:
	ArtemisTeam Teams[2];
	int Time;
	bool Overtime;
	bool UnlimitedTime;
	int Playlist;
	nlohmann::json GetJson();
	void Reset();
};

