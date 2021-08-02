using BitcoinTickerApi.Models;
using BitcoinTickerApi.Models.Request;
using BitcoinTickerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitcoinTickerApi.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BitcoinController : ControllerBase
    {
        private readonly IBitcoinApiService _bitcoinApiService;

        public BitcoinController(IBitcoinApiService bitcoinApiService)
        {
            _bitcoinApiService = bitcoinApiService;
        }

        [HttpGet("info")]
        public async Task<ActionResult<List<BtcInfo>>> Ticker()
        {
            return await _bitcoinApiService.Ticker();
        }

        [HttpPost("convert")]
        public async Task<ActionResult<decimal>> ToBtc([FromBody] ConversionViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _bitcoinApiService.ToBtc(request);
        }
    }
}
