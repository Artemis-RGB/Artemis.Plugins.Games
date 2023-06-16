using Newtonsoft.Json;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Team
{
    [JsonProperty("score")]
    public int? Score { get; set; }

    [JsonProperty("consecutive_round_losses")]
    public int? ConsecutiveRoundLosses { get; set; }

    [JsonProperty("timeouts_remaining")]
    public int? TimeoutsRemaining { get; set; }

    [JsonProperty("matches_won_this_series")]
    public int? MatchesWonThisSeries { get; set; }
}
