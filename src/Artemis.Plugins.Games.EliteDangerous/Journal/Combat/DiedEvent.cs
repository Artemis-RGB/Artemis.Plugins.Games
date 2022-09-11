using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Combat
{
    internal class DiedEvent : IJournalEvent
    {
        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Player.Died.Trigger();
        }
    }
}
