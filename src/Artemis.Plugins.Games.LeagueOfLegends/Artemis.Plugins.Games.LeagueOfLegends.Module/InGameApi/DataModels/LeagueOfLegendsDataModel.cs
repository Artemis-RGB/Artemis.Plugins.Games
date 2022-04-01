using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels
{
    public class LeagueOfLegendsDataModel : DataModel
    {
        public PlayerDataModel Player { get; } = new();
        public MatchDataModel Match { get; } = new();

        public void Apply(RootGameData rootGameData)
        {
            Player.Apply(rootGameData);
            Match.Apply(rootGameData);
        }
    }
}
