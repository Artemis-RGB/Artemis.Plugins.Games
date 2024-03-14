using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.Dota2.DataModels.Nodes;

public enum AbilityUsability
{
    Unleveled,
    Passive,
    Usable,
    OnCooldown,
}

/// <summary>
/// Class representing ability information
/// </summary>
public class Ability
{
    public static readonly Ability EmptyAbility = new();
    
    /// <summary>
    /// Ability name
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Ability level
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// A boolean representing whether the ability can be casted
    /// </summary>
    [JsonPropertyName("can_cast")]
    public bool CanCast { get; set; }

    /// <summary>
    /// A boolean representing whether the ability is passive
    /// </summary>
    public bool Passive { get; set; }

    /// <summary>
    /// A boolean representing whether the ability is active
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Ability cooldown
    /// </summary>
    public int Cooldown { get; set; }

    /// <summary>
    /// A boolean representing whether the ability is an ultimate
    /// </summary>
    public bool Ultimate { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AbilityUsability Usability {
        get
        {
            if (Level == 0)
            {
                return AbilityUsability.Unleveled;
            }
            if (Passive)
            {
                return AbilityUsability.Passive;
            }
            return Cooldown > 0 ? AbilityUsability.OnCooldown : AbilityUsability.Usable;
        }
    }
}