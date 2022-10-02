using Newtonsoft.Json;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Weapons
{
    [JsonProperty("weapon_0")]
    public Weapon? Weapon0 { get; set; }

    [JsonProperty("weapon_1")]
    public Weapon? Weapon1 { get; set; }

    [JsonProperty("weapon_2")]
    public Weapon? Weapon2 { get; set; }

    [JsonProperty("weapon_3")]
    public Weapon? Weapon3 { get; set; }
}