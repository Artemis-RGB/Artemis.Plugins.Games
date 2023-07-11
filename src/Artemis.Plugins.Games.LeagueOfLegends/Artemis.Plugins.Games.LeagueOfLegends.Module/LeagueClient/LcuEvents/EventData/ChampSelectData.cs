using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents.EventData;

public class ChampSelectData
{
    [JsonProperty("actions")] public Action[][] Actions { get; set; }

    [JsonProperty("allowBattleBoost")] public bool AllowBattleBoost { get; set; }

    [JsonProperty("allowDuplicatePicks")] public bool AllowDuplicatePicks { get; set; }

    [JsonProperty("allowLockedEvents")] public bool AllowLockedEvents { get; set; }

    [JsonProperty("allowRerolling")] public bool AllowRerolling { get; set; }

    [JsonProperty("allowSkinSelection")] public bool AllowSkinSelection { get; set; }

    [JsonProperty("bans")] public Bans Bans { get; set; }

    [JsonProperty("benchChampionIds")] public int[] BenchChampionIds { get; set; }

    [JsonProperty("benchEnabled")] public bool BenchEnabled { get; set; }

    [JsonProperty("boostableSkinCount")] public int BoostableSkinCount { get; set; }

    [JsonProperty("chatDetails")] public ChatDetails ChatDetails { get; set; }

    [JsonProperty("counter")] public int Counter { get; set; }

    [JsonProperty("entitledFeatureState")] public EntitledFeatureState EntitledFeatureState { get; set; }

    [JsonProperty("gameId")] public int GameId { get; set; }

    [JsonProperty("hasSimultaneousBans")] public bool HasSimultaneousBans { get; set; }

    [JsonProperty("hasSimultaneousPicks")] public bool HasSimultaneousPicks { get; set; }

    [JsonProperty("isCustomGame")] public bool IsCustomGame { get; set; }

    [JsonProperty("isSpectating")] public bool IsSpectating { get; set; }

    [JsonProperty("localPlayerCellId")] public int LocalPlayerCellId { get; set; }

    [JsonProperty("lockedEventIndex")] public int LockedEventIndex { get; set; }

    [JsonProperty("myTeam")] public Player[] MyTeam { get; set; }

    [JsonProperty("recoveryCounter")] public int RecoveryCounter { get; set; }

    [JsonProperty("rerollsRemaining")] public int RerollsRemaining { get; set; }

    [JsonProperty("skipChampionSelect")] public bool SkipChampionSelect { get; set; }

    [JsonProperty("theirTeam")] public Player[] TheirTeam { get; set; }

    [JsonProperty("timer")] public Timer Timer { get; set; }

    [JsonProperty("trades")] public Trade[] Trades { get; set; }
}

public class Bans
{
    [JsonProperty("myTeamBans")] public int[] MyTeamBans { get; set; }

    [JsonProperty("numBans")] public int NumBans { get; set; }

    [JsonProperty("theirTeamBans")] public int[] TheirTeamBans { get; set; }
}

public class ChatDetails
{
    [JsonProperty("chatRoomName")] public string ChatRoomName { get; set; }

    [JsonProperty("chatRoomPassword")] public string ChatRoomPassword { get; set; }
}

public class EntitledFeatureState
{
    [JsonProperty("additionalRerolls")] public int AdditionalRerolls { get; set; }

    [JsonProperty("unlockedSkinIds")] public int[] UnlockedSkinIds { get; set; }
}

public class Player
{
    [JsonProperty("assignedPosition")] public string AssignedPosition { get; set; }

    [JsonProperty("cellId")] public int CellId { get; set; }

    [JsonProperty("championId")] public int ChampionId { get; set; }

    [JsonProperty("championPickIntent")] public int ChampionPickIntent { get; set; }

    [JsonProperty("entitledFeatureType")] public string EntitledFeatureType { get; set; }

    [JsonProperty("selectedSkinId")] public int SelectedSkinId { get; set; }

    [JsonProperty("spell1Id")] public int Spell1Id { get; set; }

    [JsonProperty("spell2Id")] public int Spell2Id { get; set; }

    [JsonProperty("summonerId")] public int SummonerId { get; set; }

    [JsonProperty("team")] public int Team { get; set; }

    [JsonProperty("wardSkinId")] public int WardSkinId { get; set; }
}

public class Timer
{
    [JsonProperty("adjustedTimeLeftInPhase")]
    public int AdjustedTimeLeftInPhase { get; set; }

    [JsonProperty("internalNowInEpochMs")] public long InternalNowInEpochMs { get; set; }

    [JsonProperty("isInfinite")] public bool IsInfinite { get; set; }

    [JsonProperty("phase")] public string Phase { get; set; }

    [JsonProperty("totalTimeInPhase")] public int TotalTimeInPhase { get; set; }
}

public class Trade
{
    [JsonProperty("cellId")] public int CellId { get; set; }

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("state")] public string State { get; set; }
}

public class Action
{
    [JsonProperty("actorCellId")] public int ActorCellId { get; set; }

    [JsonProperty("championId")] public int ChampionId { get; set; }

    [JsonProperty("completed")] public bool Completed { get; set; }

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("isAllyAction")] public bool IsAllyAction { get; set; }

    [JsonProperty("isInProgress")] public bool IsInProgress { get; set; }

    [JsonProperty("pickTurn")] public int PickTurn { get; set; }

    [JsonProperty("type")] public string Type { get; set; }
}