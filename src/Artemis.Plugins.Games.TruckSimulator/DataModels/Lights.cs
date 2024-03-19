using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Lights
{
    public bool Parking { get; set; }
    public bool LowBeam { get; set; }
    public bool HighBeam { get; set; }

    public bool FrontAuxiliary { get; set; }
    public bool RoofAuxiliary { get; set; }

    [DataModelProperty(Description = "Whether the left blinker lights are on (i.e. represents the physical lights, not the button in the cab).")]
    public bool LeftIndicatorLightOn { get; set; }
    [DataModelProperty(Description = "Whether the right blinker lights are on (i.e. represents the physical lights, not the button in the cab).")]
    public bool RightIndicatorLightOn { get; set; }

    [DataModelProperty(Description = "Whether the driver has enabled the left indicators (i.e. represents the button in the cab, not the actual state of the indicator light).")]
    public bool LeftIndicatorActive { get; set; }
    [DataModelProperty(Description = "Whether the driver has enabled the right indicators (i.e. represents the button in the cab, not the actual state of the indicator light).")]
    public bool RightIndicatorActive { get; set; }

    [DataModelProperty(Description = "Whether the drive has enabled the hazard lights (i.e. represents the button in the cab, not the actual state of the indicator light).")]
    public bool Hazard { get; set; }

    [DataModelProperty(Description = "Whether the drive has enabled the beacon (i.e. represents the button in the cab, not the actual state of the beacon lights).")]
    public bool Beacon { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        Parking = data.parkingLights != 0;
        LowBeam = data.lowBeamLights != 0;
        HighBeam = data.highBeamLights != 0;
        FrontAuxiliary = data.lightsAuxFront != 0;
        RoofAuxiliary = data.lightsAuxRoof != 0;
        LeftIndicatorLightOn = data.blinkerLeftOn != 0;
        RightIndicatorLightOn = data.blinkerRightOn != 0;
        LeftIndicatorActive = data.blinkerLeftActive != 0;
        RightIndicatorActive = data.blinkerRightActive != 0;
        Hazard = data.hazardLightsOn != 0;
        Beacon = data.beaconOn != 0;
    }
}