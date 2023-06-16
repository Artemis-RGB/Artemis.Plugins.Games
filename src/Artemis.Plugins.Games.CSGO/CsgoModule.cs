using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.CSGO.DataModels;
using Artemis.Plugins.Games.CSGO.GameDataModels;
using Serilog;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.CSGO;

public class CsgoModule : Module<CsgoDataModel>
{
    private readonly IWebServerService _webServerService;
    private readonly ILogger _logger;
    private RootGameData? gameData;

    public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new();

    public CsgoModule(IWebServerService webServerService, ILogger logger)
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
        // _webServerService.AddStringEndPoint(this, "update", s =>
        // {
        //     _logger.Information(s);
        // });
        _webServerService.AddJsonEndPoint<RootGameData>(this, "update", newGameData =>
        {
            gameData = newGameData;
            DataModel.Data = newGameData;
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

    }

    public override void Update(double deltaTime)
    {

    }
}
