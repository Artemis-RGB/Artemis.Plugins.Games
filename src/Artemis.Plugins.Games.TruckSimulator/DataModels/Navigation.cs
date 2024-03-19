using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Conversions;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Navigation
{
    [DataModelProperty(Description = "Number of minutes until the driver is due a rest stop.", Affix = "min")]
    public int NextRestStop { get; set; }

    [DataModelProperty(Name = "Speed limit (km/h)", Description = "Speed limit of the current road in kilometers-per-hour.", Affix = "km/h")]
    public float SpeedLimitKph { get; set; }
    [DataModelProperty(Name = "Speed limit (mph)", Description = "Speed limit of the current road in miles-per-hour.", Affix = "mph")]
    public float SpeedLimitMph { get; set; }

    [DataModelProperty(Name = "Navigation distance (km)", Description = "Distance reamining to the destination on the navigation in kilometers.", Affix = "km")]
    public float NavigationDistanceKm { get; set; }
    [DataModelProperty(Name = "Navigation distance (mi)", Description = "Distance reamining to the destination on the navigation in miles.", Affix = "mi")]
    public float NavigationDistanceMi { get; set; }

    [DataModelProperty(Description = "The estimated duration remaining to the destination on the navigation in in-game minutes.", Affix = "min")]
    public float NavigationTime { get; set; }

    public double PositionX { get; set; }
    public double PositionY { get; set; }
    public double PositionZ { get; set; }

    [DataModelProperty(Description = "The current heading of the truck. 0° = North, 90° = East, 180° = South, 270° = West.", Affix = "°")]
    public float Heading { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        NextRestStop = data.nextRestStop;
        SpeedLimitKph = data.speedLimit.MsToKph();
        SpeedLimitMph = data.speedLimit.MsToMph();
        NavigationDistanceKm = data.navigationDistance.MToKm();
        NavigationDistanceMi = data.navigationDistance.MToMi();
        NavigationTime = data.navigationTime / 60f;
        PositionX = data.truckPosition.position.x;
        PositionY = data.truckPosition.position.y;
        PositionZ = data.truckPosition.position.z;
        Heading = (1f - (float)data.truckPosition.orientation.heading) * 360f;// Heading is [0-1), 0 = north, .25 = west
    }
}