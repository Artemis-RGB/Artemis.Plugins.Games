using Newtonsoft.Json;

namespace Artemis.Plugins.Games.Dota2.DataModels.Nodes;

[JsonObject]
public class Abilities
{
    public Ability Ability1 { get; set; } = null!;
    public Ability Ability2 { get; set; } = null!;
    public Ability Ability3 { get; set; } = null!;
    public Ability UltimateAbility { get; set; } = null!;
    public Ability Ability4 { get; set; } = null!;
    public Ability Ability5 { get; set; } = null!;
}