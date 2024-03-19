using System.Runtime.InteropServices;

namespace Artemis.Plugins.Games.TruckSimulator.Telemetry;

// Adapted from the code at https://github.com/RenCloud/scs-sdk-plugin/blob/master/scs-client/C%23/SCSSdkClient/SCSSdkConvert.cs

// Designed for SCS SDK v1.12

[StructLayout(LayoutKind.Sequential)]
internal readonly struct Trailer
{
    // ----------------------------------------------
    // First zone (offset 0)
    // ----------------------------------------------
    // Booleans
    public readonly Array16<byte> wheelsSteerable;
    public readonly Array16<byte> wheelsSimulated;
    public readonly Array16<byte> wheelsPowered;
    public readonly Array16<byte> wheelsLiftable;
    public readonly Array16<byte> wheelsOnGround;
    public readonly byte attached;

    private readonly Array3<byte> _g1; // Gap of 3 bytes


    // ----------------------------------------------
    // Second zone (offset 84)
    // ----------------------------------------------
    public readonly Array16<uint> wheelSubstances;
    public readonly uint wheelCount;

    // ----------------------------------------------
    // Third zone (offset 152)
    // ----------------------------------------------
    public readonly float damageCargo;
    public readonly float damageChassis;
    public readonly float damageWheels;
    public readonly Array16<float> wheelSuspDeflections;
    public readonly Array16<float> wheelVelocities;
    public readonly Array16<float> wheelsSteering;
    public readonly Array16<float> wheelsRotation;
    public readonly Array16<float> wheelsLift;
    public readonly Array16<float> wheelLiftOffsets;
    public readonly Array16<float> wheelRadii;


    // ----------------------------------------------
    // Fourth zone (offset 612)
    // ----------------------------------------------
    public readonly Vector<float> linearVelocity;
    public readonly Vector<float> angularVelocity;
    public readonly Vector<float> linearAcceleration;
    public readonly Vector<float> angularAcceleration;

    public readonly Vector<float> hook;

    public readonly Array16<Vector<float>> wheelPositions;


    // ----------------------------------------------
    // Fifth zone (offset 864)
    // ----------------------------------------------
    public readonly Placement<double> position;


    // ----------------------------------------------
    // Sixth zone (offset 912)
    // ----------------------------------------------
    public readonly TruckSimString64 id;
    public readonly TruckSimString64 cargoAccessoryId;
    public readonly TruckSimString64 bodyType;
    public readonly TruckSimString64 brandId;
    public readonly TruckSimString64 brand;
    public readonly TruckSimString64 model;
    public readonly TruckSimString64 chainType;
    public readonly TruckSimString64 licensePlate;
    public readonly TruckSimString64 licensePlateCountry;
    public readonly TruckSimString64 licensePlateCountryId;
}
