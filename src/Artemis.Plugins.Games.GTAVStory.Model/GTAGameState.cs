using System.Drawing;
using GTA;

namespace Artemis.Plugins.Games.GTAVStory.Model
{
    public class GTAGameState
    {
        public GTAWorld World { get; set; } = new GTAWorld();
        public GTAPlayer Player { get; set; } = new GTAPlayer();
        public GTAVehicle Vehicle { get; set; } = new GTAVehicle();
    }

    public class GTAWorld
    {
        public bool IsPaused { get; set; }
        public bool IsLoading { get; set; }
        public bool IsCutsceneActive { get; set; }
        public bool IsMissionActive { get; set; }
        public bool IsRandomEventActive { get; set; }
        public bool IsWaypointActive { get; set; }
        public int MaxWantedLevel { get; set; }
        public RadioStation RadioStation { get; set; }
    }

    public class GTAPlayer
    {
        public PedHash Model { get; set; }
        public bool CanControlCharacter { get; set; }
        public bool IsDriving { get; set; }
        public bool IsAiming { get; set; }
        public bool IsAlive { get; set; }
        public bool IsClimbing { get; set; }
        public int Money { get; set; }
        public int WantedLevel { get; set; }
        public int MaxArmor { get; set; }
        public ParachuteTint PrimaryParachuteTint { get; set; }
        public ParachuteTint ReserveParachuteTint { get; set; }
        public float RemainingSprintTime { get; set; }
        public float RemainingSprintStamina { get; set; }
        public float RemainingUnderwaterTime { get; set; }
        public bool IsSpecialAbilityActive { get; set; }
        public bool IsSpecialAbilityEnabled { get; set; }
        public bool IsNightVisionActive { get; set; }
        public bool IsThermalVisionActive { get; set; }
    }

    public class GTAVehicle
    {
        public GTAVehicleGeneral GeneralInformation { get; set; } = new GTAVehicleGeneral();
        public GTAVehicleState State { get; set; } = new GTAVehicleState();
        public GTAVehicleEngine Engine { get; set; } = new GTAVehicleEngine();
        public GTAVehicleMods Modifications { get; set; } = new GTAVehicleMods();
    }

    public class GTAVehicleMods
    {
        public VehicleColor PrimaryColor { get; set; }
        public VehicleColor SecondaryColor { get; set; }
        public VehicleColor DashboardColor { get; set; }
        public bool HasNeonLights { get; set; }
        public bool IsPrimaryColorCustom { get; set; }
        public bool IsSecondaryColorCustom { get; set; }

        public Color CustomPrimaryColor { get; set; }
        public Color CustomSecondaryColor { get; set; }
        public Color NeonLightsColor { get; set; }
        public Color TireSmokeColor { get; set; }
    }

    public class GTAVehicleEngine
    {
        public bool IsEngineRunning { get; set; }
        public bool IsEngineStarting { get; set; }
        public int Gears { get; set; }
        public int HighGear { get; set; }
        public int CurrentGear { get; set; }
        public float CurrentRPM { get; set; }
        public float Turbo { get; set; }
    }

    public class GTAVehicleState
    {
        public float BodyHealth { get; set; }
        public float EngineHealth { get; set; }
        public float PetrolTankHealth { get; set; }
        public float DirtLevel { get; set; }
        public float WheelSpeedMph { get; set; }
        public float WheelSpeedKph { get; set; }
        public bool IsAlarmSounding { get; set; }
        public bool IsSirenActive { get; set; }
        public bool AreLightsOn { get; set; }
        public bool AreHighBeamsOn { get; set; }
        public bool IsInteriorLightOn { get; set; }
        public bool IsSearchLightOn { get; set; }
        public bool IsTaxiLightOn { get; set; }
        public bool IsInBurnout { get; set; }
        public int PassengerCount { get; set; }
        public int PassengerCapacity { get; set; }
    }

    public class GTAVehicleGeneral
    {
        public string DisplayName { get; set; }
        public string LocalizedName { get; set; }
        public string ClassDisplayName { get; set; }
        public string ClassLocalizedName { get; set; }
        public VehicleClass ClassType { get; set; }

        public bool IsConvertible { get; set; }
        public bool IsBig { get; set; }
        public bool HasBulletProofGlass { get; set; }
        public bool HasLowriderHydraulics { get; set; }
        public bool HasDonkHydraulics { get; set; }
        public bool HasParachute { get; set; }
        public bool HasRocketBoost { get; set; }
        public bool CanJump { get; set; }
        public bool HasSiren { get; set; }
    }
}