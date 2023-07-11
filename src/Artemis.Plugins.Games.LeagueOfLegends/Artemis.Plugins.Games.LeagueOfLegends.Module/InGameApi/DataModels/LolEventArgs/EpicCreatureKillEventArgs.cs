using Artemis.Core;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.LolEventArgs;

public class EpicCreatureKillEventArgs : DataModelEventArgs
{
    public bool Stolen { get; set; }
    public string KillerName { get; set; }
    public string[] Assisters { get; set; }
}