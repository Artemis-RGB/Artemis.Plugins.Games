using Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents.EventData;
using JsonSubTypes;
using Newtonsoft.Json;
using static JsonSubTypes.JsonSubtypes;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents;

[JsonConverter(typeof(JsonSubtypes), nameof(Uri))]
[KnownSubType(typeof(LcuEvent<GameFlowData>), "/lol-gameflow/v1/session")]
[KnownSubType(typeof(LcuEvent<ChampSelectData>), "/lol-champ-select/v1/session")]
[KnownSubType(typeof(LcuEvent<LobbyData>), "/lol-lobby/v2/lobby")]
[KnownSubType(typeof(LcuEvent<LobbyMember[]>), "/lol-lobby/v2/lobby/members")]
[KnownSubType(typeof(LcuEvent<LobbySearchState>), "/lol-lobby/v2/lobby/matchmaking/search-state")]
[FallBackSubType(typeof(LcuEvent<object>))]
public class LcuEvent
{
    [JsonProperty("eventType")] public string EventType { get; set; }

    [JsonProperty("uri")] public string Uri { get; set; }
}

public class LcuEvent<T> : LcuEvent
{
    [JsonProperty("data")] public T Data { get; set; }
}

public class LobbyData
{
    public bool canStartActivity { get; set; }
    public GameConfig gameConfig { get; set; }
    public Invitations[] invitations { get; set; }
    public LocalMember localMember { get; set; }
    public Members[] members { get; set; }
    public MucJwtDto mucJwtDto { get; set; }
    public string multiUserChatId { get; set; }
    public string multiUserChatPassword { get; set; }
    public string partyId { get; set; }
    public string partyType { get; set; }
    public object[] restrictions { get; set; }
    public object[] scarcePositions { get; set; }
    public object[] warnings { get; set; }
}

public class GameConfig
{
    public int[] allowablePremadeSizes { get; set; }
    public string customLobbyName { get; set; }
    public string customMutatorName { get; set; }
    public object[] customRewardsDisabledReasons { get; set; }
    public string customSpectatorPolicy { get; set; }
    public object[] customSpectators { get; set; }
    public object[] customTeam100 { get; set; }
    public object[] customTeam200 { get; set; }
    public string gameMode { get; set; }
    public bool isCustom { get; set; }
    public bool isLobbyFull { get; set; }
    public bool isTeamBuilderManaged { get; set; }
    public int mapId { get; set; }
    public int maxHumanPlayers { get; set; }
    public int maxLobbySize { get; set; }
    public int maxTeamSize { get; set; }
    public string pickType { get; set; }
    public bool premadeSizeAllowed { get; set; }
    public int queueId { get; set; }
    public bool shouldForceScarcePositionSelection { get; set; }
    public bool showPositionSelector { get; set; }
}

public class Invitations
{
    public string invitationId { get; set; }
    public string invitationType { get; set; }
    public string state { get; set; }
    public string timestamp { get; set; }
    public int toSummonerId { get; set; }
    public string toSummonerName { get; set; }
}

public class LocalMember
{
    public bool allowedChangeActivity { get; set; }
    public bool allowedInviteOthers { get; set; }
    public bool allowedKickOthers { get; set; }
    public bool allowedStartActivity { get; set; }
    public bool allowedToggleInvite { get; set; }
    public bool autoFillEligible { get; set; }
    public bool autoFillProtectedForPromos { get; set; }
    public bool autoFillProtectedForSoloing { get; set; }
    public bool autoFillProtectedForStreaking { get; set; }
    public int botChampionId { get; set; }
    public string botDifficulty { get; set; }
    public string botId { get; set; }
    public string firstPositionPreference { get; set; }
    public bool isBot { get; set; }
    public bool isLeader { get; set; }
    public bool isSpectator { get; set; }
    public int primaryChampionPreference { get; set; }
    public string puuid { get; set; }
    public bool ready { get; set; }
    public string secondPositionPreference { get; set; }
    public int secondaryChampionPreference { get; set; }
    public bool showGhostedBanner { get; set; }
    public int summonerIconId { get; set; }
    public int summonerId { get; set; }
    public string summonerInternalName { get; set; }
    public int summonerLevel { get; set; }
    public string summonerName { get; set; }
    public int teamId { get; set; }
}

public class Members
{
    public bool allowedChangeActivity { get; set; }
    public bool allowedInviteOthers { get; set; }
    public bool allowedKickOthers { get; set; }
    public bool allowedStartActivity { get; set; }
    public bool allowedToggleInvite { get; set; }
    public bool autoFillEligible { get; set; }
    public bool autoFillProtectedForPromos { get; set; }
    public bool autoFillProtectedForSoloing { get; set; }
    public bool autoFillProtectedForStreaking { get; set; }
    public int botChampionId { get; set; }
    public string botDifficulty { get; set; }
    public string botId { get; set; }
    public string firstPositionPreference { get; set; }
    public bool isBot { get; set; }
    public bool isLeader { get; set; }
    public bool isSpectator { get; set; }
    public int primaryChampionPreference { get; set; }
    public string puuid { get; set; }
    public bool ready { get; set; }
    public string secondPositionPreference { get; set; }
    public int secondaryChampionPreference { get; set; }
    public bool showGhostedBanner { get; set; }
    public int summonerIconId { get; set; }
    public int summonerId { get; set; }
    public string summonerInternalName { get; set; }
    public int summonerLevel { get; set; }
    public string summonerName { get; set; }
    public int teamId { get; set; }
}

public class MucJwtDto
{
    public string channelClaim { get; set; }
    public string domain { get; set; }
    public string jwt { get; set; }
    public string targetRegion { get; set; }
}

public record LobbyMember(
    bool allowedChangeActivity,
    bool allowedInviteOthers,
    bool allowedKickOthers,
    bool allowedStartActivity,
    bool allowedToggleInvite,
    bool autoFillEligible,
    bool autoFillProtectedForPromos,
    bool autoFillProtectedForSoloing,
    bool autoFillProtectedForStreaking,
    int botChampionId,
    string botDifficulty,
    string botId,
    string firstPositionPreference,
    bool isBot,
    bool isLeader,
    bool isSpectator,
    int primaryChampionPreference,
    string puuid,
    bool ready,
    string secondPositionPreference,
    int secondaryChampionPreference,
    bool showGhostedBanner,
    int summonerIconId,
    int summonerId,
    string summonerInternalName,
    int summonerLevel,
    string summonerName,
    int teamId
);

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

