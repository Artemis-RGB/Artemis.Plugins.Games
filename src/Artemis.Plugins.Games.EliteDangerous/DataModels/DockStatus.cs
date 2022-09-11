using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.EliteDangerous.Journal;

namespace Artemis.Plugins.Games.EliteDangerous.DataModels
{
    public class DockStatus
    {
        [DataModelProperty(Description = "Whether the ship is currently docked (at a station).")]
        public bool IsDocked { get; internal set; }

        [DataModelProperty(Description = "Whether the ship is currently landed (on a planet).")]
        public bool IsLanded { get; internal set; }


        [DataModelProperty(Description = "Fired when the ship docks at a station.")]
        public DataModelEvent<DockingEventArgs> Docked { get; } = new();

        [DataModelProperty(Description = "Fired when the pilot's docking request status changes.")]
        public DataModelEvent<DockingEventArgs> RequestStatusChanged { get; } = new();

        [DataModelProperty(Description = "Fired when the ship undocks from a station.")]
        public DataModelEvent<DockingEventArgs> Undocked { get; } = new();

        [DataModelProperty(Description = "Fired when the ship touches down on a planet surface.")]
        public DataModelEvent<LandingEventArgs> Touchdown { get; } = new();

        [DataModelProperty(Description = "Fired when the ship takes off from a planet's surface.")]
        public DataModelEvent<LandingEventArgs> Liftoff { get; } = new();
    }


    public class DockingEventArgs : DataModelEventArgs
    {
        public string StationName { get; init; }
    }

    public class DockingRequestStatusChangedEventArgs : DockingEventArgs
    {
        public DockingStatus Status { get; init; }
        public int? LandingPad { get; init; }
        public DockingDenyReason? DenyReason { get; init; }
    }

    public class LandingEventArgs : DataModelEventArgs
    {
        public bool PlayerControlled { get; init; }
        public string NearestDestination { get; init; }
    }
}
