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

    public void Apply(in SVehicleState vehicle)
    {
        InputSteer = vehicle.InputSteer;
        InputGasPedal = vehicle.InputGasPedal;
        InputIsBraking = vehicle.InputIsBraking;
        InputIsHorn = vehicle.InputIsHorn;

        EngineRpm = vehicle.EngineRpm;
        EngineCurGear = vehicle.EngineCurGear;
        EngineTurboRatio = vehicle.EngineTurboRatio;
        EngineFreeWheeling = vehicle.EngineFreeWheeling;

        FrontLeftWheel.HasGroundContact = vehicle.WheelsIsGroundContact[0];
        FrontLeftWheel.IsSlipping = vehicle.WheelsIsSliping[0];
        FrontLeftWheel.DamperLength = vehicle.WheelsDamperLen[0];
        FrontRightWheel.HasGroundContact = vehicle.WheelsIsGroundContact[1];
        FrontRightWheel.IsSlipping = vehicle.WheelsIsSliping[1];
        FrontRightWheel.DamperLength = vehicle.WheelsDamperLen[1];
        BackLeftWheel.HasGroundContact = vehicle.WheelsIsGroundContact[2];
        BackLeftWheel.IsSlipping = vehicle.WheelsIsSliping[2];
        BackLeftWheel.DamperLength = vehicle.WheelsDamperLen[2];
        BackRightWheel.HasGroundContact = vehicle.WheelsIsGroundContact[3];
        BackRightWheel.IsSlipping = vehicle.WheelsIsSliping[3];
        BackRightWheel.DamperLength = vehicle.WheelsDamperLen[3];


        WheelsDamperRangeMin = vehicle.WheelsDamperRangeMin;
        WheelsDamperRangeMax = vehicle.WheelsDamperRangeMax;
        RumbleIntensity = vehicle.RumbleIntensity;

        MetricSpeed = (int) vehicle.SpeedMeter;
        ImperialSpeed = (int) Math.Round(vehicle.SpeedMeter * 0.621, MidpointRounding.AwayFromZero);
        IsInWater = vehicle.IsInWater;
        IsSparkling = vehicle.IsSparkling;
        IsLightTrails = vehicle.IsLightTrails;
        IsLightsOn = vehicle.IsLightsOn;
        IsFlying = vehicle.IsFlying;
        IsOnIce = vehicle.IsOnIce;
        Handicap = vehicle.Handicap;
        BoostRatio = vehicle.BoostRatio;
    }
    
    public void Apply(in SVehicleStateV2 vehicle)
    {
        InputSteer = vehicle.InputSteer;
        InputGasPedal = vehicle.InputGasPedal;
        InputIsBraking = vehicle.InputIsBraking;
        InputIsHorn = vehicle.InputIsHorn;

        EngineRpm = vehicle.EngineRpm;
        EngineCurGear = vehicle.EngineCurGear;
        EngineTurboRatio = vehicle.EngineTurboRatio;
        EngineFreeWheeling = vehicle.EngineFreeWheeling;

        FrontLeftWheel.HasGroundContact = vehicle.WheelsIsGroundContact[0];
        FrontLeftWheel.IsSlipping = vehicle.WheelsIsSliping[0];
        FrontLeftWheel.DamperLength = vehicle.WheelsDamperLen[0];
        FrontRightWheel.HasGroundContact = vehicle.WheelsIsGroundContact[1];
        FrontRightWheel.IsSlipping = vehicle.WheelsIsSliping[1];
        FrontRightWheel.DamperLength = vehicle.WheelsDamperLen[1];
        BackLeftWheel.HasGroundContact = vehicle.WheelsIsGroundContact[2];
        BackLeftWheel.IsSlipping = vehicle.WheelsIsSliping[2];
        BackLeftWheel.DamperLength = vehicle.WheelsDamperLen[2];
        BackRightWheel.HasGroundContact = vehicle.WheelsIsGroundContact[3];
        BackRightWheel.IsSlipping = vehicle.WheelsIsSliping[3];
        BackRightWheel.DamperLength = vehicle.WheelsDamperLen[3];


        WheelsDamperRangeMin = vehicle.WheelsDamperRangeMin;
        WheelsDamperRangeMax = vehicle.WheelsDamperRangeMax;
        RumbleIntensity = vehicle.RumbleIntensity;

        MetricSpeed = (int) vehicle.SpeedMeter;
        ImperialSpeed = (int) Math.Round(vehicle.SpeedMeter * 0.621, MidpointRounding.AwayFromZero);
        IsInWater = vehicle.IsInWater;
        IsSparkling = vehicle.IsSparkling;
        IsLightTrails = vehicle.IsLightTrails;
        IsLightsOn = vehicle.IsLightsOn;
        IsFlying = vehicle.IsFlying;
        IsOnIce = false;
        Handicap = CarHandicap.None;
        BoostRatio = 0f;
    }
}