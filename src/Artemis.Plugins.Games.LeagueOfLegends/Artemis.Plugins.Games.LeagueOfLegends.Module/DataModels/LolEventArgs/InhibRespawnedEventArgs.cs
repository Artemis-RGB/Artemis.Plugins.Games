using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.EventArgs
{
    public class InhibRespawnedEventArgs : DataModelEventArgs
    {
        public Inhibitor InhibRespawned { get; set; }
    }
}
