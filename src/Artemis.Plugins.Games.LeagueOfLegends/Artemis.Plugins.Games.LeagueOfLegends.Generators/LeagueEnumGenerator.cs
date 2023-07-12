using System;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Artemis.Plugins.Games.LeagueOfLegends.Generators.DataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators;

[Generator]
public class LeagueEnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var championsText = context
            .AdditionalTextsProvider
            .Where(at => at.Path.EndsWith("champion.json", StringComparison.OrdinalIgnoreCase))
            .Select((text, token) => text.GetText(token)?.ToString()).Collect();

        var itemsText = context
            .AdditionalTextsProvider
            .Where(at => at.Path.EndsWith("item.json", StringComparison.OrdinalIgnoreCase))
            .Select((text, token) => text.GetText(token)?.ToString()).Collect();

        context.RegisterSourceOutput(championsText.Combine(itemsText), (spc, args) =>
        {
            var (championsString, itemsString) = args;
            
            var champions = JsonConvert.DeserializeObject<Champions>(championsString[0]);
            var items = JsonConvert.DeserializeObject<Items>(itemsString[0]);
            
            var defaultDefs = new List<EnumDefinition>
            {
                new("Unknown", "Unknown", -1),
                new("None", "None", 0)
            };
            
            var champDefs = defaultDefs.Concat(champions.Data.Select(ci => 
                new EnumDefinition(
                    ci.Key, 
                    ci.Value.name, 
                    int.Parse(ci.Value.key), 
                    $"game_character_displayname_{ci.Key}")
            ));
            
            var itemDefs = defaultDefs.Concat(items.Data.Select(ci =>
                new EnumDefinition(
                    $"Item{ci.Key}",
                    ci.Value.name,
                    int.Parse(ci.Key)
                )
            ));
            
            var championsSource = EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Champion", champDefs);
            var itemsSource = EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Item", itemDefs);
            
            spc.AddSource("Champion.g.cs", championsSource);
            spc.AddSource("Item.g.cs", itemsSource);
        });
    }
}