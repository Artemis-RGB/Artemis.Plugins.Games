using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Travel
{
    internal class LiftoffEvent : IJournalEvent
    {
        public bool PlayerControlled;
        public string NearestDestination;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.DockStatus.Liftoff.Trigger(new LandingEventArgs
            {
                PlayerControlled = PlayerControlled,
                NearestDestination = NearestDestination
            });
        }
    }
}
