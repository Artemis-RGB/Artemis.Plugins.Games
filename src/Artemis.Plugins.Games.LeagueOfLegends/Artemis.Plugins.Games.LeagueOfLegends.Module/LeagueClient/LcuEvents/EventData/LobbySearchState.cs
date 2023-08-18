namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents;

public record LobbySearchState(
    object[] errors,
    LowPriorityData lowPriorityData,
    string searchState
);


public record LowPriorityData(
    string bustedLeaverAccessToken,
    object[] penalizedSummonerIds,
    double penaltyTime,
    double penaltyTimeRemaining,
    string reason
);