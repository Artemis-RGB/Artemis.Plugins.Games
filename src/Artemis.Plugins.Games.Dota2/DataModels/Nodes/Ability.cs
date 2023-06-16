using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class Ability
{
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