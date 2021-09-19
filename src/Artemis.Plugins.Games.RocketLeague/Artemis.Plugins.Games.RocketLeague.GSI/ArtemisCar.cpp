#include "pch.h"
#include "ArtemisCar.h"

nlohmann::json ArtemisCar::GetJson()
{
	nlohmann::json json_car;

	json_car["boost"] = Boost * 100.0f;
	json_car["speedKph"] = SpeedKph;
	json_car["superSonic"] = SuperSonic;
	json_car["isOnWall"] = IsOnWall;
	json_car["isOnGround"] = IsOnGround;

	return json_car;
}

void ArtemisCar::Reset()
{
	Boost = -1;
	SpeedKph = -1;
	SuperSonic = false;
	IsOnWall = false;
	IsOnGround = false;
}
