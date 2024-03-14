using Artemis.Core.Modules;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania.DataModels;

public class PluginDataModel : DataModel
{
    public TrackManiaVersion TrackManiaVersion { get; set; }

    [DataModelProperty(Description = "Information about the current state of the game")]
    public GameDataModel Game { get; set; } = new();

    [DataModelProperty(Description = "Information about the current state of the race")]
    public RaceDataModel Race { get; set; } = new();

    [DataModelProperty(Description = "Current information about the vehicle itself")]
    public VehicleDataModel Vehicle { get; set; } = new();

    internal void Apply(in STelemetry telemetry)
    {
        Game.Apply(in telemetry);
        Race.Apply(in telemetry);
        Vehicle.Apply(in telemetry);
    }
}