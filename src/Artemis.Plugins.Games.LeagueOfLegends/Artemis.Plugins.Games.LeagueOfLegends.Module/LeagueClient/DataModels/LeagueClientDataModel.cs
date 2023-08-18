using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents.EventData;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.DataModels;

public class LeagueClientDataModel : DataModel
{
    public GameFlowDataModel GameFlow { get; set; } = new();
}

public class GameFlowDataModel : DataModel
{
    public GameFlowPhase Phase { get; set; } = GameFlowPhase.None;
    public DataModelEvent QueuePop { get; set; } = new();
    public DataModelEvent ChampSelect { get; set; } = new();
    public DataModelEvent Matchmaking { get; set; } = new();
    public DataModelEvent Lobby { get; set; } = new();
}