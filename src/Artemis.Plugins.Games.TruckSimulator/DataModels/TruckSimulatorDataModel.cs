using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class TruckSimulatorDataModel : DataModel
{
    // Child data models
    public Game Game { get; } = new();
    public Truck Truck { get; }= new();
    public Trailers Trailers { get; }= new();
    public Job Job { get; }= new();
    public Navigation Navigation { get; }= new();
    public CruiseControl CruiseControl { get; }= new();
    public Controls Controls { get; } = new();
    public Events Events { get; } = new();

    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        Game.Update(in data);
        Truck.Update(in data);
        Trailers.Update(in data);
        Job.Update(in data);
        Navigation.Update(in data);
        CruiseControl.Update(in data);
        Controls.Update(in data);
        Events.Update(in data);
    }
}