namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels.Events;

public class GameEndEvent : LolEvent
{
    public string Result { get; set; }
}