using Artemis.Core.Modules;

namespace Artemis.Plugins.Games.RocketLeague
{
    public class RocketLeagueDataModel : DataModel
    {
        public RocketLeagueGameStatus Status { get; set; }
        public RocketLeagueMatch Match { get; set; }
        public RocketLeaguePlayer Player { get; set; }
        public RocketLeagueCar Car { get; set; }
    }
}