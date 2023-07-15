using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels;

public class LeagueOfLegendsDataModel : DataModel
{
    public PlayerDataModel Player { get; } = new();
    public MatchDataModel Match { get; } = new();

    public void Update(RootGameData rootGameData)
    {
        Player.Update(rootGameData);
        Match.Update(rootGameData);
    }
}