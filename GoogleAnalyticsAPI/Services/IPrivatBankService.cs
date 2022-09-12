using System.Threading.Tasks;

namespace GoogleAnalyticsAPI.Services;

public interface IPrivatBankService
{
    public Task<decimal> GetUsdUahRate();
}