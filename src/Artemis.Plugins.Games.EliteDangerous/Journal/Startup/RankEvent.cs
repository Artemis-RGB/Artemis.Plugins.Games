using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Startup
{
    // Occurs at game startup
    internal sealed class RankEvent : IJournalEvent
    {
        public CombatRank Combat;
        public TradeRank Trade;
        public ExplorerRank Explore;
        public CQCRank CQC;
        public EmpireRank Empire;
        public FederationRank Federation;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Player.Ranks.Combat = Combat;
            model.Player.Ranks.Trade = Trade;
            model.Player.Ranks.Explorer = Explore;
            model.Player.Ranks.CQC = CQC;
            model.Player.Ranks.Empire = Empire;
            model.Player.Ranks.Federation = Federation;
        }
    }
}
