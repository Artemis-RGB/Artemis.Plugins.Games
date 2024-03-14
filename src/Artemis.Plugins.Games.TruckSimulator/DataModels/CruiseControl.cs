using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Conversions;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class CruiseControl
{
    public bool CruiseControlActive { get; set; }
    [DataModelProperty(Name = "Cruise control speed (Km/h)", Description = "The speed the cruise control is set to in kilometers-per-hour.", Affix = "km/h")]
    public float CruiseControlSpeedKph { get; set; }
    [DataModelProperty(Name = "Cruise control speed (Mph)", Description = "The speed the cruise control is set to in miles-per-hour.", Affix = "mph")]
    public float CruiseControlSpeedMph { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        CruiseControlActive = data.cruiseControlActive != 0;
        CruiseControlSpeedKph = data.cruiseControl.MsToKph();
        CruiseControlSpeedMph = data.cruiseControl.MsToMph();
    }
}