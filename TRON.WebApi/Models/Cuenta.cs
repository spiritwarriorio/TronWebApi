namespace TRON.WebApi.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Cuenta
    {

        public string address { get; set; }

        public string balance { get; set; }

        [JsonProperty("asset")]
        public List<asset> assets { get; set; }

        public string create_time { get; set; }

        public string latest_opration_time { get; set; }

        public List<frozen_supply> frozen_supply { get; set; }

        public string asset_issued_name { get; set; }

        [JsonProperty("free_asset_net_usage")]
        public List<free_asset_net_usage> free_asset_net_usages { get; set; }

        public string latest_consume_free_time { get; set; }

    }
    public class asset
    {
        [JsonProperty("key")]
        public string key { get; set; }
        [JsonProperty("value")]
        public string value { get; set; }
    }

    public class free_asset_net_usage
    {
        [JsonProperty("key")]
        public string key { get; set; }
        [JsonProperty("value")]
        public string value { get; set; }
    }    

    public class frozen_supply
    {
        public string frozen_balance { get; set; }
        public string expire_time { get; set; }
    }
}