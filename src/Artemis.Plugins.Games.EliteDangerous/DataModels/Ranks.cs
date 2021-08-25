using Artemis.Plugins.Modules.EliteDangerous.Journal;

namespace Artemis.Plugins.Modules.EliteDangerous.DataModels
{
    public class Ranks
    {
        public CombatRank Combat { get; internal set; }
        public TradeRank Trade { get; internal set; }
        public ExplorerRank Explorer { get; internal set; }
        public CQCRank CQC { get; internal set; }
        public EmpireRank Empire { get; internal set; }
        public FederationRank Federation { get; internal set; }

        public int CombatProgress { get; internal set; }
        public int TradeProgress { get; internal set; }
        public int ExplorerProgress { get; internal set; }
        public int CQCProgress { get; internal set; }
        public int EmpireProgress { get; internal set; }
        public int FederationProgress { get; internal set; }
    }
}
