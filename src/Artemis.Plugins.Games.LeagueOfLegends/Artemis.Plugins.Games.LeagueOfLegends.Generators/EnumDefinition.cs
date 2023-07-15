namespace Artemis.Plugins.Games.LeagueOfLegends.Generators;

public class EnumDefinition
{
    public EnumDefinition(string name, string displayName, int? id, params string[] alternativeNames)
    {
        Name = name;
        DisplayName = displayName;
        Id = id;
        AlternativeNames = alternativeNames;
    }

    public string Name { get; set; }
    public int? Id { get; set; }
    public string DisplayName { get; set; }
    public string[] AlternativeNames { get; set; }
}