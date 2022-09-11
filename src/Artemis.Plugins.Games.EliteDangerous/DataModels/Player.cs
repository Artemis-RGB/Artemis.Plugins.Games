using Artemis.Core;
using Artemis.Plugins.Games.EliteDangerous.Status;

namespace Artemis.Plugins.Games.EliteDangerous.DataModels
{
    public class Player
    {
        public Vehicle CurrentlyPiloting { get; internal set; }
        public Ranks Ranks { get; } = new();
        public LegalState LegalState { get; internal set; }
        public bool InWing { get; internal set; }

        public DataModelEvent Died { get; } = new();
    }
}
