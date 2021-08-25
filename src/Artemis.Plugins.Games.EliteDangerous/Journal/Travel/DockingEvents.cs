using Artemis.Plugins.Modules.EliteDangerous.DataModels;

namespace Artemis.Plugins.Modules.EliteDangerous.Journal.Travel
{
    internal class DockedEvent : IJournalEvent
    {
        public string StationName;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            // Does not need to update IsDocked, this is done via Status.json.
            model.Navigation.CurrentStation = StationName;
            model.Navigation.DockStatus.Docked.Trigger(new DockingEventArgs
            {
                StationName = StationName
            });
        }
    }

    internal class DockingCancelledEvent : IJournalEvent
    {
        public string StationName;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.DockStatus.RequestStatusChanged.Trigger(new DockingRequestStatusChangedEventArgs
            {
                StationName = StationName,
                Status = DockingStatus.Cancelled
            });
        }
    }

    internal class DockingDeniedEvent : IJournalEvent
    {
        public string StationName;
        public DockingDenyReason Reason;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.DockStatus.RequestStatusChanged.Trigger(new DockingRequestStatusChangedEventArgs
            {
                StationName = StationName,
                Status = DockingStatus.Denied,
                DenyReason = Reason
            });
        }
    }

    internal class DockingGrantedEvent : IJournalEvent
    {
        public string StationName;
        public int LandingPad;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.DockStatus.RequestStatusChanged.Trigger(new DockingRequestStatusChangedEventArgs
            {
                StationName = StationName,
                Status = DockingStatus.Granted,
                LandingPad = LandingPad
            });
        }
    }

    internal class DockingRequestedEvent : IJournalEvent
    {
        public string StationName;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.DockStatus.RequestStatusChanged.Trigger(new DockingRequestStatusChangedEventArgs
            {
                StationName = StationName,
                Status = DockingStatus.Pending
            });
        }
    }

    internal class DockingTimeoutEvent : IJournalEvent
    {
        public string StationName;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.DockStatus.RequestStatusChanged.Trigger(new DockingRequestStatusChangedEventArgs
            {
                StationName = StationName,
                Status = DockingStatus.Timeout
            });
        }
    }
}
