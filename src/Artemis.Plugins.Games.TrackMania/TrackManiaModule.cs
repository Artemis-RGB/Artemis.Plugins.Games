using System.Collections.Generic;
using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TrackMania.DataModels;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania
{
    [PluginFeature(Name = "TrackMania")]
    public class TrackManiaModule : Module<PluginDataModel>
    {
        private MemoryMappedStructReader<STelemetry> _sharedProcessMemory;

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
            var data = _sharedProcessMemory.Read();
            DataModel.Apply(in data);
        }

        public override void ModuleActivated(bool isOverride)
        {
            if (isOverride)
                return;
            
            _sharedProcessMemory = new MemoryMappedStructReader<STelemetry>(@"Local\ManiaPlanet_Telemetry");
        }

        public override void ModuleDeactivated(bool isOverride)
        {
            _sharedProcessMemory?.Dispose();
            _sharedProcessMemory = null;
        }
    }
}