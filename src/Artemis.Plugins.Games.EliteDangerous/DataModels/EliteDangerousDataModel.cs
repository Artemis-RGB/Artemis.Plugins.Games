using Artemis.Core.Modules;

namespace Artemis.Plugins.Games.EliteDangerous.DataModels
{
    public class EliteDangerousDataModel : DataModel
    {
        public Player Player { get; } = new();
        public Ship Ship { get; } = new();
        public SRV SRV { get; } = new();
        public Navigation Navigation { get; } = new();
        public HUD HUD { get; } = new();
    }
}
