﻿using Artemis.Core;
using Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels.EventArgs
{
    public class InhibKillEventArgs : DataModelEventArgs
    {
        public string KillerName { get; set; }
        public Inhibitor InhibKilled { get; set; }
        public string[] Assisters { get; set; }
    }
}
