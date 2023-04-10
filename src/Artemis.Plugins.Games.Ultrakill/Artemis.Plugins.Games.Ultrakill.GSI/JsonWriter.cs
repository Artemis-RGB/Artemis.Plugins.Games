using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Artemis.Plugins.Games.Ultrakill.GSI
{
    internal static class JsonWriter
    {
        private static void AppendPropertyName(this StringBuilder b, string name)
        {
            b.Append('"');
            b.Append(name);
            b.Append('"');

            b.Append(':');
        }

        private static void AppendTypeAndValueInternal(this StringBuilder b, string name, string value, bool quotes)
        {
            b.AppendPropertyName(name);

            if (quotes)
                b.Append('"');

            b.Append(value);

            if (quotes)
                b.Append('"');
        }

        internal static void AppendTypeAndValue(this StringBuilder b, string name, string value)
            => AppendTypeAndValueInternal(b, name, value, true);

        internal static void AppendTypeAndValue(this StringBuilder b, string name, float value)
            => AppendTypeAndValueInternal(b, name, value.ToString(CultureInfo.InvariantCulture), false);

        internal static void AppendTypeAndValue(this StringBuilder b, string name, int value)
            => AppendTypeAndValueInternal(b, name, value.ToString(CultureInfo.InvariantCulture), false);

        internal static void AppendTypeAndValue(this StringBuilder b, string name, byte value)
            => AppendTypeAndValueInternal(b, name, value.ToString(CultureInfo.InvariantCulture), false);

        internal static void AppendTypeAndValue(this StringBuilder b, string name, bool value)
            => AppendTypeAndValueInternal(b, name, value ? "true" : "false", false);

        internal static void AppendTypeAndValue(this StringBuilder b, string name, IEnumerable<string> values)
        {
            b.AppendPropertyName(name);

            b.Append('[');

            foreach (var item in values)
            {
                b.Append('"');
                b.Append(item);
                b.Append('"');

                b.Append(',');
            }
            //remove trailing comma
            if (values.Any())
                b.Remove(b.Length - 1, 1);

            b.Append(']');
        }

        internal static void AppendTypeAndValue(this StringBuilder b, string name, Color value)
        {
            b.AppendPropertyName(name);

            b.Append("{");
            b.AppendTypeAndValue("Red", (byte)(value.r * 255));
            b.Append(',');
            b.AppendTypeAndValue("Green", (byte)(value.g * 255));
            b.Append(',');
            b.AppendTypeAndValue("Blue", (byte)(value.b * 255));
            b.Append("}");
        }
    }
}
