#include "pch.h"
#include "ArtemisColor.h"

nlohmann::json ArtemisColor::GetJson()
{
    nlohmann::json json;

    json["red"] = Red;
    json["green"] = Green;
    json["blue"] = Blue;

    return json;
}

void ArtemisColor::Reset()
{
    Red = 0;
    Green = 0;
    Blue = 0;
}

void ArtemisColor::SetValues(LinearColor source)
{
    Red = (unsigned char)(source.R * 255.0);
    Green = (unsigned char)(source.G * 255.0);
    Blue = (unsigned char)(source.B * 255.0);
}
