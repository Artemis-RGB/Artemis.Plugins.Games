using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

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

    [Generator]
    public class ChampionEnumGenerator : ISourceGenerator
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly Dictionary<string, ChampInfo> _champions = new Dictionary<string, ChampInfo>();

        public void Initialize(GeneratorInitializationContext context)
        {
            var versionsString = _client.GetStringAsync("https://ddragon.leagueoflegends.com/api/versions.json").Result;
            string[] versions = JsonConvert.DeserializeObject<string[]>(versionsString);
            var latestVersion = versions[0];

            var championsJson = _client.GetStringAsync($"https://ddragon.leagueoflegends.com/cdn/{latestVersion}/data/en_US/champion.json").Result;
            var champions = JsonConvert.DeserializeObject<ChampionsRoot>(championsJson);
            foreach (var item in champions.Data)
            {
                _champions.Add(item.Key, item.Value);
            }
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var defaultDefs = new List<EnumDefinition>
            {
                new EnumDefinition { Name = "Unknown", DisplayName = "Unknown", Id  = -1 },
                new EnumDefinition { Name = "None", DisplayName = "None", Id  = 0 }
            };

            var defs = _champions.Select(ci => new EnumDefinition
            {
                Name = ci.Key,
                AlternativeNames = new string[] { $"game_character_displayname_{ci.Key}" },
                DisplayName = ci.Value.name,
                Id = int.Parse(ci.Value.key)
            });

            context.AddSource("Champion.gen.cs", EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums", "Champion", defaultDefs.Concat(defs)));
        }
    }
}
