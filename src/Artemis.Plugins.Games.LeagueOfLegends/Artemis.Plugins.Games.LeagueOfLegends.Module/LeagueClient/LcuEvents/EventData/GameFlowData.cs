using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents.EventData;

public record GameFlowData
{
    [JsonProperty("gameClient")] public GameClient GameClient { get; set; }

    [JsonProperty("gameData")] public GameData GameData { get; set; }

    [JsonProperty("gameDodge")] public GameDodge GameDodge { get; set; }

    [JsonProperty("map")] public Map Map { get; set; }

    [JsonProperty("phase")] 
    [JsonConverter(typeof(StringEnumConverter))]
    public GameFlowPhase Phase { get; set; }
}

public enum GameFlowPhase
{
    None, Lobby, Matchmaking, CheckedIntoTournament, ReadyCheck, ChampSelect, GameStart, FailedToLaunch, InProgress, Reconnect, WaitingForStats, PreEndOfGame, EndOfGame, TerminatedInError
}

public record GameClient
{
    [JsonProperty("observerServerIp")] public string ObserverServerIp { get; set; }

    [JsonProperty("observerServerPort")] public int ObserverServerPort { get; set; }

    [JsonProperty("running")] public bool Running { get; set; }

    [JsonProperty("serverIp")] public string ServerIp { get; set; }

    [JsonProperty("serverPort")] public int ServerPort { get; set; }

    [JsonProperty("visible")] public bool Visible { get; set; }
}

public record GameTypeConfig
{
    [JsonProperty("advancedLearningQuests")]
    public bool AdvancedLearningQuests { get; set; }

    [JsonProperty("allowTrades")] public bool AllowTrades { get; set; }

    [JsonProperty("banMode")] public string BanMode { get; set; }

    [JsonProperty("banTimerDuration")] public int BanTimerDuration { get; set; }

    [JsonProperty("battleBoost")] public bool BattleBoost { get; set; }

    [JsonProperty("crossTeamChampionPool")]
    public bool CrossTeamChampionPool { get; set; }

    [JsonProperty("deathMatch")] public bool DeathMatch { get; set; }

    [JsonProperty("doNotRemove")] public bool DoNotRemove { get; set; }

    [JsonProperty("duplicatePick")] public bool DuplicatePick { get; set; }

    [JsonProperty("exclusivePick")] public bool ExclusivePick { get; set; }

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("learningQuests")] public bool LearningQuests { get; set; }

    [JsonProperty("mainPickTimerDuration")]
    public int MainPickTimerDuration { get; set; }

    [JsonProperty("maxAllowableBans")] public int MaxAllowableBans { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("onboardCoopBeginner")] public bool OnboardCoopBeginner { get; set; }

    [JsonProperty("pickMode")] public string PickMode { get; set; }

    [JsonProperty("postPickTimerDuration")]
    public int PostPickTimerDuration { get; set; }

    [JsonProperty("reroll")] public bool Reroll { get; set; }

    [JsonProperty("teamChampionPool")] public bool TeamChampionPool { get; set; }
}

public record QueueRewards
{
    [JsonProperty("isChampionPointsEnabled")]
    public bool IsChampionPointsEnabled { get; set; }

    [JsonProperty("isIpEnabled")] public bool IsIpEnabled { get; set; }

    [JsonProperty("isXpEnabled")] public bool IsXpEnabled { get; set; }

    [JsonProperty("partySizeIpRewards")] public List<object> PartySizeIpRewards { get; set; }
}

public record Queue
{
    [JsonProperty("allowablePremadeSizes")]
    public List<int> AllowablePremadeSizes { get; set; }

    [JsonProperty("areFreeChampionsAllowed")]
    public bool AreFreeChampionsAllowed { get; set; }

    [JsonProperty("assetMutator")] public string AssetMutator { get; set; }

    [JsonProperty("category")] public string Category { get; set; }

    [JsonProperty("championsRequiredToPlay")]
    public int ChampionsRequiredToPlay { get; set; }

    [JsonProperty("description")] public string Description { get; set; }

    [JsonProperty("detailedDescription")] public string DetailedDescription { get; set; }

    [JsonProperty("gameMode")] public string GameMode { get; set; }

    [JsonProperty("gameTypeConfig")] public GameTypeConfig GameTypeConfig { get; set; }

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("isRanked")] public bool IsRanked { get; set; }

    [JsonProperty("isTeamBuilderManaged")] public bool IsTeamBuilderManaged { get; set; }

    [JsonProperty("isTeamOnly")] public bool IsTeamOnly { get; set; }

    [JsonProperty("lastToggledOffTime")] public long LastToggledOffTime { get; set; }

    [JsonProperty("lastToggledOnTime")] public long LastToggledOnTime { get; set; }

    [JsonProperty("mapId")] public int MapId { get; set; }

    [JsonProperty("maxLevel")] public int MaxLevel { get; set; }

    [JsonProperty("maxSummonerLevelForFirstWinOfTheDay")]
    public int MaxSummonerLevelForFirstWinOfTheDay { get; set; }

    [JsonProperty("maximumParticipantListSize")]
    public int MaximumParticipantListSize { get; set; }

    [JsonProperty("minLevel")] public int MinLevel { get; set; }

    [JsonProperty("minimumParticipantListSize")]
    public int MinimumParticipantListSize { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("numPlayersPerTeam")] public int NumPlayersPerTeam { get; set; }

    [JsonProperty("queueAvailability")] public string QueueAvailability { get; set; }

    [JsonProperty("queueRewards")] public QueueRewards QueueRewards { get; set; }

    [JsonProperty("removalFromGameAllowed")]
    public bool RemovalFromGameAllowed { get; set; }

    [JsonProperty("removalFromGameDelayMinutes")]
    public int RemovalFromGameDelayMinutes { get; set; }

    [JsonProperty("shortName")] public string ShortName { get; set; }

    [JsonProperty("showPositionSelector")] public bool ShowPositionSelector { get; set; }

    [JsonProperty("spectatorEnabled")] public bool SpectatorEnabled { get; set; }

    [JsonProperty("type")] public string Type { get; set; }
}

public record GameData
{
    [JsonProperty("gameId")] public long GameId { get; set; }

    [JsonProperty("gameName")] public string GameName { get; set; }

    [JsonProperty("isCustomGame")] public bool IsCustomGame { get; set; }

    [JsonProperty("password")] public string Password { get; set; }

    [JsonProperty("playerChampionSelections")]
    public List<object> PlayerChampionSelections { get; set; }

    [JsonProperty("queue")] public Queue Queue { get; set; }

    [JsonProperty("spectatorsAllowed")] public bool SpectatorsAllowed { get; set; }

    [JsonProperty("teamOne")] public List<object> TeamOne { get; set; }

    [JsonProperty("teamTwo")] public List<object> TeamTwo { get; set; }
}

public record GameDodge
{
    [JsonProperty("dodgeIds")] public List<object> DodgeIds { get; set; }

    [JsonProperty("phase")] public string Phase { get; set; }

    [JsonProperty("state")] public string State { get; set; }
}

public record Assets
{
    [JsonProperty("champ-select-background-sound")]
    public string ChampSelectBackgroundSound { get; set; }

    [JsonProperty("champ-select-flyout-background")]
    public string ChampSelectFlyoutBackground { get; set; }

    [JsonProperty("champ-select-planning-intro")]
    public string ChampSelectPlanningIntro { get; set; }

    [JsonProperty("game-select-icon-active")]
    public string GameSelectIconActive { get; set; }

    [JsonProperty("game-select-icon-active-video")]
    public string GameSelectIconActiveVideo { get; set; }

    [JsonProperty("game-select-icon-default")]
    public string GameSelectIconDefault { get; set; }

    [JsonProperty("game-select-icon-disabled")]
    public string GameSelectIconDisabled { get; set; }

    [JsonProperty("game-select-icon-hover")]
    public string GameSelectIconHover { get; set; }

    [JsonProperty("game-select-icon-intro-video")]
    public string GameSelectIconIntroVideo { get; set; }

    [JsonProperty("gameflow-background")] public string GameflowBackground { get; set; }

    [JsonProperty("gameselect-button-hover-sound")]
    public string GameselectButtonHoverSound { get; set; }

    [JsonProperty("icon-defeat")] public string IconDefeat { get; set; }

    [JsonProperty("icon-defeat-video")] public string IconDefeatVideo { get; set; }

    [JsonProperty("icon-empty")] public string IconEmpty { get; set; }

    [JsonProperty("icon-hover")] public string IconHover { get; set; }

    [JsonProperty("icon-leaver")] public string IconLeaver { get; set; }

    [JsonProperty("icon-victory")] public string IconVictory { get; set; }

    [JsonProperty("icon-victory-video")] public string IconVictoryVideo { get; set; }

    [JsonProperty("map-north")] public string MapNorth { get; set; }

    [JsonProperty("map-south")] public string MapSouth { get; set; }

    [JsonProperty("music-inqueue-loop-sound")]
    public string MusicInqueueLoopSound { get; set; }

    [JsonProperty("parties-background")] public string PartiesBackground { get; set; }

    [JsonProperty("postgame-ambience-loop-sound")]
    public string PostgameAmbienceLoopSound { get; set; }

    [JsonProperty("ready-check-background")]
    public string ReadyCheckBackground { get; set; }

    [JsonProperty("ready-check-background-sound")]
    public string ReadyCheckBackgroundSound { get; set; }

    [JsonProperty("sfx-ambience-pregame-loop-sound")]
    public string SfxAmbiencePregameLoopSound { get; set; }

    [JsonProperty("social-icon-leaver")] public string SocialIconLeaver { get; set; }

    [JsonProperty("social-icon-victory")] public string SocialIconVictory { get; set; }
}

public record CategorizedContentBundles
{
}

public record PerPositionDisallowedSummonerSpells
{
}

public record PerPositionRequiredSummonerSpells
{
}

public record Properties
{
    [JsonProperty("suppressRunesMasteriesPerks")]
    public bool SuppressRunesMasteriesPerks { get; set; }
}

public record Map
{
    //[JsonProperty("assets")] public Assets Assets { get; set; }

    // [JsonProperty("categorizedContentBundles")] public CategorizedContentBundles CategorizedContentBundles { get; set; }

    [JsonProperty("description")] public string Description { get; set; }

    [JsonProperty("gameMode")] public string GameMode { get; set; }

    [JsonProperty("gameModeName")] public string GameModeName { get; set; }

    [JsonProperty("gameModeShortName")] public string GameModeShortName { get; set; }

    [JsonProperty("gameMutator")] public string GameMutator { get; set; }

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("isRGM")] public bool IsRGM { get; set; }

    [JsonProperty("mapStringId")] public string MapStringId { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("perPositionDisallowedSummonerSpells")]
    public PerPositionDisallowedSummonerSpells PerPositionDisallowedSummonerSpells { get; set; }

    [JsonProperty("perPositionRequiredSummonerSpells")]
    public PerPositionRequiredSummonerSpells PerPositionRequiredSummonerSpells { get; set; }

    [JsonProperty("platformId")] public string PlatformId { get; set; }

    [JsonProperty("platformName")] public string PlatformName { get; set; }

    [JsonProperty("properties")] public Properties Properties { get; set; }
}