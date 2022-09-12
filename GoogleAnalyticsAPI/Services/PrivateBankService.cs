using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GoogleAnalyticsAPI.Models;
using Newtonsoft.Json;

namespace GoogleAnalyticsAPI.Services;

public class PrivateBankService : IPrivatBankService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PrivateBankService(IHttpClientFactory clientFactory)
    {
        _httpClientFactory = clientFactory;
    }

    public async Task<decimal> GetUsdUahRate()
    {
        var client = _httpClientFactory.CreateClient("Privat");
        var response = await client.GetAsync("NBU_Exchange/exchange?json");
        var items = JsonConvert.DeserializeObject<IEnumerable<ExchangeUAH>>(await response.Content.ReadAsStringAsync());
        var usdUahRate = items.FirstOrDefault(i => i.CurrencyCodeL.Equals("USD"));
        return usdUahRate.Amount;
    }
}