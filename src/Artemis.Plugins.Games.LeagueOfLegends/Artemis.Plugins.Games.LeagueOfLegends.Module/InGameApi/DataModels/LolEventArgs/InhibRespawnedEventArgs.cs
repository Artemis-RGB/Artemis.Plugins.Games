using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.LolEventArgs;

public class InhibRespawnedEventArgs : DataModelEventArgs
{
    public Inhibitor InhibRespawned { get; set; }
}