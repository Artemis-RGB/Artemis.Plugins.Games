using System.Linq;
using System.Text;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendDisplayNameLine(this StringBuilder sb, string displayName)
    {
        sb.Append("[Display(Name = ").Append("@\"").Append(displayName).Append('"').Append(")]").AppendLine();
        return sb;
    }

    public static StringBuilder AppendNameLine(this StringBuilder sb, string[] alternativeNames)
    {
        if (alternativeNames.Any())
            sb.Append("[Name(").Append(string.Join(",", alternativeNames.Select(s => $"@\"{s}\""))).Append(")]").AppendLine();
            
        return sb;
    }

    public static StringBuilder AppendEnumLine(this StringBuilder sb, string name, int? id)
    {
        sb.Append(name);
        if (id.HasValue)
            sb.Append(" = ").Append(id.Value);
        sb.Append(",");
        sb.AppendLine();
        return sb;
    }
}