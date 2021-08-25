using Artemis.Plugins.Modules.EliteDangerous.DataModels;

namespace Artemis.Plugins.Modules.EliteDangerous.Journal.Other
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
