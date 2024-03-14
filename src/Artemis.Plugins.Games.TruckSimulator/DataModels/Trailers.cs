using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;
using System.Collections.Generic;
using System.Linq;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Trailers
{
    private readonly Trailer[] trailerAccessors;

    public Trailers()
    {
        trailerAccessors = new Trailer[TruckSimulatorMemoryStruct.TrailerCount];
        for (var i = 0; i < TruckSimulatorMemoryStruct.TrailerCount; i++)
            trailerAccessors[i] = new Trailer(i);
    }

    [DataModelProperty(Description = "Total number of trailers. Note: this includes any that are spawned but not yet hooked up to the truck.")]
    public int TrailerCount { get; set; }

    [DataModelProperty(Description = "Whether a trailer is attached to the truck.")]
    public bool Attached { get; set; }

    [DataModelProperty(Description = "Whether the trailer's lift axle is currently lifted.")]
    public bool TrailerLiftAxle { get; set; }
    [DataModelProperty(Description = "Whether the trailer's lift axle indicator light is currently on.")]
    public bool TrailerLiftAxleIndicator { get; set; }

    [DataModelProperty(Description = "List containing details about the state of each trailer.")]
    // Returns as many trailers as there are currently spawned in the world - not necessarily as many as the SDK supports.
    public IEnumerable<Trailer> TrailerData => trailerAccessors.Take(TrailerCount);

    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        Attached = data.trailers[0].attached != 0;
        TrailerLiftAxle = data.trailerLiftAxle != 0;
        TrailerLiftAxleIndicator = data.trailerLiftAxleIndicator != 0;

        var trailerCount = (int)data.maxTrailerCount;
        for (var i = 0; i < trailerCount; i++)
        {
            ref readonly var trailer = ref data.trailers[i];
            if (string.IsNullOrWhiteSpace(trailer.id))
            {
                trailerCount = i;
            }
        }

        TrailerCount = trailerCount;

        foreach (var trailer in trailerAccessors)
            trailer.Update(in data);
    }
}