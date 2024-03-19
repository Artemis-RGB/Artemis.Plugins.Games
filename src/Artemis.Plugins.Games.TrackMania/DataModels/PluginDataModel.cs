using Artemis.Core.Modules;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania.DataModels;

public class PluginDataModel : DataModel
{
    private uint _lastUpdate = uint.MaxValue;
    
    [DataModelProperty(Description = "Information about the current state of the game")]
    public GameDataModel Game { get; set; } = new();

    [DataModelProperty(Description = "Information about the current state of the race")]
    public RaceDataModel Race { get; set; } = new();

    [DataModelProperty(Description = "Current information about the vehicle itself")]
    public VehicleDataModel Vehicle { get; set; } = new();
    
    public PlayerDataModel Player { get; set; } = new();

    internal void Apply(in STelemetry telemetry)
    {
        if (telemetry.UpdateNumber == _lastUpdate)
            return;
        
        Game.Apply(in telemetry);
        Race.Apply(in telemetry);
        Vehicle.Apply(in telemetry);
        Player.Apply(in telemetry);
        
        _lastUpdate = telemetry.UpdateNumber;
    }
}