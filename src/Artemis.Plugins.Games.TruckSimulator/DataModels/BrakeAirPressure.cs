using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class BrakeAirPressure
{
    [DataModelProperty(Description = "Current air pressure of the brakes in pound-force per square inch.", Affix = "psi")]
    public float AirPressure { get; set; }

    [DataModelProperty(Description = "Maximum air pressure of the brakes in pound-force per square inch.", Affix = "psi")]
    public float AirPressureMaximum => 150f;

    [DataModelProperty(Description = "Whether the air pressure warning light is current lit.")]
    public bool AirPressureWarningActive { get; set; }
    [DataModelProperty(Description = "Whether the emergency brakes are engaged due to low air pressure.")]
    public bool AirPressureEmergencyActive { get; set; }

    [DataModelProperty(Description = "Amount of air pressure below which the air pressure warning is active.", Affix = "psi")]
    public float AirPressureWarningLevel { get; set; }
    [DataModelProperty(Description = "Amount of air pressure below which the emergency brakes engage.", Affix = "psi")]
    public float AirPressureEmergencyLevel { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        AirPressure = data.airPressure;
        AirPressureWarningActive = data.airPressureWarning != 0;
        AirPressureEmergencyActive = data.airPressureEmergency != 0;
        AirPressureWarningLevel = data.airPressureWarningLevel;
        AirPressureEmergencyLevel = data.airPressureEmergencyLevel;
    }
}