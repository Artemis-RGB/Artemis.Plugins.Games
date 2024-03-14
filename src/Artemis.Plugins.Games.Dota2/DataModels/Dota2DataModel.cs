using System.Collections.Generic;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.Dota2.DataModels.Nodes;

namespace Artemis.Plugins.Games.Dota2.DataModels;

public class Dota2DataModel : DataModel
{
    public ProviderDota2 Provider { get; set; } = new();

    public MapDota2 Map { get; set; } = new();

    public PlayerDota2 Player { get; set; } = new();

    public HeroDota2 Hero { get; set; } = new();

    public IDictionary<string, Ability> Abilities { get; set; } = new Dictionary<string, Ability>();

    public Items Items { get; set; } = new();

    public Dota2DataModel? Previously { get; set; }

    public Dota2DataModel? Added { get; set; }

    public Abilities SortedAbilities { get; set; } = new();
}