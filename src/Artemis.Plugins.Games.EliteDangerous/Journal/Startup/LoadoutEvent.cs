using Artemis.Plugins.Modules.EliteDangerous.DataModels;
using Artemis.Plugins.Modules.EliteDangerous.Utils;

namespace Artemis.Plugins.Modules.EliteDangerous.Journal.Startup
{
    internal class LoadoutEvent : IJournalEvent
    {
        public string Ship;
        public string ShipName;
        public string ShipIdent;
        public float MaxJumpRange;

        public void ApplyUpdate(EliteDangerousDataModel model)
        {
            model.Ship.Name = ShipName;
            model.Ship.Ident = ShipIdent;
            (model.Ship.Type, model.Ship.Size) = ShipTypeDefinitions.GetById(Ship);
            model.Navigation.MaximumUnladenJumpRange = MaxJumpRange;
        }
    }
}
