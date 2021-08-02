using System.ComponentModel.DataAnnotations;

namespace BitcoinTickerApi.Models.Request
{
    public class ConversionViewModel
    {
        [Required]
        public string Currency { get; set; }

        [Required]
        [Range(1, 999999)]
        public decimal Value { get; set; }
    }
}
