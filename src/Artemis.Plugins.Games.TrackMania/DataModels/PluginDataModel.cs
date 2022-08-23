using System;
using System.Collections.Generic;
using System.Linq;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania.DataModels
{
    public class PluginDataModel : DataModel
    {
        public TrackManiaVersion TrackManiaVersion { get; set; }

        [DataModelProperty(Description = "Information about the current state of the game")]
        public GameDataModel Game { get; set; } = new();

        [DataModelProperty(Description = "Information about the current state of the race")]
        public RaceDataModel Race { get; set; } = new();

        [DataModelProperty(Description = "Current information about the vehicle itself")]
        public VehicleDataModel Vehicle { get; set; } = new();

        public void Apply(STelemetry telemetry)
        {
            Game.Apply(telemetry);
            Race.Apply(telemetry);
            Vehicle.Apply(telemetry);
        }
    }

    public class GameDataModel : DataModel
    {
        public STelemetry.EGameState State { get; set; }
        public string GameplayVariant { get; set; }
        public string MapName { get; set; }
        public string MapId { get; set; }


        public void Apply(STelemetry telemetry)
        {
            State = telemetry.Game.State;
            GameplayVariant = telemetry.Game.GameplayVariant;
            MapName = telemetry.Game.MapName;
            MapId = telemetry.Game.MapId;
        }
    }

    public class RaceDataModel : DataModel
    {
        public STelemetry.ERaceState State { get; set; }
        public TimeSpan Time { get; set; }
        public int Respawns { get; set; }
        public int Checkpoints { get; set; }

        public List<TimeSpan> CheckpointTimes { get; set; }
        public int CheckpointsPerLap { get; set; }
        public int Laps { get; set; }

        public void Apply(STelemetry telemetry)
        {
            State = telemetry.Race.State;
            Time = TimeSpan.FromMilliseconds(Math.Max(0, telemetry.Race.Time));
            Respawns = (int) telemetry.Race.NbRespawns;
            Checkpoints = (int) telemetry.Race.NbCheckpoints;

            CheckpointTimes = telemetry.Race.CheckpointTimes.Select(t => TimeSpan.FromMilliseconds(Math.Max(0, t))).ToList();
            CheckpointsPerLap = (int) telemetry.Race.NbCheckpointsPerLap;
            Laps = (int) telemetry.Race.NbLaps;
        }
    }


    public class VehicleDataModel : DataModel
    {
        public float InputSteer { get; set; }
        public float InputGasPedal { get; set; }
        public bool InputIsBraking { get; set; }
        public bool InputIsHorn { get; set; }

        public float EngineRpm { get; set; } // 1500 -> 10000
        public int EngineCurGear { get; set; }
        public float EngineTurboRatio { get; set; } // 1 turbo starting/full .... 0 -> finished
        public bool EngineFreeWheeling { get; set; }

        public WheelDataModel FrontLeftWheel { get; set; } = new();
        public WheelDataModel FrontRightWheel { get; set; } = new();
        public WheelDataModel BackLeftWheel { get; set; } = new();
        public WheelDataModel BackRightWheel { get; set; } = new();

        public float WheelsDamperRangeMin { get; set; }
        public float WheelsDamperRangeMax { get; set; }

        public float RumbleIntensity { get; set; }

        public int MetricSpeed { get; set; }
        public int ImperialSpeed { get; set; }
        public bool IsInWater { get; set; }
        public bool IsSparkling { get; set; }
        public bool IsLightTrails { get; set; }
        public bool IsLightsOn { get; set; }
        public bool IsFlying { get; set; } // long time since touching ground.
        public bool IsOnIce { get; set; }
        public CarHandicap Handicap { get; set; }
        public uint HandicapTest { get; set; }
        public float BoostRatio { get; set; }

        public void Apply(STelemetry telemetry)
        {
            InputSteer = telemetry.Vehicle.InputSteer;
            InputGasPedal = telemetry.Vehicle.InputGasPedal;
            InputIsBraking = telemetry.Vehicle.InputIsBraking;
            InputIsHorn = telemetry.Vehicle.InputIsHorn;

            EngineRpm = telemetry.Vehicle.EngineRpm;
            EngineCurGear = (int) telemetry.Vehicle.EngineCurGear;
            EngineTurboRatio = telemetry.Vehicle.EngineTurboRatio;
            EngineFreeWheeling = telemetry.Vehicle.EngineFreeWheeling;

            FrontLeftWheel.HasGroundContact = telemetry.Vehicle.WheelsIsGroundContact[0];
            FrontLeftWheel.IsSlipping = telemetry.Vehicle.WheelsIsSliping[0];
            FrontLeftWheel.DamperLength = telemetry.Vehicle.WheelsDamperLen[0];
            FrontRightWheel.HasGroundContact = telemetry.Vehicle.WheelsIsGroundContact[1];
            FrontRightWheel.IsSlipping = telemetry.Vehicle.WheelsIsSliping[1];
            FrontRightWheel.DamperLength = telemetry.Vehicle.WheelsDamperLen[1];
            BackLeftWheel.HasGroundContact = telemetry.Vehicle.WheelsIsGroundContact[2];
            BackLeftWheel.IsSlipping = telemetry.Vehicle.WheelsIsSliping[2];
            BackLeftWheel.DamperLength = telemetry.Vehicle.WheelsDamperLen[2];
            BackRightWheel.HasGroundContact = telemetry.Vehicle.WheelsIsGroundContact[3];
            BackRightWheel.IsSlipping = telemetry.Vehicle.WheelsIsSliping[3];
            BackRightWheel.DamperLength = telemetry.Vehicle.WheelsDamperLen[3];


            WheelsDamperRangeMin = telemetry.Vehicle.WheelsDamperRangeMin;
            WheelsDamperRangeMax = telemetry.Vehicle.WheelsDamperRangeMax;
            RumbleIntensity = telemetry.Vehicle.RumbleIntensity;

            MetricSpeed = (int) telemetry.Vehicle.SpeedMeter;
            ImperialSpeed = (int) Math.Round(telemetry.Vehicle.SpeedMeter * 0.621, MidpointRounding.AwayFromZero);
            IsInWater = telemetry.Vehicle.IsInWater;
            IsSparkling = telemetry.Vehicle.IsSparkling;
            IsLightTrails = telemetry.Vehicle.IsLightTrails;
            IsLightsOn = telemetry.Vehicle.IsLightsOn;
            IsFlying = telemetry.Vehicle.IsFlying;
            IsOnIce = telemetry.Vehicle.IsOnIce;
            Handicap = telemetry.Vehicle.Handicap;
            BoostRatio = telemetry.Vehicle.BoostRatio;
        }


    }

    public class WheelDataModel
    {
        public bool HasGroundContact { get; set; }
        public bool IsSlipping { get; set; }
        public float DamperLength { get; set; }
    }

    public enum TrackManiaVersion
    {
        ManiaPlanet
    }
    
    
    [Flags]
    public enum CarHandicap
    {
        None = 0,
        EngineForcedOff = 1,
        EngineForcedOn = 2,
        NoBrakes = 4,
        NoSteering = 8,
        NoGrip = 16
    }
}