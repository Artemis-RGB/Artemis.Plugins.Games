using Artemis.Plugins.Games.LeagueOfLegends.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums
{
    public enum DragonType
    {
        Unknown = -1,
        [Name("Air")]
        Cloud,
        [Name("Fire")]
        Infernal,
        [Name("Water")]
        Ocean,
        [Name("Earth")]
        Mountain,
        Chemtech,
        Hextech,
        Elder
    }
}
