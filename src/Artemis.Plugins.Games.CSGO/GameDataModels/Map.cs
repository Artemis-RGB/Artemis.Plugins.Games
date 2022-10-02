using Newtonsoft.Json;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Map
{
    [JsonProperty("mode")]
    public string? Mode { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("phase")]
    public string? Phase { get; set; }

    [JsonProperty("round")]
    public int? Round { get; set; }

    [JsonProperty("team_ct")]
    public Team? TeamCt { get; set; }

    [JsonProperty("team_t")]
    public Team? TeamT { get; set; }

    [JsonProperty("num_matches_to_win_series")]
    public int? NumMatchesToWinSeries { get; set; }

    [JsonProperty("current_spectators")]
    public int? CurrentSpectators { get; set; }

    [JsonProperty("souvenirs_total")]
    public int? SouvenirsTotal { get; set; }
}
