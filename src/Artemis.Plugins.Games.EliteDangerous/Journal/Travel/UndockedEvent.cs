using Artemis.Plugins.Modules.EliteDangerous.DataModels;

namespace Artemis.Plugins.Modules.EliteDangerous.Journal.Travel
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
