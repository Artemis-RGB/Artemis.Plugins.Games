using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.Dota2.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Artemis.Plugins.Games.Dota2.DataModels.Nodes;

namespace Artemis.Plugins.Games.Dota2;

[PluginFeature(Name = "Dota 2")]
public class Dota2Module(IWebServerService webServerService) : Module<Dota2DataModel>
{
    private PluginEndPoint? _endPoint;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        TypeInfoResolverChain = { JsonSourceContext.Default }
    };

    public override List<IModuleActivationRequirement> ActivationRequirements { get; } = [new ProcessActivationRequirement("dota2")];

    public override void ModuleActivated(bool isOverride)
    {
        //unused
    }

    public override void ModuleDeactivated(bool isOverride)
    {
        //unused
    }

    public override void Enable()
    {
        _endPoint = webServerService.AddStringEndPoint(this, "dota2data", ProcessJson);
    }

    private void ProcessJson(string obj)
    {
        var newData = JsonSerializer.Deserialize<Dota2DataModel>(obj, _jsonSerializerOptions);

        if (newData == null)
        {
            return;
        }

        DataModel.Provider = newData.Provider;
        DataModel.Map = newData.Map;
        DataModel.Player = newData.Player;
        DataModel.Hero = newData.Hero;
        DataModel.Abilities = newData.Abilities;
        DataModel.Items = newData.Items;
        DataModel.Previously = newData.Previously;
        DataModel.Added = newData.Added;
        
        FilterAbilities(DataModel.Abilities);
    }

    public override void Disable()
    {
        if (_endPoint == null)
        {
            return;
        }
        webServerService.RemovePluginEndPoint(_endPoint);
        _endPoint = null;
    }

    public override void Update(double deltaTime)
    {

    }

    private void FilterAbilities(IDictionary<string, Ability> abilities)
    {
        var index = 1;
        foreach (var ability in abilities.Where(IsGameAbility).Select(p => p.Value))
        {
            switch (index++)
            {
                case 1:
                    DataModel.SortedAbilities.Ability1 = ability;
                    break;
                case 2:
                    DataModel.SortedAbilities.Ability2 = ability;
                    break;
                case 3:
                    DataModel.SortedAbilities.Ability3 = ability;
                    break;
                case 4:
                    DataModel.SortedAbilities.UltimateAbility = ability;
                    break;
                case 5:
                    DataModel.SortedAbilities.Ability4 = ability;
                    break;
                case 6:
                    DataModel.SortedAbilities.Ability5 = ability;
                    break;
            }
        }

        for (; index < 7; index++)
        {
            switch (index++)
            {
                case 1:
                    DataModel.SortedAbilities.Ability1 = Ability.EmptyAbility;
                    break;
                case 2:
                    DataModel.SortedAbilities.Ability2 = Ability.EmptyAbility;
                    break;
                case 3:
                    DataModel.SortedAbilities.Ability3 = Ability.EmptyAbility;
                    break;
                case 4:
                    DataModel.SortedAbilities.UltimateAbility = Ability.EmptyAbility;
                    break;
                case 5:
                    DataModel.SortedAbilities.Ability4 = Ability.EmptyAbility;
                    break;
                case 6:
                    DataModel.SortedAbilities.Ability5 = Ability.EmptyAbility;
                    break;
            }
        }
    }

    private static bool IsGameAbility(KeyValuePair<string, Ability> pair)
    {
        return !(pair.Value.Name.StartsWith("seasonal_") || pair.Value.Name.StartsWith("plus_"));
    }
}