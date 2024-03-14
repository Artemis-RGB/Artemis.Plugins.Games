using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

/// <summary>
/// Data model that returns data about a particular trailer (determined by the given 'trailerIndex').
/// </summary>
public class Trailer
{
    private readonly int trailerIndex;

    public Trailer(int trailerIndex)
    {
        this.trailerIndex = trailerIndex;
        Wheels = new TrailerWheels(trailerIndex);
    }

    [DataModelProperty(Description = "Whether this trailer is attached to an object. Does NOT represent whether the trailer is attached to the player's truck - in the case of multiple trailers (e.g. B-Doubles) this will be true because the second trailer will be attached to the first regardless of whether it is connected to the truck or not.")]
    public bool Attached { get; set; }
    public string ChainType { get; set; }

    public float CargoDamage { get; set; }
    public float ChassisDamage { get; set; }
    public float WheelDamage { get; set; }

    public string BodyType { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }

    public TrailerWheels Wheels { get; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        Wheels.Update(in data);
        ref readonly var trailer = ref data.trailers[trailerIndex];
        Attached = trailer.attached != 0;
        ChainType = trailer.chainType;
        CargoDamage = trailer.damageCargo;
        ChassisDamage = trailer.damageChassis;
        WheelDamage = trailer.damageWheels;
        BodyType = trailer.bodyType;
        Brand = trailer.brand;
        Model = trailer.model;
    }
}