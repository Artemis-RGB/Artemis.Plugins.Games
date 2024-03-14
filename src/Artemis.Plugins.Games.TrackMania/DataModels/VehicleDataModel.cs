using System;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania.DataModels;

public class VehicleDataModel
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

    public void Apply(in STelemetry telemetry)
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