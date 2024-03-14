using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Controls
{
    [DataModelProperty(Description = "The amount of steering that is being sent to the game as input in the range -1 (right) to 1 (left). For digital inputs (such as keyboards), this value will instantly go to -1 or 1.", MinValue = -1, MaxValue = 1)]
    public float SteeringInput { get; set; }
    [DataModelProperty(Description = "The amount of throttle that is being sent to the game as input in the range 0 (none) to 1 (full). For digital inputs (such as keyboards), this value will instantly go to 1.", MinValue = 0, MaxValue = 1)]
    public float ThrottleInput { get; set; }
    [DataModelProperty(Description = "The amount of braking that is being sent to the game as input in the range 0 (none) to 1 (full). For digital inputs (such as keyboards), this value will instantly go to 1.", MinValue = 0, MaxValue = 1)]
    public float BrakeInput{ get; set; }
    [DataModelProperty(Description = "The amount of clutch that is being sent to the game as input in the range 0 (none) to 1 (full). For digital inputs (such as keyboards), this value will instantly go to 1.", MinValue = 0, MaxValue = 1)]
    public float ClutchInput { get; set; }

    [DataModelProperty(Description = "The amount of steering that is used by the simulation (after interpolation and input easing) in the range -1 (right) to 1 (left).", MinValue = -1, MaxValue = 1)]
    public float SteeringGame { get; set; }
    [DataModelProperty(Description = "The amount of throttle that is used by the simulation (including throttle induced by cruise control) in the range 0 (none) to 1 (full).", MinValue = 0, MaxValue = 1)]
    public float ThrottleGame { get; set; }
    [DataModelProperty(Description = "The amount of braking (excluding retarder, parking brakes etc.) that is used by the simulation (after interpolation and input easing) in the range 0 (none) to 1 (full).", MinValue = 0, MaxValue = 1)]
    public float BrakeGame { get; set; }

    [DataModelProperty(Description = "The amount of clutch that is used by the simulation (including clutch from automatic shifting) in the range 0 (none) to 1 (full).", MinValue = 0, MaxValue = 1)]
    public float ClutchGame { get; set; }

    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        SteeringInput = data.steeringInput;
        ThrottleInput = data.throttleInput;
        BrakeInput = data.brakeInput;
        ClutchInput = data.clutchInput;
        SteeringGame = data.steeringGame;
        ThrottleGame = data.throttleGame;
        BrakeGame = data.brakeGame;
        ClutchGame = data.clutchGame;
    }
}