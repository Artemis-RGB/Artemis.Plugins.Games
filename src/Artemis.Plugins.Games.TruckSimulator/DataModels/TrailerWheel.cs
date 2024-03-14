using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

/// <summary>
/// Data model that gets data about a specific wheel of a specific trailer.
/// The trailer is determined by the given 'trailerIndex' and the wheel by 'wheelIndex'.
/// </summary>
public class TrailerWheel
{
    private readonly int trailerIndex;
    private readonly int wheelIndex;

    public TrailerWheel(int trailerIndex, int wheelIndex)
    {
        this.trailerIndex = trailerIndex;
        this.wheelIndex = wheelIndex;
    }

    [DataModelProperty(Description = "Whether this wheel is in contact with the ground.")]
    public bool OnGround { get; set; }
    public bool Powered { get; set; }

    [DataModelProperty(Description = "Whether this wheel can be lifted.")]
    public bool Liftable { get; set; }
    [DataModelProperty(Description = "Whether this wheel has been lifted.")]
    public bool Lifted => Liftable && LiftOffset > 0;
    [DataModelProperty(Description = "The vertical displacement of the wheel axle from its normal position due to lifting the axle.", Affix = "m")]
    public float LiftOffset { get; set; }

    [DataModelProperty(Description = "Whether this wheel may turn in the opposite direction of the truck to help the trailer steer round corners.")]
    public bool Steerable { get; set; }
    [DataModelProperty(Description = "Direction the wheel is facing relative to the trailer (0° = straight, <0° = turning left, >0° = turning right)", Affix = "°")]
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
        ref readonly var trailer = ref data.trailers[trailerIndex];
        OnGround = trailer.wheelsOnGround[wheelIndex] != 0;
        Powered = trailer.wheelsPowered[wheelIndex] != 0;
        Liftable = trailer.wheelsLiftable[wheelIndex] != 0;
        LiftOffset = trailer.wheelLiftOffsets[wheelIndex];
        Steerable = trailer.wheelsSteerable[wheelIndex] != 0;
        Steering = trailer.wheelsSteering[wheelIndex] * -360f;
        Velocity = trailer.wheelVelocities[wheelIndex];
        Rotation = trailer.wheelsRotation[wheelIndex] * 360f;
        SuspensionDeflection = trailer.wheelSuspDeflections[wheelIndex];
        Surface = data.substances[(int)trailer.wheelSubstances[wheelIndex]].name;
    }
}