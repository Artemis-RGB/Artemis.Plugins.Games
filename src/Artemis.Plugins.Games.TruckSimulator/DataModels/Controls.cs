using Artemis.Core.Modules;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels
{
    public class Controls : ChildDataModel
    {
        public Controls(TruckSimulatorDataModel root) : base(root)
        {
        }

        [DataModelProperty(Description = "The amount of steering that is being sent to the game as input in the range -1 (right) to 1 (left). For digital inputs (such as keyboards), this value will instantly go to -1 or 1.", MinValue = -1, MaxValue = 1)]
        public float SteeringInput => Telemetry.steeringInput;
        [DataModelProperty(Description = "The amount of throttle that is being sent to the game as input in the range 0 (none) to 1 (full). For digital inputs (such as keyboards), this value will instantly go to 1.", MinValue = 0, MaxValue = 1)]
        public float ThrottleInput => Telemetry.throttleInput;
        [DataModelProperty(Description = "The amount of braking that is being sent to the game as input in the range 0 (none) to 1 (full). For digital inputs (such as keyboards), this value will instantly go to 1.", MinValue = 0, MaxValue = 1)]
        public float BrakeInput => Telemetry.brakeInput;
        [DataModelProperty(Description = "The amount of clutch that is being sent to the game as input in the range 0 (none) to 1 (full). For digital inputs (such as keyboards), this value will instantly go to 1.", MinValue = 0, MaxValue = 1)]
        public float ClutchInput => Telemetry.clutchInput;

        [DataModelProperty(Description = "The amount of steering that is used by the simulation (after interpolation and input easing) in the range -1 (right) to 1 (left).", MinValue = -1, MaxValue = 1)]
        public float SteeringGame => Telemetry.steeringGame;
        [DataModelProperty(Description = "The amount of throttle that is used by the simulation (including throttle induced by cruise control) in the range 0 (none) to 1 (full).", MinValue = 0, MaxValue = 1)]
        public float ThrottleGame => Telemetry.throttleGame;
        [DataModelProperty(Description = "The amount of braking (excluding retarder, parking brakes etc.) that is used by the simulation (after interpolation and input easing) in the range 0 (none) to 1 (full).", MinValue = 0, MaxValue = 1)]
        public float BrakeGame => Telemetry.brakeGame;
        [DataModelProperty(Description = "The amount of clutch that is used by the simulation (including clutch from automatic shifting) in the range 0 (none) to 1 (full).", MinValue = 0, MaxValue = 1)]
        public float ClutchGame => Telemetry.clutchGame;
    }
}
