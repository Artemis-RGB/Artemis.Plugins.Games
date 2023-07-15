using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;

public enum DragonType
{
    Unknown = -1,
    [Name("Air")] Cloud,
    [Name("Fire")] Infernal,
    [Name("Water")] Ocean,
    [Name("Earth")] Mountain,
    Chemtech,
    Hextech,
    Elder
}