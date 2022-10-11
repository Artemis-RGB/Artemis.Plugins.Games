using System;
using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.Dota2.DataModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using Artemis.Plugins.Games.Dota2.DataModels.Nodes;
using EmbedIO;
using Newtonsoft.Json;
using Serilog;

namespace Artemis.Plugins.Games.Dota2;

[PluginFeature(Name = "Dota 2")]
public class Dota2Module : Module<Dota2DataModel>
{
    private readonly IWebServerService _webServerService;
    private readonly ILogger _logger;
    private DataModelJsonPluginEndPoint<Dota2DataModel>? _endPoint;

    public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new();

    public Dota2Module(IWebServerService webServerService, ILogger logger)
    {
        _webServerService = webServerService;
        _logger = logger;
    }

    public override void ModuleActivated(bool isOverride)
    {

    }

    public override void ModuleDeactivated(bool isOverride)
    {

    }

    public override void Enable()
    {
        _endPoint = _webServerService.AddDataModelJsonEndPoint(this, "dota2data");
        _endPoint.ProcessedRequest += _endpoint_ProcessedRequest;
    }

    public override void Disable()
    {
        if (_endPoint == null)
        {
            return;
        }
        _endPoint.ProcessedRequest -= _endpoint_ProcessedRequest;
        _webServerService.RemovePluginEndPoint(_endPoint);
        _endPoint = null;
    }

    public override void Update(double deltaTime)
    {

    }

    private readonly JsonSerializer _json = new();
    private async void _endpoint_ProcessedRequest(object? sender, EndpointRequestEventArgs e)
    {
        FilterAbilities(DataModel.Abilities);

        try
        {
            using var sr = new StreamReader(e.Context.Response.OutputStream);
            using var jReader = new JsonTextReader(sr);
            var jsObj = _json.Deserialize<JsonObject>(jReader);
            _logger.Information("JsonObject: {jr}", jsObj.ToJsonString());
            _logger.Information("res: {R}", jsObj["abilities"].ToJsonString());
        }
        catch (Exception exception)
        {
            _logger.Error(exception, "exception: ");
        }
    }

    private void FilterAbilities(IDictionary<string, Ability> abilities)
    {
        var index = 1;
        foreach (var b in abilities.Where(pair => !(pair.Value.Name.StartsWith("seasonal_") || pair.Value.Name.StartsWith("plus_"))))
        {
            switch (index++)
            {
                case 1:
                    DataModel.SortedAbilities.Ability1 = b.Value;
                    break;
                case 2:
                    DataModel.SortedAbilities.Ability2 = b.Value;
                    break;
                case 3:
                    DataModel.SortedAbilities.Ability3 = b.Value;
                    break;
                case 4:
                    DataModel.SortedAbilities.UltimateAbility = b.Value;
                    break;
                case 5:
                    DataModel.SortedAbilities.Ability4 = b.Value;
                    break;
                case 6:
                    DataModel.SortedAbilities.Ability5 = b.Value;
                    break;
            }
        }

        for (; index < 7; index++)
        {
            switch (index++)
            {
                case 1:
                    DataModel.SortedAbilities.Ability1 = new Ability();
                    break;
                case 2:
                    DataModel.SortedAbilities.Ability2 = new Ability();
                    break;
                case 3:
                    DataModel.SortedAbilities.Ability3 = new Ability();
                    break;
                case 4:
                    DataModel.SortedAbilities.UltimateAbility = new Ability();
                    break;
                case 5:
                    DataModel.SortedAbilities.Ability4 = new Ability();
                    break;
                case 6:
                    DataModel.SortedAbilities.Ability5 = new Ability();
                    break;
            }
        }
    }
}