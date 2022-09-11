using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Startup
{
    // Occurs at game startup
    // Represents how far the player is to ranking up (0 -> 100).
    internal sealed class ProgressEvent : IJournalEvent
    {
        public int Combat;
        public int Trade;
        public int Explore;
        public int CQC;
        public int Empire;
        public int Federation;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Player.Ranks.CombatProgress = Combat;
            model.Player.Ranks.TradeProgress = Trade;
            model.Player.Ranks.ExplorerProgress = Explore;
            model.Player.Ranks.CQCProgress = CQC;
            model.Player.Ranks.EmpireProgress = Empire;
            model.Player.Ranks.FederationProgress = Federation;
        }
    }
}
