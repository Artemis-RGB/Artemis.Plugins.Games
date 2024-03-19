using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.DataModels;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.TruckSimulator;

[PluginFeature(Name = "Truck Simulator (ETS2 & ATS)", Description = "Module for providing data from Euro and American Truck Simulator", Icon = "Truck")]
public class TruckSimulatorModule : Module<TruckSimulatorDataModel>
{
    private MemoryMappedStructReader<TruckSimulatorMemoryStruct> mappedFileReader;

    public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new()
    {
        new ProcessActivationRequirement("eurotrucks2"),
        new ProcessActivationRequirement("amtrucks")
    };

    public TruckSimulatorModule()
    {
        UpdateDuringActivationOverride = false;
    }

    public override void ModuleActivated(bool isOverride)
    {
        mappedFileReader = new MemoryMappedStructReader<TruckSimulatorMemoryStruct>(@"Local\SCSTelemetry");
    }

    public override void ModuleDeactivated(bool isOverride)
    {
        mappedFileReader?.Dispose();
        mappedFileReader = null;
    }

    public override void Update(double deltaTime)
    {
        var data = mappedFileReader?.Read() ?? default;
        DataModel.Update(in data);
    }

    public override void Enable()
    {
    }
        
    public override void Disable()
    {

    }
}