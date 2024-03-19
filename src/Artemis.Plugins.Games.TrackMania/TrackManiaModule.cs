using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TrackMania.DataModels;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania
{
    [PluginFeature(Name = "TrackMania")]
    public class TrackManiaModule : Module<PluginDataModel>
    {
        private uint _telemetryVersion;
        private MemoryMappedStructReader<STelemetryV3> _sharedProcessMemoryV3;
        private MemoryMappedStructReader<STelemetryV2> _sharedProcessMemoryV2;

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
            switch (_telemetryVersion)
            {
                case 2:
                    DataModel.Apply(_sharedProcessMemoryV2.Read());
                    break;
                case 3:
                    DataModel.Apply(_sharedProcessMemoryV3.Read());
                    break;
                default:
                    break;
            }
        }

        public override void ModuleActivated(bool isOverride)
        {
            if (isOverride)
                return;

            _telemetryVersion = GetTelemetryVersion();

            switch (_telemetryVersion)
            {
                case 2:
                    _sharedProcessMemoryV2 = new MemoryMappedStructReader<STelemetryV2>(@"Local\ManiaPlanet_Telemetry");
                    break;
                case 3:
                    _sharedProcessMemoryV3 = new MemoryMappedStructReader<STelemetryV3>(@"Local\ManiaPlanet_Telemetry");
                    break;
                default:
                    break;
            }
        }

        public override void ModuleDeactivated(bool isOverride)
        {
            _sharedProcessMemoryV2?.Dispose();
            _sharedProcessMemoryV2 = null;
            _sharedProcessMemoryV3?.Dispose();
            _sharedProcessMemoryV3 = null;
        }

        private static uint GetTelemetryVersion()
        {
            using var headerReader = new MemoryMappedStructReader<SHeader>(@"Local\ManiaPlanet_Telemetry");
            var header = headerReader.Read();
            return header.Version;
        }
    }
}