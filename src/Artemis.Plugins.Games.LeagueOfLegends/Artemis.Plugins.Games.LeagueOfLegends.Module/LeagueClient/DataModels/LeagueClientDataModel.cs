using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents.EventData;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.DataModels;

public class LeagueClientDataModel : DataModel
{
    public DataModelEvent QueuePop { get; set; } = new();
}