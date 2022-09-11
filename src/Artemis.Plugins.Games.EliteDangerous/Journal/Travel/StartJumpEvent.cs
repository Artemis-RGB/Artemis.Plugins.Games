using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Travel
{
    internal sealed class StartJumpEvent : IJournalEvent
    {
        public JumpType JumpType;
        public string StarSystem;
        public StarClass? StarClass; // Only when JumpType = Hyperspace

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            // Does not need to update FSD statuses (charging, cooldown, etc), this is done via Status.json.
            model.Ship.FSD.StartJump.Trigger(new StartJumpEventArgs
            {
                JumpType = JumpType,
                StarSystem = StarSystem,
                StarClass = StarClass
            });
        }
    }
}
