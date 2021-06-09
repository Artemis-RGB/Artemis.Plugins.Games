using Artemis.Core.DataModelExpansions;
using Artemis.Plugins.Games.GTAV.Model;
using GTA;
using SkiaSharp;

namespace Artemis.Plugins.Games.GTAV.Module.DataModels
{
    public class GTAVDataModel : DataModel
    {
        public GTAWorld World { get; set; } = new GTAWorld();
        public GTAPlayer Player { get; set; } = new GTAPlayer();
        public GTAVehicleDataModel Vehicle { get; set; } = new GTAVehicleDataModel();
    }

    public class GTAVehicleDataModel
    {
        public GTAVehicleGeneral GeneralInformation { get; set; } = new GTAVehicleGeneral();
        public GTAVehicleState State { get; set; } = new GTAVehicleState();
        public GTAVehicleEngine Engine { get; set; } = new GTAVehicleEngine();
        public GTAVehicleModsDataModel Modifications { get; set; } = new GTAVehicleModsDataModel();

        public void ApplyGSI(GTAVehicle gameStateVehicle)
        {
            GeneralInformation = gameStateVehicle.GeneralInformation;
            State = gameStateVehicle.State;
            Engine = gameStateVehicle.Engine;
            Modifications.ApplyGSI(gameStateVehicle.Modifications);
        }
    }

    public class GTAVehicleModsDataModel : GTAVehicleMods
    {
        public new SKColor CustomPrimaryColor { get; set; }
        public new SKColor CustomSecondaryColor { get; set; }
        public new SKColor NeonLightsColor { get; set; }
        public new SKColor TireSmokeColor { get; set; }

        public void ApplyGSI(GTAVehicleMods modifications)
        {
            PrimaryColor = modifications.PrimaryColor;
            SecondaryColor = modifications.SecondaryColor;
            DashboardColor = modifications.DashboardColor;
            HasNeonLights = modifications.HasNeonLights;
            IsPrimaryColorCustom = modifications.IsPrimaryColorCustom;
            IsSecondaryColorCustom = modifications.IsSecondaryColorCustom;
          
            CustomPrimaryColor = new SKColor(modifications.CustomPrimaryColor.R, modifications.CustomPrimaryColor.G, modifications.CustomPrimaryColor.B, modifications.CustomPrimaryColor.A);
            CustomSecondaryColor = new SKColor(modifications.CustomSecondaryColor.R, modifications.CustomSecondaryColor.G, modifications.CustomSecondaryColor.B, modifications.CustomSecondaryColor.A);
            NeonLightsColor = new SKColor(modifications.NeonLightsColor.R, modifications.NeonLightsColor.G, modifications.NeonLightsColor.B, modifications.NeonLightsColor.A);
            TireSmokeColor = new SKColor(modifications.TireSmokeColor.R, modifications.TireSmokeColor.G, modifications.TireSmokeColor.B, modifications.TireSmokeColor.A);
            
        }
    }
}