using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class Weapon
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("paintkit")]
    public string? Paintkit { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("ammo_clip")]
    public int? AmmoClip { get; set; }

    [JsonPropertyName("ammo_clip_max")]
    public int? AmmoClipMax { get; set; }

    [JsonPropertyName("ammo_reserve")]
    public int? AmmoReserve { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }
}
