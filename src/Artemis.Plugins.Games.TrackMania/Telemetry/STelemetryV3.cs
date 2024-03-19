using System.Runtime.InteropServices;
using Artemis.Plugins.Games.TrackMania.DataModels;

// ReSharper disable InconsistentNaming
#pragma warning disable 169

namespace Artemis.Plugins.Games.TrackMania.Telemetry;

public struct SBool
{
    private uint _value;
    
    public static implicit operator bool(SBool b) => b._value != 0;
}

[StructLayout(LayoutKind.Sequential)]
public struct SHeader
{
    public String32 Magic; //  "ManiaPlanet_Telemetry"
    public uint Version;
    public uint Size; // == sizeof(STelemetry)
}

[StructLayout(LayoutKind.Sequential)]
public struct SGameState
{
    public EGameState State;
    public String64 GameplayVariant; // player model 'StadiumCar', 'CanyonCar', ....
    public String64 MapId;
    public String256 MapName;
    public String128 __future__;
}

[StructLayout(LayoutKind.Sequential)]
public struct SRaceState
{
    public ERaceState State;
    public uint Time;
    public uint NbRespawns;
    public uint NbCheckpoints;

    public Array125<uint> CheckpointTimes;

    public uint NbCheckpointsPerLap; // new since Maniaplanet update 2019-10-10; not supported by Trackmania Turbo.
    public uint NbLapsPerRace; // new since Maniaplanet update 2019-10-10; not supported by Trackmania Turbo.
    public uint Timestamp;
    public uint StartTimestamp;
    public String16 __future__;
}

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
}

[StructLayout(LayoutKind.Sequential)]
public struct SVehicleState
{
    public uint Timestamp;

    public float InputSteer;
    public float InputGasPedal;
    public SBool InputIsBraking;
    public SBool InputIsHorn;

    public float EngineRpm; // 1500 -> 10000
    public int EngineCurGear;
    public float EngineTurboRatio; // 1 turbo starting/full .... 0 -> finished
    public SBool EngineFreeWheeling;

    public Array4<SBool> WheelsIsGroundContact;
    public Array4<SBool> WheelsIsSliping;
    public Array4<float> WheelsDamperLen;
    public float WheelsDamperRangeMin;
    public float WheelsDamperRangeMax;

    public float RumbleIntensity;

    public uint SpeedMeter; // unsigned km/h
    public SBool IsInWater;
    public SBool IsSparkling;
    public SBool IsLightTrails;
    public SBool IsLightsOn;
    public SBool IsFlying; // long time since touching ground.
    public SBool IsOnIce;

    public CarHandicap Handicap;
    public float BoostRatio;

    public String20 __future__;
}

[StructLayout(LayoutKind.Sequential)]
public struct SDeviceState
{
    // VrChair state.
    public Vec3 Euler; // yaw, pitch, roll  (order: pitch, roll, yaw)
    public float CenteredYaw; // yaw accumulated + recentered to apply onto the device
    public float CenteredAltitude; // Altitude accumulated + recentered

    public String32 __future__;
}

[StructLayout(LayoutKind.Sequential)]
public struct SPlayerState
{
    public SBool IsLocalPlayer;
    public String4 Trigram;
    public String4 DossardNumber;
    public float Hue;
    public String256 UserName;
    public String28 __future__;
}

[StructLayout(LayoutKind.Sequential)]
public struct STelemetryV3
{
    public SHeader Header;
    public uint UpdateNumber;
    public SGameState Game;
    public SRaceState Race;
    public SObjectState Object;
    public SVehicleState Vehicle;
    public SDeviceState Device;
    public SPlayerState Player;
}