#pragma once
#include "ArtemisObject.h"
#include "ArtemisMatch.h"
#include "ArtemisPlayer.h"
#include "ArtemisCar.h"
#include "GameStatus.h"

class ArtemisGame : ArtemisObject
{
public:
	ArtemisMatch Match;
	ArtemisPlayer Player;
	ArtemisCar Car;
	GameStatus Status;

	void Reset();
	nlohmann::json GetJson();
};

