using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.LolEventArgs;

public class InhibKillEventArgs : DataModelEventArgs
{
    public string KillerName { get; set; }
    public Inhibitor InhibKilled { get; set; }
    public string[] Assisters { get; set; }
}