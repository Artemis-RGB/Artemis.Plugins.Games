using System;

namespace Artemis.Plugins.Games.TruckSimulator.Conversions;
// Taken from https://github.com/RenCloud/scs-sdk-plugin/blob/master/scs-client/C%23/SCSSdkClient/Object/SCSTelemetry.Methods.cs

/// <summary>
/// Conversion methods for converting raw telemetry data into in-game time.
/// </summary>
internal static class DateTimeConversions
{
    public static DateTime ToGameDateTime(this uint minutes) => new((long)minutes * 10_000_000 * 60, DateTimeKind.Utc); // 10 million ticks in a second * 60 minutes in a second
}