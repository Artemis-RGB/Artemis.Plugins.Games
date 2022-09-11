using Artemis.Plugins.Games.EliteDangerous.Journal;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.EliteDangerous.Utils
{
    internal static class ShipTypeDefinitions
    {
        private static readonly ShipDefinition unknownShipDefinition = new(ShipType.Unknown, ShipSize.Unknown);

        // Sourced from: https://github.com/EDCD/EDMarketConnector/blob/main/companion.py
        // Seems to be missing the guardian fighters (Trident, Javelin, Lance)
        private static readonly Dictionary<string, ShipDefinition> shipDefintions = new()
        {
            ["adder"] = new(ShipType.Adder, ShipSize.Small),
            ["anaconda"] = new(ShipType.Anaconda, ShipSize.Large),
            ["asp"] = new(ShipType.Asp_Explorer, ShipSize.Medium),
            ["asp_scout"] = new(ShipType.Asp_Scout, ShipSize.Medium),
            ["belugaliner"] = new(ShipType.Beluga_Liner, ShipSize.Large),
            ["cobramkiii"] = new(ShipType.Cobra_MkIII, ShipSize.Small),
            ["cobramkiv"] = new(ShipType.Cobra_MkIV, ShipSize.Small),
            ["clipper"] = new(ShipType.Panther_Clipper, ShipSize.Small),
            ["cutter"] = new(ShipType.Imperial_Cutter, ShipSize.Large),
            ["diamondback"] = new(ShipType.Diamondback_Scout, ShipSize.Small),
            ["diamondbackxl"] = new(ShipType.Diamondback_Explorer, ShipSize.Small),
            ["dolphin"] = new(ShipType.Dolphin, ShipSize.Small),
            ["eagle"] = new(ShipType.Eagle, ShipSize.Small),
            ["empire_courier"] = new(ShipType.Imperial_Courier, ShipSize.Small),
            ["empire_eagle"] = new(ShipType.Imperial_Eagle, ShipSize.Small),
            ["empire_trader"] = new(ShipType.Imperial_Clipper, ShipSize.Large),
            ["federation_corvette"] = new(ShipType.Federal_Corvette, ShipSize.Large),
            ["federation_dropship"] = new(ShipType.Federal_Dropship, ShipSize.Medium),
            ["federation_dropship_mkii"] = new(ShipType.Federal_Assault_Ship, ShipSize.Medium),
            ["federation_gunship"] = new(ShipType.Federal_Gunship, ShipSize.Medium),
            ["federation_fighter"] = new(ShipType.F63_Condor, ShipSize.Fighter),
            ["ferdelance"] = new(ShipType.Fer_de_Lance, ShipSize.Medium),
            ["empire_fighter"] = new(ShipType.Gu_97, ShipSize.Fighter),
            ["hauler"] = new(ShipType.Hauler, ShipSize.Small),
            ["independant_trader"] = new(ShipType.Keelback, ShipSize.Medium),
            ["independent_fighter"] = new(ShipType.Taipan_Fighter, ShipSize.Fighter),
            ["krait_mkii"] = new(ShipType.Krait_MkII, ShipSize.Medium),
            ["krait_light"] = new(ShipType.Krait_Phantom, ShipSize.Medium),
            ["mamba"] = new(ShipType.Mamba, ShipSize.Medium),
            ["orca"] = new(ShipType.Orca, ShipSize.Large),
            ["python"] = new(ShipType.Python, ShipSize.Medium),
            ["scout"] = new(ShipType.Taipan_Fighter, ShipSize.Fighter),
            ["sidewinder"] = new(ShipType.Sidewinder, ShipSize.Small),
            ["testbuggy"] = new(ShipType.Scarab, ShipSize.Small),
            ["type6"] = new(ShipType.Type6_Transporter, ShipSize.Medium),
            ["type7"] = new(ShipType.Type7_Transporter, ShipSize.Large),
            ["type9"] = new(ShipType.Type9_Heavy, ShipSize.Large),
            ["type9_military"] = new(ShipType.Type10_Defender, ShipSize.Large),
            ["typex"] = new(ShipType.Alliance_Chieftain, ShipSize.Medium),
            ["typex_2"] = new(ShipType.Alliance_Crusader, ShipSize.Medium),
            ["typex_3"] = new(ShipType.Alliance_Challenger, ShipSize.Medium),
            ["viper"] = new(ShipType.Viper_MkIII, ShipSize.Small),
            ["viper_mkiv"] = new(ShipType.Viper_MkIV, ShipSize.Small),
            ["vulture"] = new(ShipType.Vulture, ShipSize.Small)
        };

        public static ShipDefinition GetById(string shipTypeId) =>
            shipDefintions.TryGetValue(shipTypeId, out var def) ? def : unknownShipDefinition;
    }

    internal record ShipDefinition(ShipType type, ShipSize size);
}
