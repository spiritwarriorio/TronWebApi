
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TRON.WebApi.Models
{
    public class Signature
    {
        [JsonProperty("signature")]
        public List<string> signature { get; set; }

        [JsonProperty("txID")]
        public string txID { get; set; }

        [JsonProperty("raw_data")]
        public raw_data raw_data { get; set; }
    }
}