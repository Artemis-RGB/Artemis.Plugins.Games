using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.Dota2.DataModels;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.Dota2
{
    public class Dota2Module : Module<Dota2DataModel>
    {
        private readonly IWebServerService _webServerService;

        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new();

        public Dota2Module(IWebServerService webServerService)
        {
            _webServerService = webServerService;
        }

        public override void ModuleActivated(bool isOverride)
        {

        }

        public override void ModuleDeactivated(bool isOverride)
        {

        }

        public override void Enable()
        {
            _webServerService.AddDataModelJsonEndPoint(this, "update");
        }

        public override void Disable()
        {

        }

        public override void Update(double deltaTime)
        {

        }
    }
}