using BitcoinTickerApi.Models;
using BitcoinTickerApi.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitcoinTickerApi.Services
{
    public interface IBitcoinApiService
    {
        public Task<decimal> ToBtc(ConversionViewModel request);
        public Task<List<BtcInfo>> Ticker();
    }
}