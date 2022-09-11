namespace Artemis.Plugins.Games.EliteDangerous.Status
{
    public sealed class StatusJson
    {
        public StatusFlags Flags;
        public int[] Pips = new[] { 0, 0, 0 };
        public int FireGroup;
        public GuiPanel GuiFocus;
        public Fuel Fuel = new Fuel();
        public float Cargo;
        public LegalState LegalState;
        public double? Latitude;
        public double? Longitude;
        public double? Altitude;
        public double? Heading;
    }

    public sealed class Fuel
    {
        public float FuelMain;
        public float FuelReservoir;
    }
}
