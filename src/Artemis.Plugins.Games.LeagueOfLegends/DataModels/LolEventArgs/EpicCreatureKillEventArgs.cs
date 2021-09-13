using Artemis.Core;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.EventArgs
{
    public class EpicCreatureKillEventArgs : DataModelEventArgs
    {
        public bool Stolen { get; set; }
        public string KillerName { get; set; }
        public string[] Assisters { get; set; }
    }
}
