using System.Collections.Generic;
using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TrackMania.DataModels;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania
{
    [PluginFeature(Name = "TrackMania", Icon = "logo.svg")]
    public class TrackManiaModule : Module<PluginDataModel>
    {
        private SharedProcessMemory<STelemetry> _sharedProcessMemory;

        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new()
        {
            new ProcessActivationRequirement("ManiaPlanet"),
            new ProcessActivationRequirement("ManiaPlanet32"),
            new ProcessActivationRequirement("Trackmania"),
        };

        public override void Enable()
        {
            ActivationRequirementMode = ActivationRequirementType.Any;
        }

        public override void Disable()
        {
        }

        public override void Update(double deltaTime)
        {
            _sharedProcessMemory.Read();
            DataModel.Apply(_sharedProcessMemory.Data);
        }

        public override void ModuleActivated(bool isOverride)
        {
            _sharedProcessMemory = new SharedProcessMemory<STelemetry>(@"Local\ManiaPlanet_Telemetry");
        }

        public override void ModuleDeactivated(bool isOverride)
        {
        }
    }
}