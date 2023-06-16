using System.Collections.Generic;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.Dota2.DataModels.Nodes;
using Newtonsoft.Json;

namespace Artemis.Plugins.Games.Dota2.DataModels
{
    [JsonObject]
    public class Dota2DataModel : DataModel
    {
        public ProviderDota2 Provider { get; set; } = null!;
        public MapDota2 Map { get; set; } = null!;
        public PlayerDota2 Player { get; set; } = null!;
        public HeroDota2 Hero { get; set; } = null!;
        public IDictionary<string, Ability> Abilities { get; set; } = new Dictionary<string, Ability>();
        public Items Items { get; set; } = null!;
        public Dota2DataModel? Previously { get; set; }
        public Dota2DataModel? Added { get; set; }

        public Abilities SortedAbilities { get; set; } = new();
    }
}