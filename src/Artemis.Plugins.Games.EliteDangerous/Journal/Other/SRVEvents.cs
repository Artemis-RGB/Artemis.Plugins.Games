using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Other
{
    internal class DockSRVEvent : IJournalEvent
    {
        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.SRV.Docked.Trigger();
        }
    }

    internal class LaunchSRVEvent : IJournalEvent
    {
        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.SRV.Launched.Trigger();
        }
    }
}
