using Artemis.Core.Modules;
using Artemis.Plugins.Modules.TruckSimulator.Conversions;

namespace Artemis.Plugins.Modules.TruckSimulator.DataModels
{
    public class CruiseControl : ChildDataModel
    {
        public CruiseControl(TruckSimulatorDataModel root) : base(root) { }

        public bool CruiseControlActive => Telemetry.cruiseControlActive != 0;
        [DataModelProperty(Name = "Cruise control speed (Km/h)", Description = "The speed the cruise control is set to in kilometers-per-hour.", Affix = "km/h")]
        public float CruiseControlSpeedKph => Telemetry.cruiseControl.MsToKph();
        [DataModelProperty(Name = "Cruise control speed (Mph)", Description = "The speed the cruise control is set to in miles-per-hour.", Affix = "mph")]
        public float CruiseControlSpeedMph => Telemetry.cruiseControl.MsToMph();
    }
}
