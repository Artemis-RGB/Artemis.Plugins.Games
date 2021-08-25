using Artemis.Core.Modules;
using Artemis.Plugins.Modules.TruckSimulator.Conversions;
using System;

namespace Artemis.Plugins.Modules.TruckSimulator.DataModels
{
    public class Truck : ChildDataModel
    {
        public Truck(TruckSimulatorDataModel root) : base(root)
        {
            Engine = new Engine(root);
            Fuel = new Fuel(root);
            AirPressure = new BrakeAirPressure(root);
            Lights = new Lights(root);
            Wheels = new TruckWheels(root);
            Damage = new Damage(root);
        }

        public Engine Engine { get; }
        public Fuel Fuel { get; }
        public BrakeAirPressure AirPressure { get; }
        public Lights Lights { get; }
        public TruckWheels Wheels { get; }
        public Damage Damage { get; }

        // Misc/ungrouped truck properties
        public bool IgnitionOn => Telemetry.electricEnabled != 0;
        public bool EngineOn => Telemetry.engineEnabled != 0;

        [DataModelProperty(Name = "Speed (km/h)", Description = "Speed of the truck measured in kilometers-per-hour.", Affix = "km/h")]
        public float SpeedKph => Telemetry.speed.MsToKph();
        [DataModelProperty(Name = "Speed (mph)", Description = "Speed of the truck measured in miles-per-hour.", Affix = "mph")]
        public float SpeedMph => Telemetry.speed.MsToMph();

        [DataModelProperty(Description = "Whether the parking brake (hand brake) is on.")]
        public bool ParkingBrakeOn => Telemetry.parkingBrake != 0;
        [DataModelProperty(Description = "Whether the motor is on.")]
        public bool MotorBrakeOn => Telemetry.motorBrake != 0;

        [DataModelProperty(Name = "Odometer (km)", Description = "The total distance travelled by the current truck in kilometers.", Affix = "km")]
        public float OdometerKm => Telemetry.odometer;
        [DataModelProperty(Name = "Odometer (mi)", Description = "The total distance travelled by the current truck in miles.", Affix = "mi")]
        public float OdometerMi => Telemetry.odometer.KmToMi();

        public bool WipersEnabled => Telemetry.wipers != 0;

        [DataModelProperty(Description = "Manufacturer of the truck the player is driving. E.G. 'Volvo'.")]
        public string Brand => Telemetry.brand;
        [DataModelProperty(Description = "Model of the truck the player is driving. E.G. 'FH'.")]
        public string Model => Telemetry.modelName;

        public bool DashboardBacklightActive => Telemetry.dashboardBacklight != 0;
    }


    public class Engine : ChildDataModel
    {
        public Engine(TruckSimulatorDataModel root) : base(root) { }

        public float RPM => Telemetry.engineRpm;
        public float MaximumRPM => Telemetry.engineRpmMax;

        public int Gear => Telemetry.selectedGear;
        public int GearDashboard => Telemetry.dashboardGear;
        public uint HShifterSlot => Telemetry.hShifterSlot;
        public ShifterType ShifterType => Enum.TryParse<ShifterType>(Telemetry.shifterType, ignoreCase: true, out var result) ? result : ShifterType.Unknown;

        public uint ForwardGearCount => Telemetry.forwardGearCount;
        public uint ReverseGearCount => Telemetry.reverseGearCount;

        public uint RetarderLevel => Telemetry.retarderLevel;
        public uint RetarderStepCount => Telemetry.retarderStepCount;
    }


    public class Fuel : ChildDataModel
    {
        public Fuel(TruckSimulatorDataModel root) : base(root) { }

        [DataModelProperty(Description = "Total fuel tank capacity of the truck in litres.", Affix = "L")]
        public float Capacity => Telemetry.fuelCapacity;

        [DataModelProperty(Description = "Current fuel amount stored in the truck's tanks in litres.", Affix = "L")]
        public float Amount => Telemetry.fuel;

        [DataModelProperty(Description = "Average consumption of fuel measured in liters per kilometer.", Affix = "L/km")]
        public float AverageConsumption => Telemetry.fuelAvgConsumption;

        [DataModelProperty(Description = "Estimated distance that can be travelled with remaining fuel in kilometers.", Affix = "km")]
        public float Range => Telemetry.fuelRange;

        [DataModelProperty(Description = "Whether the fuel warning light is current lit.")]
        public bool FuelWarningActive => Telemetry.fuelWarning != 0;

        [DataModelProperty(Description = "The fraction of fuel in the tank below which the fuel warning is active.")]
        public float FuelWarningLevel => Telemetry.fuelWarningLevel;

        [DataModelProperty(Description = "Whether fuel is actively being pumped into the vehicle.")]
        public bool Refueling => Telemetry.refuel != 0;
    }


    public class BrakeAirPressure : ChildDataModel
    {
        public BrakeAirPressure(TruckSimulatorDataModel root) : base(root) { }

        [DataModelProperty(Description = "Current air pressure of the brakes in pound-force per square inch.", Affix = "psi")]
        public float AirPressure => Telemetry.airPressure;

        [DataModelProperty(Description = "Maximum air pressure of the brakes in pound-force per square inch.", Affix = "psi")]
        public float AirPressureMaximum => 150f;

        [DataModelProperty(Description = "Whether the air pressure warning light is current lit.")]
        public bool AirPressureWarningActive => Telemetry.airPressureWarning != 0;
        [DataModelProperty(Description = "Whether the emergency brakes are engaged due to low air pressure.")]
        public bool AirPressureEmergencyActive => Telemetry.airPressureEmergency != 0;

        [DataModelProperty(Description = "Amount of air pressure below which the air pressure warning is active.", Affix = "psi")]
        public float AirPressureWarningLevel => Telemetry.airPressureWarningLevel;
        [DataModelProperty(Description = "Amount of air pressure below which the emergency brakes engage.", Affix = "psi")]
        public float AirPressureEmergencyLevel => Telemetry.airPressureEmergencyLevel;
    }


    public class Lights : ChildDataModel
    {
        public Lights(TruckSimulatorDataModel root) : base(root) { }

        public bool Parking => Telemetry.parkingLights != 0;
        public bool LowBeam => Telemetry.lowBeamLights != 0;
        public bool HighBeam => Telemetry.highBeamLights != 0;

        public bool FrontAuxiliary => Telemetry.lightsAuxFront != 0;
        public bool RoofAuxiliary => Telemetry.lightsAuxRoof != 0;

        [DataModelProperty(Description = "Whether the left blinker lights (as seen from outside the truck) are on. This may be due to the left indicator being active, or the harzard lights being on.")]
        public bool LeftIndicatorLightOn => Telemetry.blinkerLeftOn != 0;
        [DataModelProperty(Description = "Whether the right blinker lights (as seen from outside the truck) are on. This may be due to the right indicator being active, or the harzard lights being on.")]
        public bool RightIndicatorLightOn => Telemetry.blinkerRightOn != 0;

        [DataModelProperty(Description = "Whether the driver has enabled the left indicators.")]
        public bool LeftIndicatorActive => Telemetry.blinkerLeftActive != 0;
        [DataModelProperty(Description = "Whether the driver has enabled the right indicators.")]
        public bool RightIndicatorActive => Telemetry.blinkerRightActive != 0;

        public bool Beacon => Telemetry.beaconOn != 0;
    }


    public class Damage : ChildDataModel
    {
        public Damage(TruckSimulatorDataModel root) : base(root) { }

        [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
        public float Engine => Telemetry.wearEngine;

        [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
        public float Transmission => Telemetry.wearTransmission;

        [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
        public float Cabin => Telemetry.wearCabin;

        [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
        public float Chassis => Telemetry.wearChassis;

        [DataModelProperty(MinValue = 0f, MaxValue = 1f)]
        public float Wheels => Telemetry.wearWheelsAvg;
    }


    public enum ShifterType
    {
        Unknown,
        Acarde,
        Automatic,
        Manaul,
        HShifter
    }
}
