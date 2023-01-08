using System;
using Artemis.Plugins.Games.EliteDangerous.DataModels;
using Artemis.Plugins.Games.EliteDangerous.Utils;
using Newtonsoft.Json;
using System.IO;

namespace Artemis.Plugins.Games.EliteDangerous.Status
{
    internal class StatusParser : FileReaderBase
    {
        private readonly string dataDirectory;

        public StatusParser(string dataDirectory) : base(false)
        {
            this.dataDirectory = dataDirectory;
        }

        public override void Activate() => OpenFile(Path.Combine(dataDirectory, "Status.json"));

        protected override void OnContentRead(EliteDangerousDataModel dataModel, string content)
        {
            var trimmed = content.TrimEnd(Environment.NewLine.ToCharArray());
            var status = JsonConvert.DeserializeObject<StatusJson>(trimmed);
            if (status == null) return;

            bool Has(StatusFlags flag) => (status.Flags & flag) != 0;

            //
            // Update the datamodel based on the parsed values
            //

            // Player details
            dataModel.Player.CurrentlyPiloting =
                  Has(StatusFlags.PilotingMainShip) ? Vehicle.Ship
                : Has(StatusFlags.PilotingFighter) ? Vehicle.Fighter
                : Has(StatusFlags.PilotingSRV) ? Vehicle.SRV
                : Vehicle.Unknown;
            dataModel.Player.LegalState = status.LegalState;
            dataModel.Player.InWing = Has(StatusFlags.InWing);

            // HUD
            dataModel.HUD.FocusedPanel = status.GuiFocus;
            dataModel.HUD.AnalysisModeActive = Has(StatusFlags.AnalysisMode);
            dataModel.HUD.NightVisionActive = Has(StatusFlags.NightVision);

            // Nav
            dataModel.Navigation.DockStatus.IsDocked = Has(StatusFlags.Docked);
            dataModel.Navigation.DockStatus.IsLanded = Has(StatusFlags.Landed);
            dataModel.Navigation.Latitude = status.Latitude;
            dataModel.Navigation.Longitude = status.Longitude;
            dataModel.Navigation.Altitude = status.Altitude;
            dataModel.Navigation.Heading = status.Heading;

            // Ship
            dataModel.Ship.IsInSupercruise = Has(StatusFlags.Supercruise);
            dataModel.Ship.Systems.LandingGearDeployed = Has(StatusFlags.LandingGearDeployed);
            dataModel.Ship.Systems.CargoScoopDeployed = Has(StatusFlags.CargoScoopDeployed);
            dataModel.Ship.Systems.HardpointsDeployed = Has(StatusFlags.HardpointsDeployed);
            dataModel.Ship.Systems.ShieldsActive = Has(StatusFlags.ShieldsUp);
            dataModel.Ship.Systems.FlightAssistActive = !Has(StatusFlags.FlightAssistOff);
            dataModel.Ship.Systems.LightsActive = Has(StatusFlags.PilotingMainShip) && Has(StatusFlags.LightsOn);
            dataModel.Ship.Systems.SilentRunningActive = Has(StatusFlags.SilentRunning);
            dataModel.Ship.Systems.IsOverheating = Has(StatusFlags.Overheating);
            dataModel.Ship.IsInDanger = Has(StatusFlags.InDanger);
            dataModel.Ship.IsBeingInterdicted = Has(StatusFlags.BeingInterdicted);

            // Ship power
            dataModel.Ship.Systems.SystemPips = status.Pips[0] / 2f;
            dataModel.Ship.Systems.EnginePips = status.Pips[1] / 2f;
            dataModel.Ship.Systems.WeaponPips = status.Pips[2] / 2f;

            // Ship FSD
            dataModel.Ship.FSD.IsCharging = Has(StatusFlags.FSDCharging);
            dataModel.Ship.FSD.IsJumping = Has(StatusFlags.FSDJump);
            dataModel.Ship.FSD.IsCoolingDown = Has(StatusFlags.FSDCooldown);
            dataModel.Ship.FSD.IsMassLocked = Has(StatusFlags.FSDMassLocked);

            // Ship fuel
            dataModel.Ship.Fuel.FuelMain = status.Fuel.FuelMain;
            dataModel.Ship.Fuel.FuelReservoir = status.Fuel.FuelReservoir;
            dataModel.Ship.Fuel.IsLow = Has(StatusFlags.LowFuel);
            dataModel.Ship.Fuel.IsScooping = Has(StatusFlags.FuelScooping);

            // SRV
            dataModel.SRV.HandbrakeActive = Has(StatusFlags.SRVHandbrake);
            dataModel.SRV.TurretViewActive = Has(StatusFlags.SRVTurretView);
            dataModel.SRV.TurretRetracted = Has(StatusFlags.SRVTurretRetracted);
            dataModel.SRV.DriveAssistActive = Has(StatusFlags.SRVDriveAssist);
            dataModel.SRV.LightsActive = Has(StatusFlags.PilotingSRV) && Has(StatusFlags.LightsOn);
            dataModel.SRV.HighBeamActive = Has(StatusFlags.SRVHighBeam);
        }
    }
}
