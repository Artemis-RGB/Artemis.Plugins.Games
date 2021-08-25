using Artemis.Plugins.Modules.EliteDangerous.DataModels;

namespace Artemis.Plugins.Modules.EliteDangerous.Journal.Combat
{
    internal class DiedEvent : IJournalEvent
    {
        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Player.Died.Trigger();
        }
    }
}
