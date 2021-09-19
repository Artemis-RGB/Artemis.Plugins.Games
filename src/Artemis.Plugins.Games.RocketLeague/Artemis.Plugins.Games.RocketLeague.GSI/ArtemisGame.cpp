#include "pch.h"
#include "ArtemisGame.h"

nlohmann::json ArtemisGame::GetJson()
{
	nlohmann::json data;
	data["status"] = Status;
	data["player"] = Player.GetJson();
	data["match"] = Match.GetJson();
	data["car"] = Car.GetJson();

	return data;
}

void ArtemisGame::Reset()
{
	Status = GameStatus::Menu;
	Player.Reset();
	Match.Reset();
	Car.Reset();
}
