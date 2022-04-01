using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.LolEventArgs
{
    public class AceEventArgs : DataModelEventArgs
    {
        public string Acer { get; set; }
        public Team AcingTeam { get; set; }
    }
}
