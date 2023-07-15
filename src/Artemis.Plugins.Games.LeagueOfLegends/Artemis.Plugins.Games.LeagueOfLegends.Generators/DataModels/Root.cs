using System.Collections.Generic;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators.DataModels;

public class Root<T>
{
    public string Type { get; set; }
    public string Version { get; set; }
    public Dictionary<string, T> Data { get; set; }
}