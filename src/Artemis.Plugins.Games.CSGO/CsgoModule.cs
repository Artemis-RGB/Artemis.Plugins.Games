using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.CSGO.DataModels;
using Artemis.Plugins.Games.CSGO.GameDataModels;
using System.Collections.Generic;
using System.Text.Json;
namespace Artemis.Plugins.Games.CSGO;

public class CsgoModule(IWebServerService webServerService) : Module<CsgoDataModel>
{
    private PluginEndPoint? _endPoint;
    public override List<IModuleActivationRequirement> ActivationRequirements { get; } = [new ProcessActivationRequirement("cs2")];

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        TypeInfoResolverChain = { JsonSourceContext.Default }
    };

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
        _endPoint = webServerService.AddStringEndPoint(this, "update", newGameData =>
        {
            DataModel.Data = JsonSerializer.Deserialize<RootGameData>(newGameData, _jsonSerializerOptions);
            //TODO: this is a placeholder.
            //RootGameData will not change,
            //but we should create a plugin-specific data structure
            //that is easier to use in the UI.
            //we can then convert the RootGameData to our own data structure here.
            //we can also fire events here, and do other things.
        });
    }

    public override void Disable()
    {
        if (_endPoint != null)
        {
            webServerService.RemovePluginEndPoint(_endPoint);
        }
    }

    public override void Update(double deltaTime)
    {

    }
}
