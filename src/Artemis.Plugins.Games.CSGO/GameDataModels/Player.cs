using Newtonsoft.Json;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Player
{
    [JsonProperty("steamid")]
    public string? SteamId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("observer_slot")]
    public int? ObserverSlot { get; set; }

    [JsonProperty("team")]
    public string? Team { get; set; }

    [JsonProperty("activity")]
    public string? Activity { get; set; }

    [JsonProperty("match_stats")]
    public MatchStats? MatchStats { get; set; }

    [JsonProperty("state")]
    public State? State { get; set; }

    [JsonProperty("weapons")]
    public Weapons? Weapons { get; set; }
}
