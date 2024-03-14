using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class MatchStats
{
    [JsonPropertyName("kills")]
    public int? Kills { get; set; }

    [JsonPropertyName("assists")]
    public int? Assists { get; set; }

    [JsonPropertyName("deaths")]
    public int? Deaths { get; set; }

    [JsonPropertyName("mvps")]
    public int? Mvps { get; set; }

    [JsonPropertyName("score")]
    public int? Score { get; set; }
}
