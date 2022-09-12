using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleAnalyticsAPI.Services;

public class GoogleAnalyticsService : IGoogleAnalyticsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public GoogleAnalyticsService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<bool> SendMetrics(decimal rateUsdUah)
    {
        var client = _httpClientFactory.CreateClient("GoogleAnalytics");
        var response = await client.PostAsync(
            $"collect?v=1&t=event&tid=UA-240854845-3&cid=555&ec=rateUahUsd&ea=receive&el=rateUsdUah&ev=120",
            null);
        
        return response.IsSuccessStatusCode;
    }
}