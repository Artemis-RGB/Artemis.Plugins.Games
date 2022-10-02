using Newtonsoft.Json;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Weapon
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("paintkit")]
    public string? Paintkit { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("ammo_clip")]
    public int? AmmoClip { get; set; }

    [JsonProperty("ammo_clip_max")]
    public int? AmmoClipMax { get; set; }

    [JsonProperty("ammo_reserve")]
    public int? AmmoReserve { get; set; }

    [JsonProperty("state")]
    public string? State { get; set; }
}
