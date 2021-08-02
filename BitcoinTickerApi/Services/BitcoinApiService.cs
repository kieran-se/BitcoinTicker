using BitcoinTickerApi.Models;
using BitcoinTickerApi.Models.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace BitcoinTickerApi.Services
{
    public class BitcoinApiService: IBitcoinApiService
    {
        public HttpClient HttpClient { get; }
        IConfiguration Configuration;

        public BitcoinApiService(HttpClient httpClient, IConfiguration configuration)
        {
            Configuration = configuration;

            httpClient.BaseAddress = new Uri(Configuration["BitcoinApi:BaseUrl"]);

            HttpClient = httpClient;

        }

        public async Task<List<BtcInfo>> Ticker()
        {
            try
            {
                var response = await HttpClient.GetFromJsonAsync<Dictionary<string, BtcInfo>>("/ticker");

                var sortedDict = from entry in response orderby entry.Value.FifteenM ascending select entry;

                return sortedDict.Select(x => x.Value).ToList();
            }
            catch (HttpRequestException) // Non success
            {
                Console.WriteLine("An error occurred.");
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");
            }
            catch (JsonException ex) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
            }

            return new List<BtcInfo>();
        }

        public async Task<decimal> ToBtc(ConversionViewModel request)
        {
            try
            {
                return await HttpClient.GetFromJsonAsync<decimal>($"/tobtc?currency={request.Currency}&value={request.Value}");
            }
            catch (HttpRequestException) // Non success
            {
                Console.WriteLine("An error occurred.");
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");
            }
            catch (JsonException) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
            }

            return new decimal(0);
        }
    }
}
