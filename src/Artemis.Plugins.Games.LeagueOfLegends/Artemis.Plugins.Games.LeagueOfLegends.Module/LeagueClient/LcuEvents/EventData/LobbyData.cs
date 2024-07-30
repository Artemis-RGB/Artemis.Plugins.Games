namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents.EventData;

public record LobbyData(
    bool canStartActivity,
    GameConfig gameConfig,
    Invitations[] invitations,
    LocalMember localMember,
    Members[] members,
    MucJwtDto mucJwtDto,
    string multiUserChatId,
    string multiUserChatPassword,
    string partyId,
    string partyType,
    object[] restrictions,
    object[] scarcePositions,
    object[] warnings
);

public record GameConfig(
    int[] allowablePremadeSizes,
    string customLobbyName,
    string customMutatorName,
    object[] customRewardsDisabledReasons,
    string customSpectatorPolicy,
    object[] customSpectators,
    object[] customTeam100,
    object[] customTeam200,
    string gameMode,
    bool isCustom,
    bool isLobbyFull,
    bool isTeamBuilderManaged,
    int mapId,
    int maxHumanPlayers,
    int maxLobbySize,
    int maxTeamSize,
    string pickType,
    bool premadeSizeAllowed,
    int queueId,
    bool shouldForceScarcePositionSelection,
    bool showPositionSelector
);

public record Invitations(
    string invitationId,
    string invitationType,
    string state,
    string timestamp,
    long toSummonerId,
    string toSummonerName
);

public record LocalMember(
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
    long summonerId,
    string summonerInternalName,
    int summonerLevel,
    string summonerName,
    int teamId
);

public record Members(
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
    long summonerId,
    string summonerInternalName,
    int summonerLevel,
    string summonerName,
    int teamId
);

public record MucJwtDto(
    string channelClaim,
    string domain,
    string jwt,
    string targetRegion
);