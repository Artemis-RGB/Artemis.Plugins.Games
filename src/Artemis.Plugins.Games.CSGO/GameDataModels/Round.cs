using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Round
{
    [JsonPropertyName("phase")]
    public string? Phase { get; set; }
    
    [JsonPropertyName("win_team")]
    public string? WinTeam { get; set; }
}
