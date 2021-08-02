using System.Text.Json.Serialization;

namespace BitcoinTickerApi.Models
{
    public partial class BtcInfo
    {
        [JsonPropertyName("15m")]
        public double FifteenM { get; set; }

        [JsonPropertyName("last")]
        public double Last { get; set; }

        [JsonPropertyName("buy")]
        public double Buy { get; set; }

        [JsonPropertyName("sell")]
        public double Sell { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }
}