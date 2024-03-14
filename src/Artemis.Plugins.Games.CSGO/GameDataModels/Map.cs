using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Map
{
    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("phase")]
    public string? Phase { get; set; }

    [JsonPropertyName("round")]
    public int? Round { get; set; }

    [JsonPropertyName("team_ct")]
    public Team? TeamCt { get; set; }

    [JsonPropertyName("team_t")]
    public Team? TeamT { get; set; }

    [JsonPropertyName("num_matches_to_win_series")]
    public int? NumMatchesToWinSeries { get; set; }

    [JsonPropertyName("current_spectators")]
    public int? CurrentSpectators { get; set; }

    [JsonPropertyName("souvenirs_total")]
    public int? SouvenirsTotal { get; set; }
}
