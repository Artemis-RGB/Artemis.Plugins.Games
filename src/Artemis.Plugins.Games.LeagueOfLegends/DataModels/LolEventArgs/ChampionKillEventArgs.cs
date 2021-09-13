using Artemis.Core;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.EventArgs
{
    public class ChampionKillEventArgs : DataModelEventArgs
    {
        public string KillerName { get; set; }
        public string VictimName { get; set; }
        public string[] Assisters { get; set; }
    }
}
