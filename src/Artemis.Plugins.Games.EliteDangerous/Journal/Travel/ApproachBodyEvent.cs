using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Travel
{
    internal class ApproachBodyEvent : IJournalEvent
    {
        public string Body;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Navigation.ApproachBody.Trigger(new ApproachBodyEventArgs
            {
                BodyName = Body
            });
        }
    }
}
