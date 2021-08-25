using System;

namespace Artemis.Plugins.Modules.EliteDangerous.Status
{

    // List of statuses found here
    // https://elite-journal.readthedocs.io/en/latest/Status%20File/

    [Flags]
    public enum StatusFlags : uint
    {
        None = 0,
        Docked = 1 << 0,
        Landed = 1 << 1,
        LandingGearDeployed = 1 << 2,
        ShieldsUp = 1 << 3,
        Supercruise = 1 << 4,
        FlightAssistOff = 1 << 5,
        HardpointsDeployed = 1 << 6,
        InWing = 1 << 7,
        LightsOn = 1 << 8,
        CargoScoopDeployed = 1 << 9,
        SilentRunning = 1 << 10,
        FuelScooping = 1 << 11,
        SRVHandbrake = 1 << 12,
        SRVTurretView = 1 << 13,
        SRVTurretRetracted = 1 << 14,
        SRVDriveAssist = 1 << 15,
        FSDMassLocked = 1 << 16,
        FSDCharging = 1 << 17,
        FSDCooldown = 1 << 18,
        LowFuel = 1 << 19, // < 25% fuel
        Overheating = 1 << 20, // > 100% heat
        HasLatLng = 1 << 21,
        InDanger = 1 << 22,
        BeingInterdicted = 1 << 23,
        PilotingMainShip = 1 << 24,
        PilotingFighter = 1 << 25,
        PilotingSRV = 1 << 26,
        AnalysisMode = 1 << 27,
        NightVision = 1 << 28,
        AltFromAvgRadius = 1 << 29,
        FSDJump = 1 << 30,
        SRVHighBeam = 1u << 31
    }

    public enum Vehicle
    {
        Unknown,
        Ship,
        Fighter,
        SRV
    }

    public enum GuiPanel
    {
        None = 0,
        Internal = 1,
        External = 2,
        Comms = 3,
        Role = 4,
        StationServices = 5,
        GalaxyMap = 6,
        SystemMap = 7,
        Orrery = 8,
        FullSpectrumScanner = 9,
        DiscoveryScanner = 10,
        Codex = 11
    }

    public enum LegalState
    {
        Unknown,
        Clean,
        IllegalCargo,
        Speeding,
        Wanted,
        Hostile,
        PassengerWanted,
        Warrant
    }
}
