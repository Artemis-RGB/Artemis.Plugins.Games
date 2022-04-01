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
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("using System.ComponentModel.DataAnnotations;");
            builder.AppendLine("using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;");
            builder.AppendLine("");
            builder.AppendLine($"namespace {namespaceName} {{");
            builder.AppendLine($"    public enum {enumName} {{");

            foreach (var enumDef in defs)
            {
                builder.Append("        ").Append("[Display(Name = ").Append("@\"").Append(enumDef.DisplayName).Append('"').AppendLine(")]");
                if (enumDef.AlternativeNames?.Length > 0)
                {
                    builder.Append("        ").Append("[Name(")
                        .Append(string.Join(",", enumDef.AlternativeNames.Select(s => $"@\"{s}\""))).AppendLine(")]");

                }
                builder.Append("        ").Append(enumDef.Name).Append(" = ").Append(enumDef.Id).AppendLine(",");
                builder.AppendLine();
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
