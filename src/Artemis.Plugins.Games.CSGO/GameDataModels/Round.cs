using Newtonsoft.Json;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Round
{
    [JsonProperty("phase")]
    public string? Phase { get; set; }
    
    [JsonProperty("win_team")]
    public string? WinTeam { get; set; }
}
