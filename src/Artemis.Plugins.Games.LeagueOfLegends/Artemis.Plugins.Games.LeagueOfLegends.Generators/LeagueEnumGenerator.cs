using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Artemis.Plugins.Games.LeagueOfLegends.Generators.DataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators;

[Generator]
public class LeagueEnumGenerator : ISourceGenerator
{
    private readonly HttpClient _client = new();
    private Champions _champions;
    private Items _items;
    

    public void Initialize(GeneratorInitializationContext context)
    {
        var versionsString = _client.GetStringAsync("https://ddragon.leagueoflegends.com/api/versions.json").Result;
        var versions = JsonConvert.DeserializeObject<string[]>(versionsString);
        var latestVersion = versions[0];

        var championsJson = _client.GetStringAsync($"https://ddragon.leagueoflegends.com/cdn/{latestVersion}/data/en_US/champion.json").Result;
        var itemsJson = _client.GetStringAsync($"https://ddragon.leagueoflegends.com/cdn/{latestVersion}/data/en_US/item.json").Result;
        _champions = JsonConvert.DeserializeObject<Champions>(championsJson);
        _items = JsonConvert.DeserializeObject<Items>(itemsJson);
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var defaultDefs = new List<EnumDefinition>
        {
            new() { Name = "Unknown", DisplayName = "Unknown", Id  = -1 },
            new() { Name = "None", DisplayName = "None", Id  = 0 }
        };

        var champDefs = defaultDefs.Concat(_champions.Data.Select(ci => new EnumDefinition
        {
            Name = ci.Key,
            // ReSharper disable once StringLiteralTypo
            AlternativeNames = new[] { $"game_character_displayname_{ci.Key}" },
            DisplayName = ci.Value.name,
            Id = int.Parse(ci.Value.key)
        }));
            
        var itemDefs = defaultDefs.Concat(_items.Data.Select(ci => new EnumDefinition
        {
            Name = $"Item{ci.Key}",
            AlternativeNames = new string[] { },
            DisplayName = ci.Value.name,
            Id = int.Parse(ci.Key)
        }));

        context.AddSource("Champion.g.cs", EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Champion", champDefs));
        context.AddSource("Item.g.cs", EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Item", itemDefs));
    }
}