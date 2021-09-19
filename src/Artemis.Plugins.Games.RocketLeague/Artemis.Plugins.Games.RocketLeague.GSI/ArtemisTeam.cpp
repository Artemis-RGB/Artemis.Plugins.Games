#include "pch.h"
#include "ArtemisTeam.h"

nlohmann::json ArtemisTeam::GetJson()
{
	nlohmann::json json_team;
	json_team["goals"] = Goals;
	json_team["name"] = Name;
	json_team["primaryColor"] = PrimaryColor.GetJson();
	json_team["secondaryColor"] = SecondaryColor.GetJson();
	json_team["fontColor"] = FontColor.GetJson();
	return json_team;
}

void ArtemisTeam::Reset()
{
	Name = "";
	Goals = -1;
	PrimaryColor.Reset();
	SecondaryColor.Reset();
	FontColor.Reset();
}
