using Artemis.Plugins.Modules.EliteDangerous.DataModels;

namespace Artemis.Plugins.Modules.EliteDangerous.Journal.Travel
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
