using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

//adapted from https://stackoverflow.com/questions/30526757
[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
internal class NameAttribute : Attribute
{
    public readonly string[] Names;

    public NameAttribute(params string[] names)
    {
        if (names?.Any(x => x == null) ?? false) throw new ArgumentNullException(nameof(names));

        Names = names.Distinct().ToArray();
    }
}

internal static class ParseEnum<TEnum> where TEnum : struct, Enum
{
    private static readonly Dictionary<string, TEnum> _cache =
        Enum.GetValues<TEnum>()
            .Select(e => (Enum.GetName(e), e)) //select the regular enum names
            .Concat(typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static) //and concat with the custom ones
                .Select(fieldInfo => (Att: fieldInfo.GetCustomAttribute<NameAttribute>(), EnumValue: (TEnum)fieldInfo.GetValue(null)))
                .Where(x => x.Att != null)
                .SelectMany(tp => tp.Att.Names, (tp, name) => (name, tp.EnumValue)))
            .ToDictionary(a => a.Item1, a => a.Item2); //create a dictionary from all of them

    internal static TEnum TryParseOr(string value, TEnum defaultValue, bool ignoreCase = false)
    {
        //this should be None for all enums in this plugin
        if (string.IsNullOrWhiteSpace(value))
            return default;

        if (_cache.TryGetValue(value, out var result))
            return result;

        if (Enum.TryParse<TEnum>(value, ignoreCase, out var parseResult))
            return parseResult;

        return defaultValue;
    }
}