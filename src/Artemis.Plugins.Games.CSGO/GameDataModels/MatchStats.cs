using Newtonsoft.Json;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class MatchStats
{
    [JsonProperty("kills")]
    public int? Kills { get; set; }

    [JsonProperty("assists")]
    public int? Assists { get; set; }

    [JsonProperty("deaths")]
    public int? Deaths { get; set; }

    [JsonProperty("mvps")]
    public int? Mvps { get; set; }

    [JsonProperty("score")]
    public int? Score { get; set; }
}
