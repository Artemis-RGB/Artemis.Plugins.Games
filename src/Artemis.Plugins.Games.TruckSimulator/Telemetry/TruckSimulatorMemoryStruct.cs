using System.Runtime.InteropServices;

namespace Artemis.Plugins.Games.TruckSimulator.Telemetry;

// Adapted from the code at https://github.com/RenCloud/scs-sdk-plugin/blob/master/scs-client/C%23/SCSSdkClient/SCSSdkConvert.cs

// Designed for SCS SDK v1.13

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
internal readonly struct TruckSimulatorMemoryStruct
{
    internal const int StringSize = 64;
    internal const int WheelCount = 16;
    internal const int SlotSize = 32;
    internal const int SubstanceCount = 25;
    internal const int TrailerCount = 10;

    // ----------------------------------------------
    // First zone (offset 0)
    // ----------------------------------------------
    public readonly uint sdkActive;
    public readonly uint paused;
    public readonly ulong timestamp;
    public readonly ulong simulationTimestamp;
    public readonly ulong renderTimestamp;
    
    private readonly Array8<byte> _g1; // Gap of 8 bytes

    // ----------------------------------------------
    // Second zone (offset 40)
    // ----------------------------------------------
    public readonly uint dllVersion;
    public readonly uint gameVersionMajor;
    public readonly uint gameVersionMinor;
    public readonly uint currentGame;
    public readonly uint telemetryVersionMajor;
    public readonly uint telemetryVersionMinor;

    public readonly uint gameTime;
    public readonly uint forwardGearCount;
    public readonly uint reverseGearCount;
    public readonly uint retarderStepCount;
    public readonly uint wheelCount;
    public readonly uint selectorCount;
    public readonly uint deliveryTime;
    public readonly uint maxTrailerCount;
    public readonly uint jobCargoUnitCount;
    public readonly uint plannedJobDistanceKm;

    public readonly uint hShifterSlot;
    public readonly uint retarderLevel;
    public readonly uint lightsAuxFront;
    public readonly uint lightsAuxRoof;
    public readonly Array16<uint> wheelSubstances;
    public readonly Array32<uint> slotHandlePosition;
    public readonly Array32<uint> slotSelectors;

    public readonly uint jobDeliveryTime;
    public readonly uint jobStartTime;
    public readonly uint jobFinishingTime;

    private readonly Array48<byte> _g2; // Gap of 48 bytes
    
    // ----------------------------------------------
    // Third zone (offset 500)
    // ----------------------------------------------
    public readonly int nextRestStop;
    public readonly int selectedGear;
    public readonly int dashboardGear;
    public readonly Array32<int> slotGear;
    public readonly int earnedXp;

    private readonly Array56<byte> _g3; // Gap of 56 bytes

    // ----------------------------------------------
    // Fourth zone (offset 700)
    // ----------------------------------------------
    public readonly float scale;

    public readonly float fuelCapacity;
    public readonly float fuelWarningLevel;
    public readonly float adblueCapacity;
    public readonly float adblueWarningLevel;
    public readonly float airPressureWarningLevel;
    public readonly float airPressureEmergencyLevel;
    public readonly float oilPressureWarningLevel;
    public readonly float waterTemperatureWarningLevel;
    public readonly float batteryVoltageWarningLevel;
    public readonly float engineRpmMax;
    public readonly float differentialRatio;
    public readonly float jobCargoMass;
    public readonly Array16<float> wheelRadii;
    public readonly Array24<float> gearRatiosForward;
    public readonly Array8<float> gearRatiosReverse;
    public readonly float jobCargoUnitMass;

    public readonly float speed; // meters per second
    public readonly float engineRpm;
    public readonly float steeringInput;
    public readonly float throttleInput;
    public readonly float brakeInput;
    public readonly float clutchInput;
    public readonly float steeringGame;
    public readonly float throttleGame;
    public readonly float brakeGame;
    public readonly float clutchGame;
    public readonly float cruiseControl;
    public readonly float airPressure;
    public readonly float brakeTemperature;
    public readonly float fuel;
    public readonly float fuelAvgConsumption;
    public readonly float fuelRange;
    public readonly float adblue;
    public readonly float oilPressure;
    public readonly float oilTemperature;
    public readonly float waterTemperature;
    public readonly float batteryVoltage;
    public readonly float dashboardBacklight;
    public readonly float wearEngine;
    public readonly float wearTransmission;
    public readonly float wearCabin;
    public readonly float wearChassis;
    public readonly float wearWheelsAvg;

    public readonly float odometer;
    public readonly float navigationDistance;
    public readonly float navigationTime;
    public readonly float speedLimit;
    public readonly Array16<float> wheelSuspDeflections;
    public readonly Array16<float> wheelVelocities;
    public readonly Array16<float> wheelSteerings;
    public readonly Array16<float> wheelRotations;
    public readonly Array16<float> wheelLifts;
    public readonly Array16<float> wheelLiftOffsets;

    public readonly float jobCargoDamage;
    public readonly float jobDistanceKm;

    public readonly float refuelAmount;

    public readonly float cargoDamage;

    private readonly Array28<byte> _g4; // Gap of 28 bytes


    // ----------------------------------------------
    // Fifth zone (offset 1500)
    // ----------------------------------------------
    public readonly Array16<byte> wheelsSteerable;
    public readonly Array16<byte> wheelsSimulated;
    public readonly Array16<byte> wheelsPowered;
    public readonly Array16<byte> wheelsLiftable;

    public readonly byte cargoLoaded;
    public readonly byte specialJob;

    public readonly byte parkingBrake;
    public readonly byte motorBrake;
    public readonly byte airPressureWarning;
    public readonly byte airPressureEmergency;

    public readonly byte fuelWarning;
    public readonly byte adblueWarning;
    public readonly byte oilPressureWarning;
    public readonly byte waterTemperatureWarning;
    public readonly byte batteryVoltageWarning;
    public readonly byte electricEnabled;
    public readonly byte engineEnabled;
    public readonly byte wipers;
    public readonly byte blinkerLeftActive; // True if the player has enabled the left blinker
    public readonly byte blinkerRightActive;
    public readonly byte blinkerLeftOn; // True if the actual blinker light is on or off (i.e. this will switch true/false while blinkerLeftActive remains true)
    public readonly byte blinkerRightOn;
    public readonly byte parkingLights;
    public readonly byte lowBeamLights;
    public readonly byte highBeamLights;
    public readonly byte beaconOn;
    public readonly byte brakeLightsOn;
    public readonly byte reverseLightsOn;
    public readonly byte hazardLightsOn;
    public readonly byte cruiseControlActive;

    public readonly Array16<byte> wheelsOnGround;

    public readonly Array2<byte> hShifterSelector;

    public readonly byte differentialLock;
    public readonly byte liftAxle;
    public readonly byte liftAxleIndicator;
    public readonly byte trailerLiftAxle;
    public readonly byte trailerLiftAxleIndicator;

    public readonly byte jobAutoParked;
    public readonly byte jobAutoLoaded;

    private readonly Array25<byte> _g5; // Gap of 25 bytes

    // ----------------------------------------------
    // Sixth zone (offset 1640)
    // ----------------------------------------------
    public readonly Vector<float> cabinPosition;
    public readonly Vector<float> headPosition;
    public readonly Vector<float> hookPosition;

    public readonly Array16<float> wheelPositionsX;
    public readonly Array16<float> wheelPositionsY;
    public readonly Array16<float> wheelPositionsZ;

    public readonly Vector<float> linearVelocity;
    public readonly Vector<float> angularVelocity;
    public readonly Vector<float> linearAcceleration;
    public readonly Vector<float> angularAcceleration;
    public readonly Vector<float> cabinAngularVelocity;
    public readonly Vector<float> cabinAngularAcceleration;

    private readonly Array60<byte> _g6; // Gap of 60 bytes

    // ----------------------------------------------
    // Seventh zone (offset 2000)
    // ----------------------------------------------
    public readonly Placement<float> cabinOffset;
    public readonly Placement<float> headOffset;

    private readonly Array152<byte> _g7; // Gap of 152 bytes
    
    // ----------------------------------------------
    // Eighth zone (offset 2200)
    // ----------------------------------------------
    public readonly Placement<double> truckPosition;

    private readonly Array52<byte> _g8; // Gap of 52 bytes

    // ----------------------------------------------
    // Ninth zone (offset 2300)
    // ----------------------------------------------
    public readonly TruckSimString64 brandId;
    public readonly TruckSimString64 brand;
    public readonly TruckSimString64 modelId;
    public readonly TruckSimString64 modelName;
    public readonly TruckSimString64 cargoId;
    public readonly TruckSimString64 cargo;
    public readonly TruckSimString64 destinationCityId;
    public readonly TruckSimString64 destinationCity;
    public readonly TruckSimString64 destinationCompanyId;
    public readonly TruckSimString64 destinationCompany;
    public readonly TruckSimString64 sourceCityId;
    public readonly TruckSimString64 sourceCity;
    public readonly TruckSimString64 sourceCompanyId;
    public readonly TruckSimString64 sourceCompany;

    public readonly TruckSimString16 shifterType;

    public readonly TruckSimString64 licensePlate;
    public readonly TruckSimString64 licensePlateCountryId;
    public readonly TruckSimString64 licensePlateCountry;

    public readonly TruckSimString32 jobType;

    public readonly TruckSimString32 fineOffence;

    public readonly TruckSimString64 ferrySourceName;
    public readonly TruckSimString64 ferryDestinationName;
    public readonly TruckSimString64 ferrySourceId;
    public readonly TruckSimString64 ferryDestinationId;

    public readonly TruckSimString64 trainSourceName;
    public readonly TruckSimString64 trainDestinationName;
    public readonly TruckSimString64 trainSourceId;
    public readonly TruckSimString64 trainDestinationId;

    private readonly Array20<byte> _g9; // Gap of 20 bytes

    // ----------------------------------------------
    // Tenth zone (offset 4000)
    // ----------------------------------------------
    public readonly ulong jobIncome;

    private readonly Array192<byte> _g10; // Gap of 192 bytes

    // ----------------------------------------------
    // Eleventh zone (offset 4200)
    // ----------------------------------------------
    public readonly long jobCancelPenalty;
    public readonly long jobDeliveryRevenue;
    public readonly long finedAmount;
    public readonly long tollgatePayAmount;
    public readonly long ferryPayAmount;
    public readonly long trainPayAmount;

    private readonly Array52<byte> _g11; // Gap of 52 bytes

    // ----------------------------------------------
    // Twelfth zone (offset 4300)
    // ----------------------------------------------
    public readonly byte onJob;
    public readonly byte jobFinished;

    public readonly byte jobCancelled;
    public readonly byte jobDelivered;
    public readonly byte fined;
    public readonly byte tollgate;
    public readonly byte ferry;
    public readonly byte train;

    public readonly byte refuel;
    public readonly byte refuelPayed;

    private readonly Array90<byte> _g12; // Gap of 90 bytes

    // ----------------------------------------------
    // Thirteenth zone (offset 4400)
    // ----------------------------------------------
    public readonly Array25<Substance> substances;

    // No gap required.


    // ----------------------------------------------
    // Fourteenth zone (offset 6000)
    // ----------------------------------------------
    public readonly Array10<Trailer> trailers;
}


[StructLayout(LayoutKind.Sequential)]
internal readonly struct Vector<T> where T : unmanaged
{
    public readonly T x;
    public readonly T y;
    public readonly T z;
}

[StructLayout(LayoutKind.Sequential)]
internal readonly struct Euler<T> where T : unmanaged
{
    public readonly T heading;
    public readonly T pitch;
    public readonly T roll;
}

[StructLayout(LayoutKind.Sequential)]
internal readonly struct Placement<T> where T : unmanaged
{
    public readonly Vector<T> position;
    public readonly Euler<T> orientation;
}

[StructLayout(LayoutKind.Sequential)]
internal readonly struct Substance
{
    public readonly TruckSimString64 name;
}
