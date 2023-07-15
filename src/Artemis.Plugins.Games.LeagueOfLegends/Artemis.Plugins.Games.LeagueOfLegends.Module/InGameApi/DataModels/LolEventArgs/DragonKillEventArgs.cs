using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.LolEventArgs;

public class DragonKillEventArgs : DataModelEventArgs
{
    public DragonType DragonType { get; set; }
    public bool Stolen { get; set; }
    public string KillerName { get; set; }
    public string[] Assisters { get; set; }
}