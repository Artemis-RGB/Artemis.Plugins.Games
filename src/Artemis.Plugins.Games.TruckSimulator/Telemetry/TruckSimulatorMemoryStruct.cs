using System.Runtime.InteropServices;

namespace Artemis.Plugins.Games.TruckSimulator.Telemetry
{

    // Adapted from the code at https://github.com/RenCloud/scs-sdk-plugin/blob/master/scs-client/C%23/SCSSdkClient/SCSSdkConvert.cs

    // Designed for SCS SDK v1.12

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

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] readonly int[] _g1; // Gap of 8 bytes


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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly uint[] wheelSubstances;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SlotSize)] public readonly uint[] slotHandlePosition;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SlotSize)] public readonly uint[] slotSelectors;

        public readonly uint jobDeliveryTime;
        public readonly uint jobStartTime;
        public readonly uint jobFinishingTime;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)] readonly byte[] _g2; // Gap of 48 bytes


        // ----------------------------------------------
        // Third zone (offset 500)
        // ----------------------------------------------
        public readonly int nextRestStop;
        public readonly int selectedGear;
        public readonly int dashboardGear;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SlotSize)] public readonly int[] slotGear;
        public readonly int earnedXp;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)] readonly byte[] _g3; // Gap of 56 bytes


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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelRadii;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)] public readonly float[] gearRatiosForward;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public readonly float[] gearRatiosReverse;
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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelSuspDeflections;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelVelocities;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelSteerings;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelRotations;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelLifts;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelLiftOffsets;

        public readonly float jobCargoDamage;
        public readonly float jobDistanceKm;

        public readonly float refuelAmount;

        public readonly float cargoDamage;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)] readonly byte[] _g4; // Gap of 28 bytes


        // ----------------------------------------------
        // Fifth zone (offset 1500)
        // ----------------------------------------------
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsSteerable;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsSimulated;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsPowered;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsLiftable;

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
        public readonly byte cruiseControlActive;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsOnGround;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public readonly byte[] hShifterSelector;

        public readonly byte jobAutoParked;
        public readonly byte jobAutoLoaded;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)] readonly byte[] _g5; // Gap of 31 bytes


        // ----------------------------------------------
        // Sixth zone (offset 1640)
        // ----------------------------------------------
        public readonly Vector<float> cabinPosition;
        public readonly Vector<float> headPosition;
        public readonly Vector<float> hookPosition;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelPositionsX;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelPositionsY;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelPositionsZ;

        public readonly Vector<float> linearVelocity;
        public readonly Vector<float> angularVelocity;
        public readonly Vector<float> linearAcceleration;
        public readonly Vector<float> angularAcceleration;
        public readonly Vector<float> cabinAngularVelocity;
        public readonly Vector<float> cabinAngularAcceleration;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)] readonly byte[] _g6; // Gap of 60 bytes


        // ----------------------------------------------
        // Seventh zone (offset 2000)
        // ----------------------------------------------
        public readonly Placement<float> cabinOffset;
        public readonly Placement<float> headOffset;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 152)] readonly byte[] _g7; // Gap of 152 bytes


        // ----------------------------------------------
        // Eigth zone (offset 2200)
        // ----------------------------------------------
        public readonly Placement<double> truckPosition;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 52)] readonly byte[] _g8; // Gap of 52 bytes

        // ----------------------------------------------
        // Nineth zone (offset 2300)
        // ----------------------------------------------
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string brandId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string brand;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string modelId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string modelName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string cargoId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string cargo;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string destinationCityId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string destinationCity;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string destinationCompanyId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string destinationCompany;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string sourceCityId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string sourceCity;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string sourceCompanyId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string sourceCompany;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public readonly string shifterType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string licensePlate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string licensePlateCountryId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string licensePlateCountry;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public readonly string jobType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public readonly string fineOffence;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string ferrySourceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string ferryDestinationName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string ferrySourceId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string ferryDestinationId;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string trainSourceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string trainDestinationName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string trainSourceId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string trainDestinationId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] readonly byte[] _g9; // Gap of 20 bytes


        // ----------------------------------------------
        // Tenth zone (offset 4000)
        // ----------------------------------------------
        public readonly ulong jobIncome;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 192)] readonly byte[] _g10; // Gap of 192 bytes


        // ----------------------------------------------
        // Eleventh zone (offset 4200)
        // ----------------------------------------------
        public readonly long jobCancelPenalty;
        public readonly long jobDeliveryRevenue;
        public readonly long finedAmount;
        public readonly long tollgatePayAmount;
        public readonly long ferryPayAmount;
        public readonly long trainPayAmount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 52)] readonly byte[] _g11; // Gap of 52 bytes


        // ----------------------------------------------
        // Twelveth zone (offset 4300)
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

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 90)] readonly byte[] _g12; // Gap of 90 bytes


        // ----------------------------------------------
        // Thirteenth zone (offset 4400)
        // ----------------------------------------------
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SubstanceCount)] public readonly Substance[] substances;

        // No gap required.


        // ----------------------------------------------
        // Fourteenth zone (offset 6000)
        // ----------------------------------------------
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TrailerCount)] public readonly Trailer[] trailers;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal readonly struct Vector<T> where T : struct
    {
        public readonly T x;
        public readonly T y;
        public readonly T z;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal readonly struct Euler<T> where T : struct
    {
        public readonly T heading;
        public readonly T pitch;
        public readonly T roll;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal readonly struct Placement<T> where T : struct
    {
        public readonly Vector<T> position;
        public readonly Euler<T> orientation;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal readonly struct Substance
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = TruckSimulatorMemoryStruct.StringSize)] public readonly string name;
    }
}
