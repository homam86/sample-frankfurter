using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Frankfurter.Api;

public class FrankfurterApiService : IFrankfurterApiService
{
    private readonly ILogger _logger;
    private readonly IFrankfurterApiClient _client;

    private readonly AsyncRetryPolicy _retryPolicy;

    public FrankfurterApiService(ILogger<FrankfurterApiService> logger, 
        IFrankfurterApiClient client)
    {
        _logger = logger;
        _client = client;

        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                3,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry.");
                });

    }

    public Task<ForexResponse> GetRateAsync(string baseCurrency, string[] currencies)
    {
        var symbols = currencies.Length > 0 ? string.Join(",", currencies) : null;

        return _retryPolicy.ExecuteAsync(() => _client.GetLatestRatesAsync(baseCurrency, symbols));
    }

    public Task<ForexResponseByDate> GetHistoryAsync(string baseCurrency, DateOnly startDate, DateOnly? endDate, string[] currencies)
    {
        var symbols = currencies.Length > 0 ? string.Join(",", currencies) : null;
        var dateRange = $"{startDate:yyyy-MM-dd}..";
        if (endDate.HasValue)
        {
            dateRange += $"{endDate:yyyy-MM-dd}";
        }

        return _retryPolicy.ExecuteAsync(() => _client.GetHistoricalRatesAsync(baseCurrency, dateRange, symbols));
    }
}
