using Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents.EventData;
using JsonSubTypes;
using Newtonsoft.Json;
using static JsonSubTypes.JsonSubtypes;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient;

[JsonConverter(typeof(JsonSubtypes), nameof(Uri))]
[KnownSubType(typeof(LcuEvent<GameFlowData>), "/lol-gameflow/v1/session")]
[KnownSubType(typeof(LcuEvent<ChampSelectData>), "/lol-champ-select/v1/session")]
[FallBackSubType(typeof(LcuEvent<>))]
public interface ILcuEvent
{
    [JsonProperty("eventType")] public string EventType { get; set; }

    [JsonProperty("uri")] public string Uri { get; set; }
}

public class LcuEvent<T> : ILcuEvent
{
    [JsonProperty("data")] public T Data { get; set; }

    [JsonProperty("eventType")] public string EventType { get; set; }

    [JsonProperty("uri")] public string Uri { get; set; }
}