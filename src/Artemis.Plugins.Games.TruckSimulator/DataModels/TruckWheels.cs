using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;
using System.Collections.Generic;
using System.Linq;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class TruckWheels
{
    private readonly TruckWheel[] wheelAccessors;

    public TruckWheels()
    {
        wheelAccessors = new TruckWheel[TruckSimulatorMemoryStruct.WheelCount];
        for (var i = 0; i < TruckSimulatorMemoryStruct.WheelCount; i++)
            wheelAccessors[i] = new TruckWheel(i);
    }

    [DataModelProperty(Description = "Number of wheels on this truck.")]
    public int WheelCount { get; set; }

    [DataModelProperty(Description = "Whether the differential lock is enabled.")]
    public bool DifferentialLock { get; set; }

    [DataModelProperty(Description = "Gets whether the wheels are currently in a lifted state. For trucks without liftable wheels, this is always false.")]
    public bool Lifted { get; set; }

    [DataModelProperty(Description = "Gets details about individual wheels on the tractor. The first wheel in this list is usually the front left, then front right, 2nd-from-front left, 2nd-from-front right, etc.")]
    // Only return as many datamodels are there are wheels on the tractor, e.g. don't return an 11th wheel if there are only 10 on the truck.
    public IEnumerable<TruckWheel> WheelData => wheelAccessors.Take(WheelCount);
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        WheelCount = (int)data.wheelCount;
        DifferentialLock = data.differentialLock != 0;
        foreach (var wheel in wheelAccessors)
            wheel.Update(in data);
        Lifted = wheelAccessors.Any(wheel => wheel.Lifted);
    }
}