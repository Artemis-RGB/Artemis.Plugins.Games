using System.Runtime.InteropServices;
// ReSharper disable UnusedMember.Global

namespace Artemis.Plugins.Games.TrackMania.Telemetry;

public struct SRaceStateV2
{
    public ERaceState State;
    public uint Time;
    public uint NbRespawns;
    public uint NbCheckpoints;

    public Array125<uint> CheckpointTimes;

    //public uint NbCheckpointsPerLap; // new since Maniaplanet update 2019-10-10; not supported by Trackmania Turbo.
    //public uint NbLapsPerRace; // new since Maniaplanet update 2019-10-10; not supported by Trackmania Turbo.
    //public uint Timestamp;
    //public uint StartTimestamp;
    //public String16 __future__;
    public String32 __future__;
}

[StructLayout(LayoutKind.Sequential)]
public struct SVehicleStateV2
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
    //public SBool IsOnIce;

    //public CarHandicap Handicap;
    //public float BoostRatio;

    public String32 __future__;
};

public struct STelemetryV2
{
    public SHeader Header;
    public uint UpdateNumber;
    public SGameState Game;
    public SRaceStateV2 Race;
    public SObjectState Object;
    public SVehicleStateV2 Vehicle;
    public SDeviceState Device;
    //player state is not present in V2
    //public SPlayerState Player;
}