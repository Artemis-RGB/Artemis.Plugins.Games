using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Travel
{
    internal class SupercruiseEntryEvent : IJournalEvent
    {
        public string StarSystem;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            // Does not need to update InSupercruise, this is done via Status.json
            model.Navigation.UpdateLocation(StarSystem);
            model.Navigation.EnterSupercruise.Trigger();
        }
    }

    internal class SupercruiseExitEvent : IJournalEvent
    {

        public string StarSystem;
        public string Body;
        public BodyType BodyType;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            // Does not need to update InSupercruise, this is done via Status.json
            model.Navigation.UpdateLocation(StarSystem, Body, BodyType);
            model.Navigation.ExitSupercruise.Trigger();
        }
    }
}
