using System.Threading.Tasks;
using GoogleAnalyticsAPI.Services;
using Microsoft.Extensions.Logging;
using Quartz;

namespace GoogleAnalyticsAPI.Jobs;

[DisallowConcurrentExecution]
public class GaSendMetricsJob : IJob
{
    private readonly ILogger<GaSendMetricsJob> _logger;
    private readonly IGoogleAnalyticsService _googleAnalyticsService;
    private readonly IPrivatBankService _privatBankService;

    public GaSendMetricsJob(
        IPrivatBankService privatBankService,
        IGoogleAnalyticsService googleAnalyticsService,
        ILogger<GaSendMetricsJob> logger)
    {
        _logger = logger;
        _privatBankService = privatBankService;
        _googleAnalyticsService = googleAnalyticsService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var rate = await _privatBankService.GetUsdUahRate();
        var result = await _googleAnalyticsService.SendMetrics(rate);
        
        _logger.LogInformation($"Send metrics to GA. Result : {result}");
    }
}