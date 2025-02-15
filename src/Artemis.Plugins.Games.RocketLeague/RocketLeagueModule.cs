using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Core;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.RocketLeague
{
    [PluginFeature(AlwaysEnabled = true, Name = "Rocket League")]
    public class RocketLeagueModule : Module<RocketLeagueDataModel>
    {
        private readonly IWebServerService _webServerService;

        private JsonPluginEndPoint<RocketLeagueDataModel> _updateEndpoint;

        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new() { new ProcessActivationRequirement("RocketLeague") };

        public RocketLeagueModule(IWebServerService webServerService)
        {
            _webServerService = webServerService;
        }

        public override void Enable()
        {
            _updateEndpoint = _webServerService.AddJsonEndPoint<RocketLeagueDataModel>(this, "update", OnProcessedRequest);
        }

        private void OnProcessedRequest(RocketLeagueDataModel data)
        {
            DataModel.Match = data.Match;
            DataModel.Player = data.Player;
            DataModel.Car = data.Car;
            DataModel.Status = data.Status;
            
            if (DataModel.Player == null)
            {
                DataModel.Match.MyTeam = null;
                DataModel.Match.OpponentTeam = null;
                return;
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