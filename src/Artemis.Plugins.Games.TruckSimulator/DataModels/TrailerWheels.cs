using System.Collections.Generic;
using System.Linq;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class TrailerWheels
{
    private readonly int trailerIndex;
    private readonly TrailerWheel[] wheelAccessors;

    public TrailerWheels(int trailerIndex)
    {
        this.trailerIndex = trailerIndex;

        wheelAccessors = new TrailerWheel[TruckSimulatorMemoryStruct.WheelCount];
        for (var wheelIndex = 0; wheelIndex < TruckSimulatorMemoryStruct.WheelCount; wheelIndex++)
            wheelAccessors[wheelIndex] = new TrailerWheel(trailerIndex, wheelIndex);
    }

    [DataModelProperty(Description = "Number of wheels on this trailer.")]
    public int WheelCount { get; set; }

    [DataModelProperty(Description = "Gets details about individual wheels on this trailer. The first wheel in the list is usually the front left, then front right, 2nd-from-front left, 2nd-from-front right, etc.")]
    // Only return as many wheels as there are on the trailer - not usually as many as the SDK supports
    public IEnumerable<TrailerWheel> WheelData => wheelAccessors.Take(WheelCount);
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        WheelCount = (int)data.trailers[trailerIndex].wheelCount;
        
        foreach (var wheel in wheelAccessors)
            wheel.Update(in data);
    }
}