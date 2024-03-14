using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Team
{
    [JsonPropertyName("score")]
    public int? Score { get; set; }

    [JsonPropertyName("consecutive_round_losses")]
    public int? ConsecutiveRoundLosses { get; set; }

    [JsonPropertyName("timeouts_remaining")]
    public int? TimeoutsRemaining { get; set; }

    [JsonPropertyName("matches_won_this_series")]
    public int? MatchesWonThisSeries { get; set; }
}
