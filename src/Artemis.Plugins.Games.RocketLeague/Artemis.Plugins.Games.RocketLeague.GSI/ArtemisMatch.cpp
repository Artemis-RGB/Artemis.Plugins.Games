#include "pch.h"
#include "ArtemisMatch.h"

nlohmann::json ArtemisMatch::GetJson()
{
	nlohmann::json json_match;
	json_match["playlist"] = Playlist;
	json_match["time"] = Time;
	json_match["unlimitedTime"] = UnlimitedTime;
	json_match["overtime"] = Overtime;

	json_match["team_0"] = Teams[0].GetJson();
	json_match["team_1"] = Teams[1].GetJson();

	return json_match;
}

void ArtemisMatch::Reset()
{
	Teams[0].Reset();
	Teams[1].Reset();
	Time = -1;
	Overtime = false;
	UnlimitedTime = false;
	Playlist = -1;
}
