using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Artemis.Plugins.Games.Dota2.DataModels.Nodes;

[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class Items
{
    public Item Slot0 { get; set; } = null!;
    public Item Slot1 { get; set; } = null!;
    public Item Slot2 { get; set; } = null!;
    public Item Slot3 { get; set; } = null!;
    public Item Slot4 { get; set; } = null!;
    public Item Slot5 { get; set; } = null!;
    public Item Slot6 { get; set; } = null!;
    public Item Slot7 { get; set; } = null!;
    public Item Slot8 { get; set; } = null!;
    
    public Item Stash0 { get; set; } = null!;
    public Item Stash1 { get; set; } = null!;
    public Item Stash2 { get; set; } = null!;
    public Item Stash3 { get; set; } = null!;
    public Item Stash4 { get; set; } = null!;
    public Item Stash5 { get; set; } = null!;
    
    public Item Teleport0 { get; set; } = null!;
    public Item Neutral0 { get; set; } = null!;
}