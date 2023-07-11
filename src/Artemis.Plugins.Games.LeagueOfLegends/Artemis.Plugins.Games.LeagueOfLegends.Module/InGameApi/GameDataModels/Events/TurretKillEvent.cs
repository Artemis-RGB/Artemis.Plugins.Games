namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels.Events;

public class TurretKillEvent : LolEvent
{
    public string KillerName { get; set; }
    public string TurretKilled { get; set; }
    public string[] Assisters { get; set; }
}