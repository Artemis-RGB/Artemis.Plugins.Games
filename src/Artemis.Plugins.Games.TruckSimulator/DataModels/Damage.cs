using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Damage
{
    [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
    public float Engine { get; set; }

    [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
    public float Transmission { get; set; }

    [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
    public float Cabin { get; set; }

    [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
    public float Chassis { get; set; }

    [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
    public float Wheels { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        Engine = data.wearEngine;
        Transmission = data.wearTransmission;
        Cabin = data.wearCabin;
        Chassis = data.wearChassis;
        Wheels = data.wearWheelsAvg;
    }
}