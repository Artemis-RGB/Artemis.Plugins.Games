using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Travel
{
    // Written at startup and resurrection
    internal class LocationEvent : IJournalEvent
    {

        public string StarSystem;
        public string Body;
        public BodyType BodyType;
        public bool Docked;
        public string StationName;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.UpdateLocation(StarSystem, Body, BodyType, StationName);
        }
    }
}
