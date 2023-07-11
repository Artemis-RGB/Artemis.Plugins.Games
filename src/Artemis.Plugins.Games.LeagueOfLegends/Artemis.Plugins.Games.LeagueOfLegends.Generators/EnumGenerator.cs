using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators
{
    public class EnumDefinition
    {
        public string Name { get; set; }
        public int? Id { get; set; }
        public string DisplayName { get; set; }
        public string[] AlternativeNames { get; set; }
    }

    public static class EnumGenerator
    {
        public static string GetEnum(string namespaceName, string enumName, IEnumerable<EnumDefinition> defs)
        {
            const string TAB = "    ";

            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("using System.ComponentModel.DataAnnotations;");
            builder.AppendLine("using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;");
            builder.AppendLine("");
            builder.AppendLine($"namespace {namespaceName};");
            builder.AppendLine("");
            
            
            builder.AppendLine($"public enum {enumName} {{");
            foreach (var enumDef in defs)
            {
                builder.Append(TAB).AppendDisplayName(enumDef.DisplayName).AppendLine();
                builder.Append(TAB).AppendName(enumDef.AlternativeNames).AppendLine();
                builder.Append(TAB).AppendEnum(enumDef.Name, enumDef.Id).AppendLine();
                builder.AppendLine();
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }

    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendDisplayName(this StringBuilder sb, string displayName)
        {
            sb.Append("[Display(Name = ").Append("@\"").Append(displayName).Append('"').Append(")]");
            return sb;
        }

        public static StringBuilder AppendName(this StringBuilder sb, string[] alternativeNames)
        {
            if (alternativeNames.Any())
                sb.Append("[Name(").Append(string.Join(",", alternativeNames.Select(s => $"@\"{s}\""))).Append(")]");
            
            return sb;
        }

        public static StringBuilder AppendEnum(this StringBuilder sb, string name, int? id)
        {
            sb.Append(name);
            if (id.HasValue)
                sb.Append(" = ").Append(id);
            sb.Append(",");
            return sb;
        }
    }
}