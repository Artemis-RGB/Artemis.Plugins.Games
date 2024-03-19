namespace Artemis.Plugins.Games.TruckSimulator.Conversions;

/// <summary>
/// Conversions for converting speed formats.
/// </summary>
internal static class SpeedConversions
{
    /// <summary>
    /// Converts the given value from meters per second to kilometers per hour.
    /// </summary>
    public static float MsToKph(this float ms) => ms * 3.6f;

    /// <summary>
    /// Converts the given value from meters per second to miles per hour.
    /// </summary>
    public static float MsToMph(this float ms) => ms * 2.237f;
}