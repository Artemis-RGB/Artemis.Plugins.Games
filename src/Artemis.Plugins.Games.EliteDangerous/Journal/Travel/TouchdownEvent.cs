using Artemis.Plugins.Modules.EliteDangerous.DataModels;

namespace Artemis.Plugins.Modules.EliteDangerous.Journal.Travel
{
    internal class TouchdownEvent : IJournalEvent
    {
        public bool PlayerControlled;
        public string NearestDestination;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            // Does not need to update IsLanded, this is done via Status.json.
            model.Navigation.DockStatus.Touchdown.Trigger(new LandingEventArgs
            {
                PlayerControlled = PlayerControlled,
                NearestDestination = NearestDestination
            });
        }
    }
}
