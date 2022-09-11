using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.Valheim.Module.DataModels;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.Valheim.Module
{
    [PluginFeature(AlwaysEnabled = true, Name = "Valheim")]
    public class ValheimModule : Module<ValheimDataModel>
    {
        private readonly IWebServerService _webServerService;

        private PluginEndPoint _playerUpdateEndpoint;
        private PluginEndPoint _environmentUpdateEndpoint;
        private PluginEndPoint _teleportEventEndpoint;
        private PluginEndPoint _levelUpEventEndpoint;
        private PluginEndPoint _lforsakenEventEventEndpoint;

        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new() { new ProcessActivationRequirement("valheim") };

        public ValheimModule(IWebServerService webServerService)
        {
            _webServerService = webServerService;
        }

        public override void Enable()
        {
            _playerUpdateEndpoint = _webServerService.AddJsonEndPoint<PlayerData>(this, "player", p => DataModel.Player = p);
            _environmentUpdateEndpoint = _webServerService.AddJsonEndPoint<Enviroment>(this, "environment", e => DataModel.Environment = e);
            _teleportEventEndpoint = _webServerService.AddStringEndPoint(this, "teleport", _ => DataModel.Teleport.Trigger());
            _levelUpEventEndpoint = _webServerService.AddJsonEndPoint<SkillLevelUpEventArgs>(this, "levelUp", e => DataModel.SkillLevelUp.Trigger(e));
            _lforsakenEventEventEndpoint = _webServerService.AddStringEndPoint(this, "forsakenActivated", _ => DataModel.ForsakenActivated.Trigger());
        }

        public override void Disable()
        {
            _webServerService.RemovePluginEndPoint(_playerUpdateEndpoint);
            _webServerService.RemovePluginEndPoint(_environmentUpdateEndpoint);
            _webServerService.RemovePluginEndPoint(_teleportEventEndpoint);
            _webServerService.RemovePluginEndPoint(_levelUpEventEndpoint);
            _webServerService.RemovePluginEndPoint(_lforsakenEventEventEndpoint);
        }

        public override void ModuleActivated(bool isOverride)
        {
        }

        public override void ModuleDeactivated(bool isOverride)
        {
        }

        public override void Update(double deltaTime)
        {
        }
    }
}