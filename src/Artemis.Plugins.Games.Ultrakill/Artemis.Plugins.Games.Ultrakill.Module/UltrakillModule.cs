using System;
using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.Ultrakill.Module.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace Artemis.Plugins.Games.Ultrakill.Module
{
    [PluginFeature(AlwaysEnabled = true)]
    public class UltrakillModule : Module<UltrakillDataModel>
    {
        private readonly IWebServerService _webServerService;
        private readonly ILogger _logger;
        private DataModelJsonPluginEndPoint<UltrakillDataModel>? _updateEndpoint;

        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new()
        {
            new ProcessActivationRequirement("ULTRAKILL")
        };

        public UltrakillModule(IWebServerService webServerService, ILogger logger)
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
            _updateEndpoint = _webServerService.AddDataModelJsonEndPoint(this, "update");
        }

        public override void Disable()
        {
            _webServerService.RemovePluginEndPoint(_updateEndpoint!);
        }

        public override void Update(double deltaTime)
        {

        }
    }
}