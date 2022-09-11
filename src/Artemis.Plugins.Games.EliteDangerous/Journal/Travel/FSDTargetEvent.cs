using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Travel
{
    internal class FSDTargetEvent : IJournalEvent
    {
        public string Name;
        public StarClass StarClass;
        public int RemainingJumpsInRoute;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.RemainingJumpsInRoute = RemainingJumpsInRoute;
            model.Navigation.FSDTarget.Trigger();
        }
    }
}
