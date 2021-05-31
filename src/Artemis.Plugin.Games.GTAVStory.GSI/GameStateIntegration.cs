using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Artemis.Plugins.Games.GTAVStory.Model;
using GTA;
using Newtonsoft.Json;

namespace Artemis.Plugins.Games.GTAVStory.GSI
{
    public class GameStateIntegration : Script, IDisposable
    {
        private bool _enabled;
        private string _url;
        private HttpClient _client;
        private DateTime _lastException;
        private DateTime _lastUpdate;

        public GameStateIntegration()
        {
            Interval = 40;
            Tick += OnTick;

            var webServerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Artemis", "webserver.txt");
            if (!File.Exists(webServerPath))
                _enabled = false;
            else
            {
                _url = File.ReadAllText(webServerPath) + "plugins/97e39a06-2efe-4271-889f-fc41a8ab2d03/Update";
                _client = new HttpClient();
                _enabled = true;
            }
        }

        private async void OnTick(object sender, EventArgs e)
        {
            if (!_enabled || DateTime.UtcNow - _lastUpdate < TimeSpan.FromMilliseconds(40) || DateTime.UtcNow - _lastException < TimeSpan.FromSeconds(5))
                return;

            _lastUpdate = DateTime.UtcNow;

            var gameState = new GTAGameState();

            gameState.World.IsPaused = Game.IsPaused;
            gameState.World.IsLoading = Game.IsLoading;
            gameState.World.IsCutsceneActive = Game.IsCutsceneActive;
            gameState.World.IsMissionActive = Game.IsMissionActive;
            gameState.World.IsRandomEventActive = Game.IsRandomEventActive;
            gameState.World.IsWaypointActive = Game.IsWaypointActive;
            gameState.World.MaxWantedLevel = Game.MaxWantedLevel;
            gameState.World.RadioStation = Game.RadioStation;

            gameState.Player.Model = (PedHash) Game.Player.Character.Model.Hash;
            gameState.Player.CanControlCharacter = Game.Player.CanControlCharacter;
            gameState.Player.IsDriving = Game.Player.Character.CurrentVehicle != null;
            gameState.Player.IsAiming = Game.Player.IsAiming;
            gameState.Player.IsAlive = Game.Player.IsAlive;
            gameState.Player.IsClimbing = Game.Player.IsClimbing;
            gameState.Player.Money = Game.Player.Money;
            gameState.Player.WantedLevel = Game.Player.WantedLevel;
            gameState.Player.MaxArmor = Game.Player.MaxArmor;
            gameState.Player.PrimaryParachuteTint = Game.Player.PrimaryParachuteTint;
            gameState.Player.ReserveParachuteTint = Game.Player.ReserveParachuteTint;
            gameState.Player.RemainingSprintTime = Game.Player.RemainingSprintTime;
            gameState.Player.RemainingSprintStamina = Game.Player.RemainingSprintStamina;
            gameState.Player.RemainingUnderwaterTime = Game.Player.RemainingUnderwaterTime;
            gameState.Player.IsSpecialAbilityActive = Game.Player.IsSpecialAbilityActive;
            gameState.Player.IsSpecialAbilityEnabled = Game.Player.IsSpecialAbilityEnabled;
            gameState.Player.IsNightVisionActive = Game.IsNightVisionActive;
            gameState.Player.IsThermalVisionActive = Game.IsThermalVisionActive;

            gameState.Vehicle.GeneralInformation.DisplayName = Game.Player.LastVehicle.DisplayName;
            gameState.Vehicle.GeneralInformation.LocalizedName = Game.Player.LastVehicle.LocalizedName;
            gameState.Vehicle.GeneralInformation.ClassDisplayName = Game.Player.LastVehicle.ClassDisplayName;
            gameState.Vehicle.GeneralInformation.ClassLocalizedName = Game.Player.LastVehicle.ClassLocalizedName;
            gameState.Vehicle.GeneralInformation.ClassType = Game.Player.LastVehicle.ClassType;
            gameState.Vehicle.GeneralInformation.IsConvertible = Game.Player.LastVehicle.IsConvertible;
            gameState.Vehicle.GeneralInformation.IsBig = Game.Player.LastVehicle.IsBig;
            gameState.Vehicle.GeneralInformation.HasBulletProofGlass = Game.Player.LastVehicle.HasBulletProofGlass;
            gameState.Vehicle.GeneralInformation.HasLowriderHydraulics = Game.Player.LastVehicle.HasLowriderHydraulics;
            gameState.Vehicle.GeneralInformation.HasDonkHydraulics = Game.Player.LastVehicle.HasDonkHydraulics;
            gameState.Vehicle.GeneralInformation.HasParachute = Game.Player.LastVehicle.HasParachute;
            gameState.Vehicle.GeneralInformation.HasRocketBoost = Game.Player.LastVehicle.HasRocketBoost;
            gameState.Vehicle.GeneralInformation.CanJump = Game.Player.LastVehicle.CanJump;
            gameState.Vehicle.GeneralInformation.HasSiren = Game.Player.LastVehicle.HasSiren;

            gameState.Vehicle.State.DirtLevel = Game.Player.LastVehicle.DirtLevel;
            gameState.Vehicle.State.BodyHealth = Game.Player.LastVehicle.BodyHealth;
            gameState.Vehicle.State.EngineHealth = Game.Player.LastVehicle.EngineHealth;
            gameState.Vehicle.State.PetrolTankHealth = Game.Player.LastVehicle.PetrolTankHealth;
            gameState.Vehicle.State.WheelSpeedMph = Game.Player.LastVehicle.WheelSpeed * 2.51f;
            gameState.Vehicle.State.WheelSpeedKph = gameState.Vehicle.State.WheelSpeedMph * 1.61f;
            gameState.Vehicle.State.IsSirenActive = Game.Player.LastVehicle.IsSirenActive;
            gameState.Vehicle.State.AreLightsOn = Game.Player.LastVehicle.AreLightsOn;
            gameState.Vehicle.State.AreHighBeamsOn = Game.Player.LastVehicle.AreHighBeamsOn;
            gameState.Vehicle.State.IsInteriorLightOn = Game.Player.LastVehicle.IsInteriorLightOn;
            gameState.Vehicle.State.IsSearchLightOn = Game.Player.LastVehicle.IsSearchLightOn;
            gameState.Vehicle.State.IsTaxiLightOn = Game.Player.LastVehicle.IsTaxiLightOn;
            gameState.Vehicle.State.IsInBurnout = Game.Player.LastVehicle.IsInBurnout;
            gameState.Vehicle.State.PassengerCount = Game.Player.LastVehicle.PassengerCount;
            gameState.Vehicle.State.PassengerCapacity = Game.Player.LastVehicle.PassengerCapacity;

            gameState.Vehicle.Engine.IsEngineRunning = Game.Player.LastVehicle.IsEngineRunning;
            gameState.Vehicle.Engine.IsEngineStarting = Game.Player.LastVehicle.IsEngineStarting;
            gameState.Vehicle.Engine.Gears = Game.Player.LastVehicle.Gears;
            gameState.Vehicle.Engine.HighGear = Game.Player.LastVehicle.HighGear;
            gameState.Vehicle.Engine.CurrentGear = Game.Player.LastVehicle.CurrentGear;
            gameState.Vehicle.Engine.Turbo = Game.Player.LastVehicle.Turbo;
            gameState.Vehicle.Engine.CurrentRPM = Game.Player.LastVehicle.CurrentRPM;
            gameState.Vehicle.State.IsAlarmSounding = Game.Player.LastVehicle.IsAlarmSounding;

            gameState.Vehicle.Modifications.PrimaryColor = Game.Player.LastVehicle.Mods.PrimaryColor;
            gameState.Vehicle.Modifications.SecondaryColor = Game.Player.LastVehicle.Mods.SecondaryColor;
            gameState.Vehicle.Modifications.DashboardColor = Game.Player.LastVehicle.Mods.DashboardColor;
            gameState.Vehicle.Modifications.HasNeonLights = Game.Player.LastVehicle.Mods.HasNeonLights;
            gameState.Vehicle.Modifications.IsPrimaryColorCustom = Game.Player.LastVehicle.Mods.IsPrimaryColorCustom;
            gameState.Vehicle.Modifications.IsSecondaryColorCustom = Game.Player.LastVehicle.Mods.IsSecondaryColorCustom;
            gameState.Vehicle.Modifications.CustomPrimaryColor = Game.Player.LastVehicle.Mods.CustomPrimaryColor;
            gameState.Vehicle.Modifications.CustomSecondaryColor = Game.Player.LastVehicle.Mods.CustomSecondaryColor;
            gameState.Vehicle.Modifications.NeonLightsColor = Game.Player.LastVehicle.Mods.NeonLightsColor;
            gameState.Vehicle.Modifications.TireSmokeColor = Game.Player.LastVehicle.Mods.TireSmokeColor;

            try
            {
                var json = JsonConvert.SerializeObject(gameState);
                await _client.PostAsync(_url, new StringContent(json, Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {
                _lastException = DateTime.UtcNow;
                try
                {
                    Logger.Log($"Exception while POSTing to {_url} \r\n {ex}");
                }
                catch
                {
                    // too bad, whatever
                }
            }
        }

        public void Dispose()
        {
            Tick -= OnTick;

            _client?.Dispose();
            _client = null;
        }
    }
}