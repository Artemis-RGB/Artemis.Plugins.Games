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