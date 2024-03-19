using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

/// <summary>
/// Data model that accesses details about a specific wheel on the truck itself (determined by 'wheelIndex').
/// </summary>
public class TruckWheel
{
    private readonly int _wheelIndex;

    public TruckWheel(int wheelIndex) => _wheelIndex = wheelIndex;

    [DataModelProperty(Description = "Whether this wheel is in contact with the ground.")]
    public bool OnGround { get; set; }
    public bool Powered { get; set; }

    [DataModelProperty(Description = "Whether this wheel can be lifted.")]
    public bool Liftable { get; set; }
    [DataModelProperty(Description = "Whether this wheel has been lifted.")]
    public bool Lifted { get; set; }
    [DataModelProperty(Description = "The vertical displacement of the wheel axle from its normal position due to lifting the axle.", Affix = "m")]
    public float LiftOffset { get; set; }

    public bool Steerable { get; set; }
    [DataModelProperty(Description = "Direction the wheel is facing relative to the trailer (0° = straight)", Affix = "°")]
    public float Steering { get; set; } 

    [DataModelProperty(Description = "Current rotational speed of the wheel about the axle in rotations per second.", Affix = "RPS")]
    public float Velocity { get; set; }
    [DataModelProperty(Description = "Current rotation of the wheel about the axle in degrees.", Affix = "°")]
    public float Rotation { get; set; }

    [DataModelProperty(Description = "The vertical displacement of the wheel due to the suspension.", Affix = "m")]
    public float SuspensionDeflection { get; set; }

    [DataModelProperty(Description = "Name of the substance underneath this wheel. E.G. 'road' or 'dirt'.")]
    public string Surface { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        OnGround = data.wheelsOnGround[_wheelIndex] != 0;
        Powered = data.wheelsPowered[_wheelIndex] != 0;
        Liftable = data.wheelsLiftable[_wheelIndex] != 0;
        LiftOffset = data.wheelLiftOffsets[_wheelIndex];
        Lifted = Liftable && LiftOffset > 0;
        Steerable = data.wheelsSteerable[_wheelIndex] != 0;
        Steering = data.wheelSteerings[_wheelIndex] * -360f;// Multiply by negative because negative turning is to the right which is counter-intuitive IMO
        Velocity = data.wheelVelocities[_wheelIndex];
        Rotation = data.wheelRotations[_wheelIndex] * 360f;
        SuspensionDeflection = data.wheelSuspDeflections[_wheelIndex];
        Surface = data.substances[(int)data.wheelSubstances[_wheelIndex]].name;
    }
}