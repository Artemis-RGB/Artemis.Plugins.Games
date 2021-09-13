﻿using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.EventArgs
{
    public class TurretKillEventArgs : DataModelEventArgs
    {
        public string KillerName { get; set; }
        public Turret TurretKilled { get; set; }
        public string[] Assisters { get; set; }
    }
}
