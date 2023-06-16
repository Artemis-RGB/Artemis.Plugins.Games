using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.CSGO.GameDataModels;

public class RootGameData
{
    [JsonProperty("round")]
    public Round? Round { get; set; }

    [JsonProperty("map")]
    public Map? Map { get; set; }

    [JsonProperty("player")]
    public Player? Player { get; set; }

    [JsonProperty("previously")]
    public RootGameData? Previously { get; set; }
    
    //this one is weird, not exactly the same structure.
    //i think it's just a dictionary of bools.
    //if true, it got added?
    //probably not useful to us.
    //[JsonProperty("added")]
    //public RootGameData? Added { get; set; }
}
