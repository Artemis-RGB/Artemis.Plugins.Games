#include "pch.h"
#include "ArtemisPlayer.h"

nlohmann::json ArtemisPlayer::GetJson()
{
	nlohmann::json json_player;
	json_player["team"] = Team;
	json_player["score"] = Score;
	json_player["goals"] = Goals;
	json_player["assists"] = Assists;
	json_player["saves"] = Saves;
	json_player["shots"] = Shots;
	json_player["ballTouches"] = BallTouches;
	json_player["carTouches"] = CarTouches;
	json_player["demolishes"] = Demolishes;
	return json_player;
}

void ArtemisPlayer::Reset()
{
	Team = -1;
	Score = -1;
	Goals = -1;
	Assists = -1;
	Saves = -1;
	Shots = -1;
	BallTouches = -1;
	CarTouches = -1;
	Demolishes = -1;
}
