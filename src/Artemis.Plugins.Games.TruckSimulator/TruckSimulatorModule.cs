using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.DataModels;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.TruckSimulator
{
    [PluginFeature(Name = "Truck Simulator (ETS2 & ATS)", Description = "Module for providing data from Euro and American Truck Simulator", Icon = "Truck")]
    public class TruckSimulatorModule : Module<TruckSimulatorDataModel>
    {
        private MappedFileReader<TruckSimulatorMemoryStruct> mappedFileReader;

        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new()
        {
            new ProcessActivationRequirement("eurotrucks2"),
            new ProcessActivationRequirement("amtrucks")
        };

        public override void Enable()
        {
            mappedFileReader = new MappedFileReader<TruckSimulatorMemoryStruct>(@"Local\SCSTelemetry");
        }

        public override void Update(double deltaTime)
        {
            if (!IsActivatedOverride && DataModel != null)
                DataModel.Telemetry = mappedFileReader?.Read() ?? default;
        }

        public override void Disable()
        {
            mappedFileReader?.Dispose();
            mappedFileReader = null;
        }
    }
}
