using Newtonsoft.Json;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class State
{
    [JsonProperty("health")]
    public int? Health { get; set; }

    [JsonProperty("armor")]
    public int? Armor { get; set; }

    [JsonProperty("helmet")]
    public bool? Helmet { get; set; }

    [JsonProperty("defusekit")]
    public bool? DefuseKit { get; set; }

    [JsonProperty("flashed")]
    public int? Flashed { get; set; }

    [JsonProperty("smoked")]
    public int? Smoked { get; set; }

    [JsonProperty("burning")]
    public int? Burning { get; set; }

    [JsonProperty("money")]
    public int? Money { get; set; }

    [JsonProperty("round_kills")]
    public int? RoundKills { get; set; }

    [JsonProperty("round_killhs")]
    public int? RoundKillHeadshot { get; set; }

    [JsonProperty("equip_value")]
    public int? EquipValue { get; set; }
}
