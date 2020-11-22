using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexScheduler
{
    public class ApexCommand
    {
        [JsonPropertyName("status")]
        public List<string> status { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("gid")]
        public string gid { get; set; }

        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("did")]
        public string did { get; set; }
    }
}