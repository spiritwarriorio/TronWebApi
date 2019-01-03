
using Newtonsoft.Json;

namespace TRON.WebApi.Models
{
    public class AssetContractResponse
    {
        [JsonProperty("txID")]
        public string txID { get; set; }

        [JsonProperty("raw_data")]
        public raw_data raw_data { get; set; }
       
    }
}