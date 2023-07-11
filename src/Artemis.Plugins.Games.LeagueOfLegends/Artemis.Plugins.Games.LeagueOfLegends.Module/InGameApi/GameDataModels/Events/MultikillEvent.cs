namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels.Events;

public class MultikillEvent : LolEvent
{
    public string KillerName { get; set; }
    public int KillStreak { get; set; }
}