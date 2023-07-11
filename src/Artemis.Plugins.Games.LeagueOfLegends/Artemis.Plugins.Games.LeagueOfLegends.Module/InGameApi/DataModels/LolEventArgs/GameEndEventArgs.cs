using Artemis.Core;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.LolEventArgs;

public class GameEndEventArgs : DataModelEventArgs
{
    public bool Win { get; set; }
}