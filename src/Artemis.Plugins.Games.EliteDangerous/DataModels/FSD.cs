using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.EliteDangerous.Journal;

namespace Artemis.Plugins.Games.EliteDangerous.DataModels
{
    public class FSD
    {
        public bool IsCharging { get; internal set; }
        [DataModelProperty(Description = "Note that this is also true during the uninteruptable jump timer (for both supercruise and hyperspace jumps)")]
        public bool IsJumping { get; internal set; }
        public bool IsCoolingDown { get; internal set; }
        public bool IsMassLocked { get; internal set; }

        [DataModelProperty(Description = "Occurs when the jump is first initiated (i.e. when the countdown starts after charging).")]
        public DataModelEvent<StartJumpEventArgs> StartJump { get; } = new();

        [DataModelProperty(Description = "Occurs when a hyperspace jump completes (i.e. as you enter a new system).")]
        public DataModelEvent<CompleteJumpEventArgs> CompleteJump { get; } = new();
    }


    public class StartJumpEventArgs : DataModelEventArgs
    {
        public JumpType JumpType { get; init; }
        public string StarSystem { get; init; }
        public StarClass? StarClass { get; init; }
    }

    public class CompleteJumpEventArgs : DataModelEventArgs
    {
        public StarClass StarClass { get; init; }
        public float JumpDistance { get; init; }
        public float FuelUsed { get; init; }
    }
}
