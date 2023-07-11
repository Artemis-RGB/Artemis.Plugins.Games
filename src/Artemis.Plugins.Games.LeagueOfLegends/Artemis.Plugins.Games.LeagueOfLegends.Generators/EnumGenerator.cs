using System.Collections.Generic;
using System.Text;

namespace Artemis.Plugins.Games.LeagueOfLegends.Generators;

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
            builder.Append(TAB).AppendDisplayNameLine(enumDef.DisplayName);
            builder.Append(TAB).AppendNameLine(enumDef.AlternativeNames);
            builder.Append(TAB).AppendEnumLine(enumDef.Name, enumDef.Id);
            builder.AppendLine();
        }
        builder.AppendLine("}");
        return builder.ToString();
    }
}