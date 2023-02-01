using System.Collections.Generic;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators
{
    public class ChampInfo
    {
        public string version { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string title { get; set; }
    }
    
    public class ChampionsRoot
    {
        public string type { get; set; }
        public string format { get; set; }
        public string version { get; set; }
        public Dictionary<string, ChampInfo> Data { get; set; }
    }
}