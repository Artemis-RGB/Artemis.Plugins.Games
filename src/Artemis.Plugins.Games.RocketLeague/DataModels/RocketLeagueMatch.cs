using Artemis.Core.Modules;
using System;

namespace Artemis.Plugins.Games.RocketLeague
{
    public class RocketLeagueMatch
    {
        [DataModelIgnore]
        public int Time { get; set; }
        public TimeSpan TimeRemaining => TimeSpan.FromSeconds(Time);
        public bool Overtime { get; set; }
        public bool UnlimitedTime { get; set; }

        public RocketLeaguePlaylist Playlist { get; set; }

        public RocketLeagueTeam Team_0 { get; set; }
        public RocketLeagueTeam Team_1 { get; set; }
        public RocketLeagueTeam OpponentTeam { get; set; }
        public RocketLeagueTeam MyTeam { get; set; }

        public int TotalGoals => (Team_0?.Goals ?? 0) + (Team_1?.Goals ?? 0);
    }
}