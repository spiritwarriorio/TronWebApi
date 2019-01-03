using Newtonsoft.Json;
using System.Collections.Generic;


namespace TRON.WebApi.Models
{
    public class SignatureResponse
    {
        [JsonProperty("result")]
        public bool result { get; set; }

        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("message")]
        public raw_data message { get; set; }
    }
}