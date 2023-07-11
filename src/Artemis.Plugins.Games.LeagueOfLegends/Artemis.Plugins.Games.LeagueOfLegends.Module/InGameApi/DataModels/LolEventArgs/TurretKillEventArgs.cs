using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.LolEventArgs;

public class TurretKillEventArgs : DataModelEventArgs
{
    public string KillerName { get; set; }
    public Turret TurretKilled { get; set; }
    public string[] Assisters { get; set; }
}