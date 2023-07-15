using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators.DataModels;

public class RuneInfo
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Name { get; set; }
    public Slot[]? Slots { get; set; }
    
    public IEnumerable<RuneInfo> Children => Slots?.SelectMany(s => s.Runes ?? Enumerable.Empty<RuneInfo>()) ?? Enumerable.Empty<RuneInfo>();
}

public class Slot
{
    public RuneInfo[]? Runes { get; set; }
}