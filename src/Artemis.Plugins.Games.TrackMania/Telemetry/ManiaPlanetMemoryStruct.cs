using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.TrackMania.Telemetry
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct Vec3
    {
        float x, y, z;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct Quat
    {
        float w, x, y, z;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct STelemetry
    {
        public struct SHeader
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string Magic; //  "ManiaPlanet_Telemetry"

            uint Version;
            uint Size; // == sizeof(STelemetry)
        };

        public enum EGameState
        {
            EState_Starting = 0,
            EState_Menus,
            EState_Running,
            EState_Paused,
        };

        public enum ERaceState
        {
            ERaceState_BeforeState = 0,
            ERaceState_Running,
            ERaceState_Finished,
        };

        public struct SGameState
        {
            public EGameState State;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string GameplayVariant; // player model 'StadiumCar', 'CanyonCar', ....

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string MapId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string MapName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            string __future__;
        };

        public struct SRaceState
        {
            public ERaceState State;
            public uint Time;
            public uint NbRespawns;
            public uint NbCheckpoints;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 125)]
            public int[] CheckpointTimes;

            public uint NbCheckpointsPerLap; // new since Maniaplanet update 2019-10-10; not supported by Trackmania Turbo.
            public uint NbLaps; // new since Maniaplanet update 2019-10-10; not supported by Trackmania Turbo.

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            string __future__;
        };

        public struct SObjectState
        {
            uint Timestamp;
            uint DiscontinuityCount; // the number changes everytime the object is moved not continuously (== teleported).
            Quat Rotation;
            Vec3 Translation; // +x is "left", +y is "up", +z is "front"
            Vec3 Velocity; // (world velocity)
            uint LatestStableGroundContactTime;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string __future__;
        };

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

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public bool[] WheelsIsGroundContact;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public bool[] WheelsIsSliping;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public float[] WheelsDamperLen;

            public float WheelsDamperRangeMin;
            public float WheelsDamperRangeMax;

            public float RumbleIntensity;

            public uint SpeedMeter; // unsigned km/h
            public bool IsInWater;
            public bool IsSparkling;
            public bool IsLightTrails;
            public bool IsLightsOn;
            public bool IsFlying; // long time since touching ground.

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string __future__;
        };

        public struct SDeviceState
        {
            // VrChair state.
            Vec3 Euler; // yaw, pitch, roll  (order: pitch, roll, yaw)
            float CenteredYaw; // yaw accumulated + recentered to apply onto the device
            float CenteredAltitude; // Altitude accumulated + recentered

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string __future__;
        };

        public SHeader Header;
        public uint UpdateNumber;
        public SGameState Game;
        public SRaceState Race;
        public SObjectState Object;
        public SVehicleState Vehicle;
        public SDeviceState Device;
    };
}