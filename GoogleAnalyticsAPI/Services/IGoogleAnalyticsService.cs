using System.Threading.Tasks;

namespace GoogleAnalyticsAPI.Services;

public interface IGoogleAnalyticsService
{
    Task<bool> SendMetrics(decimal rateUsdUah);
}