using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;

public enum Team
{
    Unknown = -1,
    None = 0,
    [Name("ORDER")] Blue,
    [Name("CHAOS")] Red
}