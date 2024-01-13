using Artemis.Core.Modules;

namespace Artemis.Plugins.Games.Ultrakill.Module.DataModels
{
    public class UltrakillDataModel : DataModel
    {
        public float Health { get; set; }
        public float Speed { get; set; }
        public float Stamina { get; set; }
        public bool Jumping { get; set; }
        public bool Dead { get; set; }
        public int CurrentGun { get; set; }
        public int CurrentGunVariation { get; set; }
    }
}