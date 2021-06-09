using System;
using System.Collections.Generic;
using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.GTAV.Model;
using Artemis.Plugins.Games.GTAV.Module.DataModels;

namespace Artemis.Plugins.Games.GTAV.Module
{
    [PluginFeature(Name = "Grand Theft Auto V")]
    public class GTAVModule : Module<GTAVDataModel>
    {
        private readonly IWebServerService _webServerService;

        public override List<IModuleActivationRequirement> ActivationRequirements => new() {new ProcessActivationRequirement("GTA5")};

        public GTAVModule(IWebServerService webServerService)
        {
            _webServerService = webServerService;
        }

        // This is the beginning of your plugin feature's life cycle. Use this instead of a constructor.
        public override void Enable()
        {
            var jsonPluginEndPoint = _webServerService.AddJsonEndPoint<GTAGameState>(this, "Update", UpdateHandler);
            jsonPluginEndPoint.ThrowOnFail = true;
            jsonPluginEndPoint.RequestException += delegate(object sender, EndpointExceptionEventArgs args) { Console.WriteLine(args); };
        }

        // This is the end of your plugin feature's life cycle.
        public override void Disable()
        {
            // Make sure to clean up resources where needed (dispose IDisposables etc.)
        }

        public override void ModuleActivated(bool isOverride)
        {
            // When this gets called your activation requirements have been met and the module will start displaying
        }

        public override void ModuleDeactivated(bool isOverride)
        {
            // When this gets called your activation requirements are no longer met and your module will stop displaying
        }

        public override void Update(double deltaTime)
        {
            // This is where you can add your update logic, this method is called before the profile is updated
        }

        private void UpdateHandler(GTAGameState gameState)
        {
            DataModel.World = gameState.World;
            DataModel.Player = gameState.Player;
            DataModel.Vehicle.ApplyGSI(gameState.Vehicle);
        }
    }
}