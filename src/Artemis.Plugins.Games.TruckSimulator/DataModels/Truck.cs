using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Conversions;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Truck
{
    public Engine Engine { get; } = new();
    public Fuel Fuel { get; } = new();
    public BrakeAirPressure AirPressure { get; } = new();
    public Lights Lights { get; } = new();
    public TruckWheels Wheels { get; } = new();
    public Damage Damage { get; } = new();

    // Misc/ungrouped truck properties
    [DataModelProperty(Name = "Speed (km/h)", Description = "Speed of the truck measured in kilometers-per-hour.", Affix = "km/h")]
    public float SpeedKph { get; set; }

    [DataModelProperty(Name = "Speed (mph)", Description = "Speed of the truck measured in miles-per-hour.", Affix = "mph")]
    public float SpeedMph { get; set; }

    [DataModelProperty(Description = "Whether the parking brake (hand brake) is on.")]
    public bool ParkingBrakeOn { get; set; }

    [DataModelProperty(Description = "Whether the motor is on.")]
    public bool MotorBrakeOn { get; set; }

    [DataModelProperty(Name = "Odometer (km)", Description = "The total distance travelled by the current truck in kilometers.", Affix = "km")]
    public float OdometerKm { get; set; }

    [DataModelProperty(Name = "Odometer (mi)", Description = "The total distance travelled by the current truck in miles.", Affix = "mi")]
    public float OdometerMi { get; set; }

    public bool WipersEnabled { get; set; }

    [DataModelProperty(Description = "Manufacturer of the truck the player is driving. E.G. 'Volvo'.")]
    public string Brand { get; set; }

    [DataModelProperty(Description = "Model of the truck the player is driving. E.G. 'FH'.")]
    public string Model { get; set; }

    public bool DashboardBacklightActive { get; set; }

    [DataModelProperty(Description = "Whether the truck's lift axle is currently lifted.")]
    public bool LiftAxle { get; set; }

    [DataModelProperty(Description = "Whether the truck's lift axle indicator light is currently on.")]
    public bool LiftAxleIndicator { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        Engine.Update(in data);
        Fuel.Update(in data);
        AirPressure.Update(in data);
        Lights.Update(in data);
        Wheels.Update(in data);
        Damage.Update(in data);
        SpeedKph = data.speed.MsToKph();
        SpeedMph = data.speed.MsToMph();
        ParkingBrakeOn = data.parkingBrake != 0;
        MotorBrakeOn = data.motorBrake != 0;
        OdometerKm = data.odometer;
        OdometerMi = data.odometer.KmToMi();
        WipersEnabled = data.wipers != 0;
        Brand = data.brand;
        Model = data.modelName;
        DashboardBacklightActive = data.dashboardBacklight != 0;
        LiftAxle = data.liftAxle != 0;
        LiftAxleIndicator = data.liftAxleIndicator != 0;
    }
}