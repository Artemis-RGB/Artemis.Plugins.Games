using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.EventArgs
{
    public class InhibRespawningSoonEventArgs : DataModelEventArgs
    {
        public Inhibitor InhibRespawningSoon { get; set; }
    }
}
