using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Conversions;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels
{
    public class Navigation : ChildDataModel
    {
        public Navigation(TruckSimulatorDataModel root) : base(root)
        {
        }

        [DataModelProperty(Description = "Number of minutes until the driver is due a rest stop.", Affix = "min")]
        public int NextRestStop => Telemetry.nextRestStop;

        [DataModelProperty(Name = "Speed limit (km/h)", Description = "Speed limit of the current road in kilometers-per-hour.", Affix = "km/h")]
        public float SpeedLimitKph => Telemetry.speedLimit < 0f ? 0f : Telemetry.speedLimit.MsToKph();
        [DataModelProperty(Name = "Speed limit (mph)", Description = "Speed limit of the current road in miles-per-hour.", Affix = "mph")]
        public float SpeedLimitMph => Telemetry.speedLimit < 0f ? 0f : Telemetry.speedLimit.MsToMph();

        [DataModelProperty(Name = "Navigation distance (km)", Description = "Distance reamining to the destination on the navigation in kilometers.", Affix = "km")]
        public float NavigationDistanceKm => Telemetry.navigationDistance.MToKm();
        [DataModelProperty(Name = "Navigation distance (mi)", Description = "Distance reamining to the destination on the navigation in miles.", Affix = "mi")]
        public float NavigationDistanceMi => Telemetry.navigationDistance.MToMi();

        [DataModelProperty(Description = "The estimated duration remaining to the destination on the navigation in in-game minutes.", Affix = "min")]
        public float NavigationTime => Telemetry.navigationTime / 60f;

        public double PositionX => Telemetry.truckPosition.position.x;
        public double PositionY => Telemetry.truckPosition.position.y;
        public double PositionZ => Telemetry.truckPosition.position.z;

        [DataModelProperty(Description = "The current heading of the truck. 0° = North, 90° = East, 180° = South, 270° = West.", Affix = "°")]
        public float Heading => (1f - (float)Telemetry.truckPosition.orientation.heading) * 360f; // Heading is [0-1), 0 = north, .25 = west
    }
}
