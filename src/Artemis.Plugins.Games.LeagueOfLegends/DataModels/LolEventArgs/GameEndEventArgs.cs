using Artemis.Core;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.EventArgs
{
    public class GameEndEventArgs : DataModelEventArgs
    {
        public bool Win { get; set; }
    }
}
