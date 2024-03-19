using System;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Conversions;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Job
{
    [DataModelProperty(Description = "Whether the player is currently doing a delivery job.")]
    public bool OnJob { get; set; }

    [DataModelProperty(Description = "The date and time that the customer expects their delivery by.")]
    public DateTime DeliveryTime { get; set; }

    [DataModelProperty(Description = "Time until the current delivery is due at the destination in minutes.", Affix = "min")]
    public int RemainingDeliveryTime { get; set; }

    [DataModelProperty(Description = "Indicates whether cargo is loaded onto the trailer. In the case of the freight market or external market, cargo is always pre-loaded onto the trailer. Only when taking jobs through the cargo market or external cargo market (i.e. when you bring your own trailer) can cargo be waiting to be loaded.")]
    public bool CargoLoaded { get; set; }
    public bool SpecialCargo { get; set; }

    [DataModelProperty(Description = "Name of the cargo type being transported.")]
    public string CargoType { get; set; }
    [DataModelProperty(Description = "Total number of units of cargo being transported.")]
    public uint CargoUnitCount { get; set; }
    [DataModelProperty(Description = "Mass of a single unit of cargo in kilograms.", Affix = "kg")]
    public float CargoUnitMass { get; set; }
    [DataModelProperty(Description = "Total mass of all cargo in kilograms.", Affix = "kg")]
    public float CargoTotalMass { get; set; }

    public string DestinationCity { get; set; }
    public string DestinationCompany { get; set; }

    public string SourceCity { get; set; }
    public string SourceCompany { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        OnJob = data.onJob != 0;
        DeliveryTime = data.deliveryTime.ToGameDateTime();
        RemainingDeliveryTime = data is { gameTime: < 4_000_000_000, deliveryTime: > 0 }
            ? (int)(data.deliveryTime - data.gameTime)
            : 0;
        CargoLoaded = data.cargoLoaded != 0;
        SpecialCargo = data.specialJob != 0;
        CargoType = data.cargo;
        CargoUnitCount = data.jobCargoUnitCount;
        CargoUnitMass = data.jobCargoUnitMass;
        CargoTotalMass = data.jobCargoMass;
        DestinationCity = data.destinationCity;
        DestinationCompany = data.destinationCompany;
        SourceCity = data.sourceCity;
        SourceCompany = data.sourceCompany;
    }
}