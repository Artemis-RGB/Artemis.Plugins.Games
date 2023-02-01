using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;
using System;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels
{
    public class ItemSlotDataModel : DataModel
    {
        public Enums.Item Item { get; set; }
        public string Name { get; set; }
        public bool HasItem => !string.IsNullOrWhiteSpace(Name);
        public bool CanUse { get; set; }
        public bool Consumable { get; set; }
        public int Count { get; set; }

        public void Update(Item[] items, int index)
        {
            var item = Array.Find(items, i => i.Slot == index);
            if (item == null)
            {
                Item = Enums.Item.None;
                Name = "";
                CanUse = false;
                Consumable = false;
                Count = 0;
                return;
            }

            Item = Enum.IsDefined(typeof(Enums.Item), item.ItemID) ? (Enums.Item)item.ItemID : Enums.Item.Unknown;
            Name = item.DisplayName;
            CanUse = item.CanUse;
            Consumable = item.Consumable;
            Count = item.Count;
        }
    }
}
