using Artemis.Plugins.Games.EliteDangerous.DataModels;

namespace Artemis.Plugins.Games.EliteDangerous.Journal.Combat
{
    internal class EscapeInterdictionEvent : IJournalEvent
    {
        public string Interdictor;
        public bool IsPlayer;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Ship.Interdiction.Trigger(new InterdictionEventArgs
            {
                Interdictor = Interdictor,
                InterdictorIsPlayer = IsPlayer,
                Escaped = true,
                Submitted = false
            });
        }
    }

    internal class Interdicted : IJournalEvent
    {
        public string Interdictor;
        public bool IsPlayer;
        public bool Submitted;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Ship.Interdiction.Trigger(new InterdictionEventArgs
            {
                Interdictor = Interdictor,
                InterdictorIsPlayer = IsPlayer,
                Escaped = false,
                Submitted = Submitted
            });
        }
    }
}
