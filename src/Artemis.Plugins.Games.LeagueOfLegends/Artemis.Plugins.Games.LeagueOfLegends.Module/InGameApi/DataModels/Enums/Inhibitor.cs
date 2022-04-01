using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums
{
    public enum Inhibitor
    {
        Unknown = -1,
        None = 0,
        [Name("Barracks_T2_L1")]
        BlueTopInhibitor,
        [Name("Barracks_T2_C1")]
        BlueMidInhibitor,
        [Name("Barracks_T2_R1")]
        BlueBotInhibitor,
        [Name("Barracks_T1_L1")]
        RedTopInhibitor,
        [Name("Barracks_T1_C1")]
        RedMidInhibitor,
        [Name("Barracks_T1_R1")]
        RedBotInhibitor,
    }
}
