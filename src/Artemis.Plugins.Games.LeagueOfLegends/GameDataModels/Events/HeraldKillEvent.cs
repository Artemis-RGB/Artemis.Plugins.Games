﻿namespace Artemis.Plugins.Games.LeagueOfLegends.GameDataModels
{
    public class HeraldKillEvent : LolEvent
    {
        public bool Stolen { get; set; }
        public string KillerName { get; set; }
        public string[] Assisters { get; set; }
    }
}
