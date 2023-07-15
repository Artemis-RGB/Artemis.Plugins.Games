using System;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Artemis.Plugins.Games.LeagueOfLegends.Generators.DataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators;

[Generator]
public class LeagueEnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var defaultDefs = new List<EnumDefinition>
        {
            new("Unknown", "Unknown", -1),
            new("None", "None", 0)
        };

        var championsText = GetValueProvider(context, "champion.json");
        var itemsText = GetValueProvider(context, "item.json");
        var mapsText = GetValueProvider(context, "map.json");
        var runesText = GetValueProvider(context, "runesReforged.json");

        context.RegisterSourceOutput(championsText, (spc, args) =>
        {
            if (args.Length == 0)
            {
                var diagnostics =
                    Diagnostic.Create(
                        new DiagnosticDescriptor(
                            "LOL001",
                            "League of Legends generator",
                            "No champion data found",
                            "League of Legends",
                            DiagnosticSeverity.Error,
                            true
                        ),
                        Location.None
                    );
                
                spc.ReportDiagnostic(diagnostics);
                return;
            }
            
            var championsString = args[0];
            var champions = JsonConvert.DeserializeObject<Root<ChampionInfo>>(championsString);

            var champDefs = defaultDefs.Concat(champions.Data.Select(ci =>
                new EnumDefinition(
                    ci.Value.Id,
                    ci.Value.Name,
                    int.Parse(ci.Value.Key),
                    $"game_character_displayname_{ci.Value.Id}")
            ));

            var championsSource =
                EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Champion", champDefs);
            spc.AddSource("Champion.g.cs", championsSource);
        });

        context.RegisterSourceOutput(itemsText, (spc, args) =>
        {
            if (args.Length == 0)
            {
                var diagnostics =
                    Diagnostic.Create(
                        new DiagnosticDescriptor(
                            "LOL002",
                            "League of Legends generator",
                            "No item data found",
                            "League of Legends",
                            DiagnosticSeverity.Error,
                            true
                        ),
                        Location.None
                    );
                
                spc.ReportDiagnostic(diagnostics);
                return;
            }
            
            var itemsString = args[0];
            var items = JsonConvert.DeserializeObject<Root<ItemInfo>>(itemsString);

            var itemDefs = defaultDefs.Concat(items.Data.Select(ci =>
                new EnumDefinition(
                    $"Item{ci.Key}",
                    CleanupName(ci.Value.Name),
                    int.Parse(ci.Key)
                )
            ));

            var itemsSource = EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Item", itemDefs);
            spc.AddSource("Item.g.cs", itemsSource);
        });

        context.RegisterSourceOutput(mapsText, (spc, args) =>
        {
            if (args.Length == 0)
            {
                var diagnostics =
                    Diagnostic.Create(
                        new DiagnosticDescriptor(
                            "LOL003",
                            "League of Legends generator",
                            "No map data found",
                            "League of Legends",
                            DiagnosticSeverity.Error,
                            true
                        ),
                        Location.None
                    );
                
                spc.ReportDiagnostic(diagnostics);
                return;
            }
            
            
            var mapsString = args[0];
            var maps = JsonConvert.DeserializeObject<Root<MapInfo>>(mapsString);

            var mapDefs = maps.Data.Select(m => new EnumDefinition(
                $"Map{m.Value.MapId}",
                m.Value.MapName,
                int.Parse(m.Value.MapId)
            ));

            var mapsSource = EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Map", mapDefs);
            spc.AddSource("Map.g.cs", mapsSource);
        });

        context.RegisterSourceOutput(runesText, (spc, args) =>
        {
            if (args.Length == 0)
            {
                var diagnostics =
                    Diagnostic.Create(
                        new DiagnosticDescriptor(
                            "LOL004",
                            "League of Legends generator",
                            "No rune data found",
                            "League of Legends",
                            DiagnosticSeverity.Error,
                            true
                        ),
                        Location.None
                    );
                
                spc.ReportDiagnostic(diagnostics);
                return;
            }
            
            
            var runesString = args[0];
            var runes = JsonConvert.DeserializeObject<RuneInfo[]>(runesString);

            var runeDefs = defaultDefs.Concat(
                runes.Concat(runes.SelectMany(r => r.Children))
                    .OrderBy(r => r.Id)
                    .Select(r => new EnumDefinition(
                        $"Rune{r.Id}",
                        r.Name,
                        r.Id
                    )));

            var runesSource = EnumGenerator.GetEnum("Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums", "Rune", runeDefs);
            spc.AddSource("RuneInfo.g.cs", runesSource);
        });
    }

    private static IncrementalValueProvider<ImmutableArray<string>> GetValueProvider(IncrementalGeneratorInitializationContext context,
        string searchPattern)
    {
        return context
            .AdditionalTextsProvider
            .Where(at => at.Path.EndsWith(searchPattern, StringComparison.OrdinalIgnoreCase))
            .Select((text, token) => text.GetText(token)?.ToString()).Collect();
    }

    private static readonly Regex NameRegex = new(@"<[^>]+>([^<]+)");
    private static string CleanupName(string dirtyName)
    {
        var match = NameRegex.Match(dirtyName);
        return !match.Success ? dirtyName : match.Groups[1].Value;
    }
}