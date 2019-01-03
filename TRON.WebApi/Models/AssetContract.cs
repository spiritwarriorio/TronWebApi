namespace TRON.WebApi.Models
{
    public class AssetContract
    {
        public string owner_address { get; set; }
        public string to_address { get; set; }

        public string asset_name { get; set; }

        public long amount { get; set; }
    }
}