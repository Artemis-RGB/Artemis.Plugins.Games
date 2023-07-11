using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;
using System;
using ItemEnum = Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums.Item;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels;

public class ItemSlotDataModel : DataModel
{
    public ItemEnum Item { get; set; }
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
            Item = ItemEnum.None;
            Name = "";
            CanUse = false;
            Consumable = false;
            Count = 0;
            return;
        }

        Item = Enum.IsDefined(typeof(ItemEnum), item.ItemID) ? (ItemEnum)item.ItemID : ItemEnum.Unknown;
        Name = item.DisplayName;
        CanUse = item.CanUse;
        Consumable = item.Consumable;
        Count = item.Count;
    }
}