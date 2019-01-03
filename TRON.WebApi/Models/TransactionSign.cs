using Newtonsoft.Json;
using System.Collections.Generic;

namespace TRON.WebApi.Models
{
    public class TransactionSign
    {
        [JsonProperty("transaction")]
        public transaction transaction { get; set; }        
    }

    public class transaction
    {
        [JsonProperty("txID")]
        public string txID { get; set; }       

        [JsonProperty("raw_data")]
        public raw_data raw_data { get; set; }

        [JsonProperty("privateKey")]
        public string privateKey { get; set; }
    }

    public class raw_data
    {
        [JsonProperty("contract")]
        public List<contract> contract { get; set; }
        [JsonProperty("ref_block_bytes")]
        public string ref_block_bytes { get; set; }

        [JsonProperty("ref_block_hash")]
        public string ref_block_hash { get; set; }

        [JsonProperty("expiration")]
        public long expiration { get; set; }
        [JsonProperty("timestamp")]
        public long timestamp { get; set; }
    }

    public class contract
    {
        [JsonProperty("parameter")]
        public parameter parameter { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
    }

    public class parameter
    {
        [JsonProperty("value")]
        public value value { get; set; }
        [JsonProperty("type_url")]
        public string type_url { get; set; }
    }

    public class value
    {
        [JsonProperty("owner_address")]
        public string owner_address { get; set; }
        [JsonProperty("to_address")]
        public string to_address { get; set; }
        [JsonProperty("asset_name")]
        public string asset_name { get; set; }
        [JsonProperty("amount")]
        public long amount { get; set; }
    }

}