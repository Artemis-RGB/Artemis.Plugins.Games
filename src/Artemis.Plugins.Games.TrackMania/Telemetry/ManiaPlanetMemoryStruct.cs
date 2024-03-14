using System.Runtime.InteropServices;
using Artemis.Plugins.Games.TrackMania.DataModels;

// ReSharper disable InconsistentNaming
#pragma warning disable 169

namespace Artemis.Plugins.Games.TrackMania.Telemetry;

[StructLayout(LayoutKind.Sequential)]
public struct SHeader
{
    public String32 Magic; //  "ManiaPlanet_Telemetry"
    public uint Version;
    public uint Size; // == sizeof(STelemetry)
};

[StructLayout(LayoutKind.Sequential)]
public struct SGameState
{
    public EGameState State;
    public String64 GameplayVariant; // player model 'StadiumCar', 'CanyonCar', ....
    public String64 MapId;
    public String256 MapName;
    public String128 __future__;
};

[StructLayout(LayoutKind.Sequential)]
public struct SRaceState
{
    public ERaceState State;
    public uint Time;
    public uint NbRespawns;
    public uint NbCheckpoints;

    public Array125<int> CheckpointTimes;

    public uint NbCheckpointsPerLap; // new since Maniaplanet update 2019-10-10; not supported by Trackmania Turbo.
    public uint NbLaps; // new since Maniaplanet update 2019-10-10; not supported by Trackmania Turbo.

    public String24 __future__;
};

[StructLayout(LayoutKind.Sequential)]
public struct SObjectState
{
    public uint Timestamp;
    public uint DiscontinuityCount; // the number changes everytime the object is moved not continuously (== teleported).
    public Quat Rotation;
    public Vec3 Translation; // +x is "left", +y is "up", +z is "front"
    public Vec3 Velocity; // (world velocity)
    public uint LatestStableGroundContactTime;

    public String32 __future__;
};

[StructLayout(LayoutKind.Sequential)]
public struct SVehicleState
{
    public uint Timestamp;

    public float InputSteer;
    public float InputGasPedal;
    public bool InputIsBraking;
    public bool InputIsHorn;

    public float EngineRpm; // 1500 -> 10000
    public uint EngineCurGear;
    public float EngineTurboRatio; // 1 turbo starting/full .... 0 -> finished
    public bool EngineFreeWheeling;

    //todo: bool or byte?
    public Array4<bool> WheelsIsGroundContact;

    public Array4<bool> WheelsIsSliping;

    public Array4<float> WheelsDamperLen;

    public float WheelsDamperRangeMin;
    public float WheelsDamperRangeMax;

    public float RumbleIntensity;

    public uint SpeedMeter; // unsigned km/h
    public bool IsInWater;
    public bool IsSparkling;
    public bool IsLightTrails;
    public bool IsLightsOn;
    public bool IsFlying; // long time since touching ground.
    public bool IsOnIce;

    public CarHandicap Handicap;
    public float BoostRatio;

    public String20 __future__;
};

[StructLayout(LayoutKind.Sequential)]
public struct SDeviceState
{
    // VrChair state.
    public Vec3 Euler; // yaw, pitch, roll  (order: pitch, roll, yaw)
    public float CenteredYaw; // yaw accumulated + recentered to apply onto the device
    public float CenteredAltitude; // Altitude accumulated + recentered

    public String20 __future__;
};

[StructLayout(LayoutKind.Sequential)]
public struct STelemetry
{
    public SHeader Header;
    public uint UpdateNumber;
    public SGameState Game;
    public SRaceState Race;
    public SObjectState Object;
    public SVehicleState Vehicle;
    public SDeviceState Device;
};