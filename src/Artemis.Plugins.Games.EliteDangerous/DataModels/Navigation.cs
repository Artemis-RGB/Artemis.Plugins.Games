using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.EliteDangerous.Journal;

namespace Artemis.Plugins.Games.EliteDangerous.DataModels
{
    public class Navigation
    {
        [DataModelProperty(Description = "The name of the current system.")]
        public string CurrentSystem { get; private set; } = "Unknown";
        [DataModelProperty(Description = "When not in supercruise, the name of the body the player is at.")]
        public string CurrentBody { get; private set; } = "Unknown";
        [DataModelProperty(Description = "When not in supercruise, the type of body the player is at.")]
        public BodyType? CurrentBodyType { get; private set; }
        [DataModelProperty(Description = "When docked at a station, the name of the station.")]
        public string CurrentStation { get; internal set; } = "Unknown";

        public double? Latitude { get; internal set; }
        public double? Longitude { get; internal set; }
        public double? Altitude { get; internal set; }
        public double? Heading { get; internal set; }

        public float MaximumUnladenJumpRange { get; internal set; }
        public int RemainingJumpsInRoute { get; internal set; }

        [DataModelProperty(Description = "Fires when entering orbital cruise around a planetary body.")]
        public DataModelEvent<ApproachBodyEventArgs> ApproachBody { get; } = new();
        [DataModelProperty(Description = "Fires when leaving orbital cruise around a planetary body.")]
        public DataModelEvent<ApproachBodyEventArgs> LeaveBody { get; } = new();

        public DataModelEvent EnterSupercruise { get; } = new();
        public DataModelEvent ExitSupercruise { get; } = new();

        [DataModelProperty(Description = "Occurs when a location is selected in the galaxy map. Also occurs just after entering a hyperspace jump when on a multi-stop route.")]
        public DataModelEvent FSDTarget { get; } = new();

        public DockStatus DockStatus { get; } = new();

        internal void UpdateLocation(string system, string body = null, BodyType? bodyType = null, string station = null)
        {
            CurrentSystem = system;
            CurrentBody = body;
            CurrentBodyType = bodyType;
            CurrentStation = station;
        }
    }

    public class ApproachBodyEventArgs : DataModelEventArgs
    {
        public string BodyName { get; init; }
    }
}
