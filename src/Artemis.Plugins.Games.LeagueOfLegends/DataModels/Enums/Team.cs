using Artemis.Plugins.Games.LeagueOfLegends.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums
{
    public enum Team
    {
        Unknown = -1,
        None = 0,
        [Name("ORDER")]
        Blue,
        [Name("CHAOS")]
        Red
    }
}
