using System.Collections.Generic;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators.DataModels;

public class Champions
{
    public string type { get; set; }
    public string format { get; set; }
    public string version { get; set; }
    public Dictionary<string, ChampionInfo> Data { get; set; }
}