using System.Runtime.InteropServices;

namespace Artemis.Plugins.Modules.TruckSimulator.Telemetry
{

    // Adapted from the code at https://github.com/RenCloud/scs-sdk-plugin/blob/master/scs-client/C%23/SCSSdkClient/SCSSdkConvert.cs

    // Designed for SCS SDK v1.12

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal readonly struct Trailer
    {

        internal const int StringSize = TruckSimulatorMemoryStruct.StringSize;
        internal const int WheelCount = TruckSimulatorMemoryStruct.WheelCount;

        // ----------------------------------------------
        // First zone (offset 0)
        // ----------------------------------------------
        // Booleans
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsSteerable;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsSimulated;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsPowered;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsLiftable;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly byte[] wheelsOnGround;
        public readonly byte attached;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] readonly byte[] _g1; // Gap of 3 bytes


        // ----------------------------------------------
        // Second zone (offset 84)
        // ----------------------------------------------
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly uint[] wheelSubstances;
        public readonly uint wheelCount;


        // ----------------------------------------------
        // Third zone (offset 152)
        // ----------------------------------------------
        public readonly float damageCargo;
        public readonly float damageChassis;
        public readonly float damageWheels;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelSuspDeflections;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelVelocities;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelsSteering;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelsRotation;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelsLift;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelLiftOffsets;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly float[] wheelRadii;


        // ----------------------------------------------
        // Fourth zone (offset 612)
        // ----------------------------------------------
        public readonly Vector<float> linearVelocity;
        public readonly Vector<float> angularVelocity;
        public readonly Vector<float> linearAcceleration;
        public readonly Vector<float> angularAcceleration;

        public readonly Vector<float> hook;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = WheelCount)] public readonly Vector<float>[] wheelPositions;


        // ----------------------------------------------
        // Fifth zone (offset 864)
        // ----------------------------------------------
        public readonly Placement<double> position;


        // ----------------------------------------------
        // Sixth zone (offset 912)
        // ----------------------------------------------
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string id;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string cargoAccessoryId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string bodyType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string brandId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string brand;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string model;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string chainType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string licensePlate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string licensePlateCountry;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = StringSize)] public readonly string licensePlateCountryId;
    }
}
