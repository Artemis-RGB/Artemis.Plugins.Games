using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums
{
    public enum SummonerSpell
    {
        Unknown = -1,
        None = 0,
        Cleanse,
        Exhaust,
        Flash,
        Ghost,
        Heal,
        [Name("Chilling Smite", "Challenging Smite")]
        Smite,
        Teleport,
        Clarity,
        Ignite,
        Barrier,
        Mark,
        Dash
    }
}
