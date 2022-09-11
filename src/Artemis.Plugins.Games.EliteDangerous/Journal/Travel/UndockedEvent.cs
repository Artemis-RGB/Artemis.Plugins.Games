using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Travel
{
    internal class UndockedEvent : IJournalEvent
    {
        public string StationName;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.CurrentStation = null;
            model.Navigation.DockStatus.Undocked.Trigger(new DockingEventArgs { StationName = StationName });
        }
    }
}
