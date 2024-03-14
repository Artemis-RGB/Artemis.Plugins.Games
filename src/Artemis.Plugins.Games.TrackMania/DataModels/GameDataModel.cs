using Artemis.Core.Modules;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania.DataModels;

public class GameDataModel
{
    public EGameState State { get; set; }
    public string GameplayVariant { get; set; }
    public string MapName { get; set; }
    public string MapId { get; set; }

    public void Apply(in STelemetry telemetry)
    {
        State = telemetry.Game.State;
        GameplayVariant = telemetry.Game.GameplayVariant;
        MapName = telemetry.Game.MapName;
        MapId = telemetry.Game.MapId;
    }
}