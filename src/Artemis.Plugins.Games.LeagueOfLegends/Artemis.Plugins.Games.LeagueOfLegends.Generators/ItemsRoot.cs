using System.Collections.Generic;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators
{
    public class ItemInfo
    {
        public string name { get; set; }
    }
    
    public class ItemsRoot
    {
        public string type { get; set; }
        public string format { get; set; }
        public string version { get; set; }
        public Dictionary<string, ItemInfo> Data { get; set; }
    }
}