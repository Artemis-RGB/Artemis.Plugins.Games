using Artemis.Core.Modules;
using Artemis.Plugins.Modules.TruckSimulator.Conversions;

namespace Artemis.Plugins.Modules.TruckSimulator.DataModels
{
    public class Game : ChildDataModel
    {
        public Game(TruckSimulatorDataModel root) : base(root)
        {
            GameTime = new DateTimeModel(() => Telemetry.gameTime.ToGameDateTime());
        }

        [DataModelProperty(Description = "Returns the game that the telemetry data is currently being retrieved from.")]
        public SCSGame CurrentGame => (SCSGame)Telemetry.currentGame;
        public bool Paused => Telemetry.paused != 0;

        [DataModelProperty(Description = "Current date and time of the game world. Note that the year starts at 1.")]
        public DateTimeModel GameTime { get; }

        [DataModelProperty(Description = "Determines the scale between real time and in-game time. Typical values are 3 (in cities), 15 (in UK) and 19 (everywhere else). For example, at a scale of 19, one real-life minute would be 19 minutes in game.")]
        public float Scale => Telemetry.scale;
    }

    public enum SCSGame
    {
        Unknown,
        ETS2,
        ATS
    }
}
