using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Player
{
    [JsonPropertyName("steamid")]
    public string? SteamId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("observer_slot")]
    public int? ObserverSlot { get; set; }

    [JsonPropertyName("team")]
    public string? Team { get; set; }

    [JsonPropertyName("activity")]
    public string? Activity { get; set; }

    [JsonPropertyName("match_stats")]
    public MatchStats? MatchStats { get; set; }

    [JsonPropertyName("state")]
    public State? State { get; set; }

    [JsonPropertyName("weapons")]
    public Weapons? Weapons { get; set; }
}
