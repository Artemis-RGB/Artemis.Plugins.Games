using Artemis.Core.Modules;

namespace Artemis.Plugins.Games.EliteDangerous.DataModels
{
    public class Fuel
    {
        [DataModelProperty(Name = "Fuel (Main)", Description = "Amount of fuel in the main fuel tank (thick bar on the HUD).")]
        public float FuelMain { get; internal set; }

        [DataModelProperty(Name = "Fuel (Reservoir)", Description = "Amount of fuel in the reservoir fuel tank (thin bar on HUD).")]
        public float FuelReservoir { get; internal set; }

        [DataModelProperty(Description = "If the ship currently has less than 25% fuel.")]
        public bool IsLow { get; internal set; }

        [DataModelProperty(Description = "If the fuel scoop is currently deployed and gathering fuel from a star.")]
        public bool IsScooping { get; internal set; }
    }
}
