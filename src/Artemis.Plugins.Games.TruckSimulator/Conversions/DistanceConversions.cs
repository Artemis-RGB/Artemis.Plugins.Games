namespace Artemis.Plugins.Games.TruckSimulator.Conversions
{
    /// <summary>
    /// Conversions for converting distance formats.
    /// </summary>
    internal static class DistanceConversions
    {
        /// <summary>
        /// Converts the given value from meters to kilometers.
        /// </summary>
        public static float MToKm(this float m) => m / 1000f;

        /// <summary>
        /// Converts the given value from meters to miles.
        /// </summary>
        public static float MToMi(this float m) => m / 0.0006213f;

        /// <summary>
        /// Converts the given value from kilometers to miles.
        /// </summary>
        public static float KmToMi(this float ms) => ms * 0.621f;
    }
}
