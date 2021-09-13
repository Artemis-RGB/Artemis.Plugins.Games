using System;

namespace Artemis.Plugins.Games.LeagueOfLegends.GameDataModels
{
    public class EventList
    {
        public LolEvent[] Events { get; set; } = Array.Empty<LolEvent>();
    }
}
