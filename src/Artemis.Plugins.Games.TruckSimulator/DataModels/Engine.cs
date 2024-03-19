using System;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Engine
{
    public bool IgnitionOn { get; set; }
    public bool EngineOn { get; set; }

    public float RPM { get; set; }
    public float MaximumRPM { get; set; }

    public int Gear { get; set; }
    public int GearDashboard { get; set; }
    public uint HShifterSlot { get; set; }
    public ShifterType ShifterType { get; set; }

    public uint ForwardGearCount { get; set; }
    public uint ReverseGearCount { get; set; }

    public uint RetarderLevel { get; set; }
    public uint RetarderStepCount { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        IgnitionOn = data.electricEnabled != 0;
        EngineOn = data.engineEnabled != 0;
        RPM = data.engineRpm;
        MaximumRPM = data.engineRpmMax;
        Gear = data.selectedGear;
        GearDashboard = data.dashboardGear;
        HShifterSlot = data.hShifterSlot;
        ShifterType = Enum.TryParse<ShifterType>(data.shifterType, ignoreCase: true, out var result) ? result : ShifterType.Unknown;
        ForwardGearCount = data.forwardGearCount;
        ReverseGearCount = data.reverseGearCount;
        RetarderLevel = data.retarderLevel;
        RetarderStepCount = data.retarderStepCount;
    }
}