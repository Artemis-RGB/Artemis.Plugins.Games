using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class RootGameData
{
    [JsonPropertyName("round")]
    public Round? Round { get; set; }

    [JsonPropertyName("map")]
    public Map? Map { get; set; }

    [JsonPropertyName("player")]
    public Player? Player { get; set; }

    [JsonPropertyName("previously")]
    public RootGameData? Previously { get; set; }
    
    //this one is weird, not exactly the same structure.
    //i think it's just a dictionary of bools.
    //if true, it got added?
    //probably not useful to us.
    //[JsonPropertyName("added")]
    //public RootGameData? Added { get; set; }
}
