using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators
{
    [Generator]
    public class LeagueEnumGenerator : ISourceGenerator
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly Dictionary<string, ChampInfo> _champions = new Dictionary<string, ChampInfo>();
        private readonly Dictionary<string, ItemInfo> _items = new Dictionary<string, ItemInfo>();

        public void Initialize(GeneratorInitializationContext context)
        {
            var versionsString = _client.GetStringAsync("https://ddragon.leagueoflegends.com/api/versions.json").Result;
            string[] versions = JsonConvert.DeserializeObject<string[]>(versionsString);
            var latestVersion = versions[0];

            var championsJson = _client.GetStringAsync($"https://ddragon.leagueoflegends.com/cdn/{latestVersion}/data/en_US/champion.json").Result;
            var itemsJson = _client.GetStringAsync($"https://ddragon.leagueoflegends.com/cdn/{latestVersion}/data/en_US/item.json").Result;
            var champions = JsonConvert.DeserializeObject<ChampionsRoot>(championsJson);
            var items = JsonConvert.DeserializeObject<ItemsRoot>(itemsJson);
            
            foreach (var item in champions.Data)
            {
                _champions.Add(item.Key, item.Value);
            }
            
            foreach (var item in items.Data)
            {
                _items.Add(item.Key, item.Value);
            }
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var defaultDefs = new List<EnumDefinition>
            {
                new EnumDefinition { Name = "Unknown", DisplayName = "Unknown", Id  = -1 },
                new EnumDefinition { Name = "None", DisplayName = "None", Id  = 0 }
            };

            var champDefs = _champions.Select(ci => new EnumDefinition
            {
                Name = ci.Key,
                AlternativeNames = new string[] { $"game_character_displayname_{ci.Key}" },
                DisplayName = ci.Value.name,
                Id = int.Parse(ci.Value.key)
            });
            
            var itemDefs = _items.Select(ci => new EnumDefinition
            {
                Name = $"Item{ci.Key}",
                AlternativeNames = new string[] { },
                DisplayName = ci.Value.name,
                Id = int.Parse(ci.Key)
            });

            context.AddSource("Champion.g.cs", EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Champion", defaultDefs.Concat(champDefs)));
            context.AddSource("Item.g.cs", EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Item", defaultDefs.Concat(itemDefs)));
        }
    }
}
