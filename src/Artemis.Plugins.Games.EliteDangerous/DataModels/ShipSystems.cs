using Artemis.Core.Modules;

namespace Artemis.Plugins.Games.EliteDangerous.DataModels
{
    public class ShipSystems
    {
        public bool ShieldsActive { get; internal set; }

        public bool HardpointsDeployed { get; internal set; }
        public bool LandingGearDeployed { get; internal set; }
        public bool CargoScoopDeployed { get; internal set; }

        [DataModelProperty(Description = "When the ship's heat level is above 100%.")]
        public bool IsOverheating { get; internal set; }

        public bool LightsActive { get; internal set; }
        public bool SilentRunningActive { get; internal set; }
        public bool FlightAssistActive { get; internal set; }

        [DataModelProperty(Description = "The number of power pips diverted to systems. Value between 0 and 4. May be a half value.", MinValue = 0f, MaxValue = 4f)]
        public float SystemPips { get; internal set; }

        [DataModelProperty(Description = "The number of power pips diverted to engines. Value between 0 and 4. May be a half value.", MinValue = 0f, MaxValue = 4f)]
        public float EnginePips { get; internal set; }

        [DataModelProperty(Description = "The number of power pips diverted to weapons. Value between 0 and 4. May be a half value.", MinValue = 0f, MaxValue = 4f)]
        public float WeaponPips { get; internal set; }
    }
}
