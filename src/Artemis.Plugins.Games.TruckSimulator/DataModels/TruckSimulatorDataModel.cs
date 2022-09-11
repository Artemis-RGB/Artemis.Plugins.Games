using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels
{
    public partial class TruckSimulatorDataModel : DataModel
    {
        public TruckSimulatorDataModel()
        {
            Game = new Game(this);
            Truck = new Truck(this);
            Trailers = new Trailers(this);
            Job = new Job(this);
            Navigation = new Navigation(this);
            CruiseControl = new CruiseControl(this);
            Controls = new Controls(this);
        }

        // This is the struct that gets updated with the latest telemetry data from the mapped file reader.
        internal TruckSimulatorMemoryStruct Telemetry { get; set; }

        // Child data models
        public Game Game { get; }
        public Truck Truck { get; }
        public Trailers Trailers { get; }
        public Job Job { get; }
        public Navigation Navigation { get; }
        public CruiseControl CruiseControl { get; }
        public Controls Controls { get; }

        // Misc./ungrouped properties
        [DataModelProperty(Description = "Number of minutes until the driver is due a rest stop.", Affix = "min")]
        public int NextRestStop => Telemetry.nextRestStop;
    }
}
