using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels;

public class InventoryDataModel : DataModel
{
    public ItemSlotDataModel Slot1 { get; } = new();
    public ItemSlotDataModel Slot2 { get; } = new();
    public ItemSlotDataModel Slot3 { get; } = new();
    public ItemSlotDataModel Slot4 { get; } = new();
    public ItemSlotDataModel Slot5 { get; } = new();
    public ItemSlotDataModel Slot6 { get; } = new();
    public ItemSlotDataModel Trinket { get; } = new();

    public int ItemCount { get; set; }

    public void Update(Item[] items)
    {
        Slot1.Update(items, 0);
        Slot2.Update(items, 1);
        Slot3.Update(items, 2);
        Slot4.Update(items, 3);
        Slot5.Update(items, 4);
        Slot6.Update(items, 5);
        Trinket.Update(items, 6);
        ItemCount = items.Length;
    }
}