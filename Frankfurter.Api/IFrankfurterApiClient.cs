using Refit;

namespace Frankfurter.Api;

public interface IFrankfurterApiClient
{
    [Get("/latest")]
    Task<ForexResponse> GetLatestRatesAsync([AliasAs("from")] string fromCurrency);

    [Get("/latest")]
    Task<ForexResponse> GetLatestRatesAsync([AliasAs("from")] string fromCurrency, [AliasAs("to")] string toCurrency);

    [Get("/latest")]
    Task<ForexResponse> GetLatestRatesAsync([AliasAs("from")] string fromCurrency, [AliasAs("to")] string[] toCurrencies);

    [Get("/{date}")]
    Task<ForexResponse> GetHistoricalRatesAsync(string date, [AliasAs("from")] string fromCurrency);

    [Get("/{date}")]
    Task<ForexResponse> GetHistoricalRatesAsync(string date, [AliasAs("from")] string fromCurrency, [AliasAs("to")] string toCurrency);

    [Get("/{date}")]
    Task<ForexResponse> GetHistoricalRatesAsync(string date, [AliasAs("from")] string fromCurrency, [AliasAs("to")] string[] toCurrencies);

}
