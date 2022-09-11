using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Travel
{
    internal class LeaveBodyEvent : IJournalEvent
    {
        public string Body;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.LeaveBody.Trigger(new ApproachBodyEventArgs
            {
                BodyName = Body
            });
        }
    }
}
