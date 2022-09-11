
// http://edcodex.info/?m=doc

using System.ComponentModel;

namespace Artemis.Plugins.Games.EliteDangerous.Journal
{
    public enum StarClass
    {
        O, B, A, F, G, K, M, L, T, Y, // Main sequence
        TTS, AeBe, // Proto
        W, WN, WNC, WC, WO, // Wolf-Rayet
        CS, C, CN, CJ, CH, CHd, // Carbon
        MS, S,
        D, DA, DAB, DAO, DAZ, DAV, DB, DBZ, DBV, DO, DOV, DQ, DC, DCV, DX, // White dwarfs
        N, // Neutron
        H, // Black hole
        X, // Exotic
        SupermassiveBlackHole,
        A_BlueWhiteSuperGiant,
        F_WhiteSuperGiant,
        M_RedSuperGiant,
        M_RedGiant,
        K_OrangeGiant,
        RoguePlanet,
        Nebula,
        StellarRemnantNebula
    }

    public enum JumpType
    {
        Supercruise,
        Hyperspace
    }

    public enum BodyType
    {
        Null,
        Star,
        Planet,
        PlanetaryRing,
        StellarRing,
        Station,
        AsteroidCluster
    }

    public enum ShipType
    {
        Adder,
        Alliance_Challenger,
        Alliance_Chieftain,
        Alliance_Crusader,
        Anaconda,
        Asp_Explorer,
        Asp_Scout,
        Beluga_Liner,
        Cobra_MkIII,
        Cobra_MkIV,
        Diamondback_Explorer,
        Diamondback_Scout,
        Dolphin,
        Eagle,
        F63_Condor,
        Federal_Assault_Ship,
        Federal_Corvette,
        Federal_Dropship,
        Federal_Gunship,
        [Description("Fer-de-Lance")] Fer_de_Lance,
        Hauler,
        Imperial_Clipper,
        Imperial_Courier,
        Imperial_Cutter,
        Imperial_Eagle,
        Gu_97,
        Keelback,
        Krait_MkII,
        Krait_Phantom,
        Mamba,
        Orca,
        Panther_Clipper,
        Python,
        Scarab,
        Sidewinder,
        Taipan_Fighter,
        [Description("Type-6 Transporter")] Type6_Transporter,
        [Description("Type-7 Transporter")] Type7_Transporter,
        [Description("Type-9 Heavy")] Type9_Heavy,
        [Description("Type-10 Defender")] Type10_Defender,
        Viper_MkIII,
        Viper_MkIV,
        Vulture,
        Unknown
    }

    public enum ShipSize
    {
        Small,
        Medium,
        Large,
        Fighter,
        Unknown
    }

    public enum DockingStatus
    {
        Cancelled,
        Granted,
        Denied,
        Pending,
        Timeout
    }

    public enum DockingDenyReason
    {
        NoSpace,
        TooLarge,
        Hostile,
        Offences,
        Distance,
        ActiveFighter,
        NoReason
    }

    public enum CombatRank
    {
        Harmless = 0,
        MostlyHarmless = 1,
        Novice = 2,
        Competent = 3,
        Expert = 4,
        Master = 5,
        Dangerous = 6,
        Deadly = 7,
        Elite = 8
    }

    public enum TradeRank
    {
        Penniless = 0,
        MostlyPenniless = 1,
        Peddler = 2,
        Dealer = 3,
        Merchant = 4,
        Broker = 5,
        Entrepreneur = 6,
        Tycoon = 7,
        Elite = 8
    }

    public enum ExplorerRank
    {
        Aimless = 0,
        MostlyAimless = 1,
        Scout = 2,
        Surveyor = 3,
        Trailblazer = 4,
        Pathfinder = 5,
        Ranger = 6,
        Pioneer = 7,
        Elite = 8
    }

    public enum CQCRank
    {
        Helpless = 0,
        MostlyHelpless = 1,
        Amateur = 2,
        SemiProfessional = 3,
        Professional = 4,
        Champion = 5,
        Hero = 6,
        Legend = 7,
        Elite = 8
    }

    public enum EmpireRank
    {
        None = 0,
        Outsider = 1,
        Serf = 2,
        Master = 3,
        Squire = 4,
        Knight = 5,
        Lord = 6,
        Baron = 7,
        Viscount = 8,
        Count = 9,
        Earl = 10,
        Marquis = 11,
        Duke = 12,
        Prince = 13,
        King = 14
    }

    public enum FederationRank
    {
        None = 0,
        Recruit = 1,
        Cadet = 2,
        Midshipman = 3,
        PettyOfficer = 4,
        ChiefPettyOfficer = 5,
        WarrantOfficer = 6,
        Ensign = 7,
        Lieutenant = 8,
        LieutenantCommander = 9,
        PostCommander = 10,
        PostCaptain = 11,
        RearAdmiral = 12,
        ViceAdmiral = 13,
        Admiral = 14
    }
}
