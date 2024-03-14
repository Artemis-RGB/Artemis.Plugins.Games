using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Weapons
{
    [JsonPropertyName("weapon_0")]
    public Weapon? Weapon0 { get; set; }

    [JsonPropertyName("weapon_1")]
    public Weapon? Weapon1 { get; set; }

    [JsonPropertyName("weapon_2")]
    public Weapon? Weapon2 { get; set; }

    [JsonPropertyName("weapon_3")]
    public Weapon? Weapon3 { get; set; }
}