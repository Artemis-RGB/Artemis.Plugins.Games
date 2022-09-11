using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Core;
using SkiaSharp;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.RocketLeague
{
    [PluginFeature(AlwaysEnabled = true, Name = "Rocket League")]
    public class RocketLeagueModule : Module<RocketLeagueDataModel>
    {
        private readonly IWebServerService _webServerService;

        private DataModelJsonPluginEndPoint<RocketLeagueDataModel> _updateEndpoint;

        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new() { new ProcessActivationRequirement("RocketLeague") };

        public RocketLeagueModule(IWebServerService webServerService)
        {
            _webServerService = webServerService;
        }

        public override void Enable()
        {
            _updateEndpoint = _webServerService.AddDataModelJsonEndPoint(this, "update");
            _updateEndpoint.ProcessedRequest += OnProcessedRequest;
        }

        private void OnProcessedRequest(object sender, EndpointRequestEventArgs e)
        {
            if (DataModel.Player == null)
            {
                DataModel.Match.MyTeam = null;
                DataModel.Match.OpponentTeam = null;
            }

            if (DataModel.Player.Team == 0)
            {
                DataModel.Match.MyTeam = DataModel.Match.Team_0;
                DataModel.Match.OpponentTeam = DataModel.Match.Team_1;
            }
            else
            {
                DataModel.Match.MyTeam = DataModel.Match.Team_1;
                DataModel.Match.OpponentTeam = DataModel.Match.Team_0;
            }
        }

        public override void Disable()
        {
            _updateEndpoint.ProcessedRequest -= OnProcessedRequest;
            _webServerService.RemovePluginEndPoint(_updateEndpoint);
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