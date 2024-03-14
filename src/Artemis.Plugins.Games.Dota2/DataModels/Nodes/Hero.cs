using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.Dota2.DataModels.Nodes;

/// <summary>
/// Class representing hero information
/// </summary>
public class HeroDota2
{
    /// <summary>
    /// Hero ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Hero name
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Hero level
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is alive
    /// </summary>
    public bool Alive { get; set; }

    /// <summary>
    /// Amount of seconds until the hero respawns
    /// </summary>
    [JsonPropertyName("respawn_seconds")]
    public int SecondsToRespawn { get; set; }

    /// <summary>
    /// The buyback cost
    /// </summary>
    public int BuybackCost { get; set; }

    /// <summary>
    /// The buyback cooldown
    /// </summary>
    public int BuybackCooldown { get; set; }

    /// <summary>
    /// Hero health
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// Hero max health
    /// </summary>
    public int MaxHealth { get; set; }

    /// <summary>
    /// Hero health percentage
    /// </summary>
    public int HealthPercent { get; set; }

    /// <summary>
    /// Hero mana
    /// </summary>
    public int Mana { get; set; }

    /// <summary>
    /// Hero max mana
    /// </summary>
    public int MaxMana { get; set; }

    /// <summary>
    /// Hero mana percent
    /// </summary>
    public int ManaPercent { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is silenced
    /// </summary>
    public bool Silenced { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is stunned
    /// </summary>
    public bool Stunned { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is disarmed
    /// </summary>
    public bool Disarmed { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is magic immune
    /// </summary>
    /// 
    [JsonPropertyName("magicimmune")]
    public bool MagicImmune { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is hexed
    /// </summary>
    [JsonPropertyName("hexed")]
    public bool Hexed { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is muted
    /// </summary>
    public bool Muted { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is broken
    /// </summary>
    public bool Break { get; set; }

    /// <summary>
    /// A boolean representing whether the hero has Aghanim's Scepter
    /// </summary>
    [JsonPropertyName("aghanims_scepter")]
    public bool HasScepter { get; set; }

    /// <summary>
    /// A boolean representing whether the hero has Aghanim's Shard
    /// </summary>
    [JsonPropertyName("aghanims_shard")]
    public bool HasShard { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is under smoke effect
    /// </summary>
    public bool Smoked { get; set; }

    /// <summary>
    /// A boolean representing whether the hero is debuffed
    /// </summary>
    public bool HasDebuff { get; set; }

    [JsonPropertyName("talent_1")]
    public bool HasRightTalent1 { get; set; }
    [JsonPropertyName("talent_2")]
    public bool HasLeftTalent1 { get; set; }
    [JsonPropertyName("talent_3")]
    public bool HasRightTalent2 { get; set; }
    [JsonPropertyName("talent_4")]
    public bool HasLeftTalent2 { get; set; }
    [JsonPropertyName("talent_5")]
    public bool HasRightTalent3 { get; set; }
    [JsonPropertyName("talent_6")]
    public bool HasLeftTalent3 { get; set; }
    [JsonPropertyName("talent_7")]
    public bool HasRightTalent4 { get; set; }
    [JsonPropertyName("talent_8")]
    public bool HasLeftTalent4 { get; set; }
}