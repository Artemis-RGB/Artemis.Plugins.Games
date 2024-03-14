using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class State
{
    [JsonPropertyName("health")]
    public int? Health { get; set; }

    [JsonPropertyName("armor")]
    public int? Armor { get; set; }

    [JsonPropertyName("helmet")]
    public bool? Helmet { get; set; }

    [JsonPropertyName("defusekit")]
    public bool? DefuseKit { get; set; }

    [JsonPropertyName("flashed")]
    public int? Flashed { get; set; }

    [JsonPropertyName("smoked")]
    public int? Smoked { get; set; }

    [JsonPropertyName("burning")]
    public int? Burning { get; set; }

    [JsonPropertyName("money")]
    public int? Money { get; set; }

    [JsonPropertyName("round_kills")]
    public int? RoundKills { get; set; }

    [JsonPropertyName("round_killhs")]
    public int? RoundKillHeadshot { get; set; }

    [JsonPropertyName("equip_value")]
    public int? EquipValue { get; set; }
}
