using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient
{
    public class LcuEvent
    {
        [JsonProperty("data")]
        public JToken Data { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
