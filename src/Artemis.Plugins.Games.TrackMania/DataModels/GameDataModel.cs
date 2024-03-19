using Artemis.Core.Modules;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania.DataModels;

public class GameDataModel
{
    public EGameState State { get; set; }
    public string GameplayVariant { get; set; }
    public string MapName { get; set; }
    public string MapId { get; set; }

    public void Apply(in SGameState game)
    {
        State = game.State;
        GameplayVariant = game.GameplayVariant;
        MapName = game.MapName;
        MapId = game.MapId;
    }
}