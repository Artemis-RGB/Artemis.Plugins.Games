using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Conversions;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels
{
    public class Job : ChildDataModel
    {
        public Job(TruckSimulatorDataModel root) : base(root)
        {
            DeliveryTime = new DateTimeModel(() => Telemetry.deliveryTime.ToGameDateTime());
        }

        [DataModelProperty(Description = "Whether the player is currently doing a delivery job.")]
        public bool OnJob => Telemetry.onJob != 0;

        [DataModelProperty(Description = "The date and time that the customer expects their delivery by.")]
        public DateTimeModel DeliveryTime { get; }

        [DataModelProperty(Description = "Time until the current delivery is due at the destination in minutes.", Affix = "min")]
        public int RemainingDeliveryTime => Telemetry.gameTime < 4_000_000_000 && Telemetry.deliveryTime > 0
            ? (int)(Telemetry.deliveryTime - Telemetry.gameTime)
            : 0;

        [DataModelProperty(Description = "Indicates whether cargo is loaded onto the trailer. In the case of the freight market or external market, cargo is always pre-loaded onto the trailer. Only when taking jobs through the cargo market or external cargo market (i.e. when you bring your own trailer) can cargo be waiting to be loaded.")]
        public bool CargoLoaded => Telemetry.cargoLoaded != 0;
        public bool SpecialCargo => Telemetry.specialJob != 0;

        [DataModelProperty(Description = "Name of the cargo type being transported.")]
        public string CargoType => Telemetry.cargo;
        [DataModelProperty(Description = "Total number of units of cargo being transported.")]
        public uint CargoUnitCount => Telemetry.jobCargoUnitCount;
        [DataModelProperty(Description = "Mass of a single unit of cargo in kilograms.", Affix = "kg")]
        public float CargoUnitMass => Telemetry.jobCargoUnitMass;
        [DataModelProperty(Description = "Total mass of all cargo in kilograms.", Affix = "kg")]
        public float CargoTotalMass => Telemetry.jobCargoMass;

        public string DestinationCity => Telemetry.destinationCity;
        public string DestinationCompany => Telemetry.destinationCompany;

        public string SourceCity => Telemetry.sourceCity;
        public string SourceCompany => Telemetry.sourceCompany;
    }
}
