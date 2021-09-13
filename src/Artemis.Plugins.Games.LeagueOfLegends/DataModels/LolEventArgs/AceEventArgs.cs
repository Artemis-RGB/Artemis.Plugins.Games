using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.EventArgs
{
    public class AceEventArgs : DataModelEventArgs
    {
        public string Acer { get; set; }
        public Team AcingTeam { get; set; }
    }
}
