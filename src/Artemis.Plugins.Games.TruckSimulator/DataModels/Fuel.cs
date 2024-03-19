using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Fuel
{
    [DataModelProperty(Description = "Total fuel tank capacity of the truck in litres.", Affix = "L")]
    public float Capacity { get; set; }

    [DataModelProperty(Description = "Current fuel amount stored in the truck's tanks in litres.", Affix = "L")]
    public float Amount { get; set; }

    [DataModelProperty(Description = "Average consumption of fuel measured in liters per kilometer.", Affix = "L/km")]
    public float AverageConsumption { get; set; }

    [DataModelProperty(Description = "Estimated distance that can be travelled with remaining fuel in kilometers.", Affix = "km")]
    public float Range { get; set; }

    [DataModelProperty(Description = "Whether the fuel warning light is current lit.")]
    public bool FuelWarningActive { get; set; }

    [DataModelProperty(Description = "The fraction of fuel in the tank below which the fuel warning is active.")]
    public float FuelWarningLevel { get; set; }

    [DataModelProperty(Description = "Whether fuel is actively being pumped into the vehicle.")]
    public bool Refueling { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        Capacity = data.fuelCapacity;
        Amount = data.fuel;
        AverageConsumption = data.fuelAvgConsumption;
        Range = data.fuelRange;
        FuelWarningActive = data.fuelWarning != 0;
        FuelWarningLevel = data.fuelWarningLevel;
        Refueling = data.refuel != 0;
    }
}