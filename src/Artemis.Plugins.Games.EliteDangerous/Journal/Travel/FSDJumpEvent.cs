using Artemis.Plugins.Modules.EliteDangerous.DataModels;

namespace Artemis.Plugins.Modules.EliteDangerous.Journal.Travel
{
    internal sealed class FSDJumpEvent : IJournalEvent
    {
        public string StarSystem;
        public string Body;
        public float JumpDist;
        public float FuelUsed;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            // Does not need to update FSD statuses (charging, etc.), this is done via Status.json.
            model.Navigation.UpdateLocation(StarSystem);
            model.Ship.FSD.CompleteJump.Trigger(new CompleteJumpEventArgs
            {
                // The star class is not passed to the FSDJump event, but we can get it from the most
                // recent "StartJump" event.
                StarClass = model.Ship.FSD.StartJump.LastEventArguments.StarClass ?? default,
                JumpDistance = JumpDist,
                FuelUsed = FuelUsed
            });
        }
    }
}
