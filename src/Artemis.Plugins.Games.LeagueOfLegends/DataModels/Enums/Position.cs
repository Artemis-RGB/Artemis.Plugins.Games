
using Artemis.Plugins.Games.LeagueOfLegends.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums
{
    public enum Position
    {
        Unknown = -1,
        None = 0,
        Top,
        Jungle,
        Middle,
        Bottom,
        [Name("UTILITY")]
        Support
    }
}
