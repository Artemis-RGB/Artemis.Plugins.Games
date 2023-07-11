using System;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            new("Unknown", "Unknown", -1),
            new("None", "None", 0)
        };

        var champDefs = defaultDefs.Concat(_champions.Data.Select(ci => 
            new EnumDefinition(
                ci.Key, 
                ci.Value.name, 
                int.Parse(ci.Value.key), 
                $"game_character_displayname_{ci.Key}")
        ));

        var itemDefs = defaultDefs.Concat(_items.Data.Select(ci =>
            new EnumDefinition(
                $"Item{ci.Key}",
                ci.Value.name,
                int.Parse(ci.Key)
                )
        ));

        var championsSource = EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Champion", champDefs);
        var itemsSource = EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Item", itemDefs);
        
        context.AddSource("Champion.g.cs", championsSource);
        context.AddSource("Item.g.cs", itemsSource);
    }
}