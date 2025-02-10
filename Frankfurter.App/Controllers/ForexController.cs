using Frankfurter.Api;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Frankfurter.App.Controllers;

[ApiController]
[Route("[controller]")]
public class ForexController : ControllerBase
{
    private static readonly string[] BannedCurrencies = ["TRY", "PLN", "THB", "MXN"];

    private readonly ILogger<ForexController> _logger;
    private readonly IFrankfurterApiClient _frankfurterApiClient;

    public ForexController(ILogger<ForexController> logger,
        IFrankfurterApiClient frankfurterApiClient)
    {
        _logger = logger;
        _frankfurterApiClient = frankfurterApiClient;
    }

    [HttpGet("{currency}")]
    public async Task<ForexResponse> GetAsync(string currency)
    {
        var result = await _frankfurterApiClient.GetLatestRatesAsync(currency);
        return result;
    }
    
    [HttpGet("{src}/{dst}")]
    public async Task<ActionResult<decimal>> GetExchangeAsync(string src, string dst)
    {
        dst = dst.ToUpper();
        if(IsBannedCurrency(dst))
        {
            return BadRequest($"Currency '{dst}' is not supported");
        }

        var result = await _frankfurterApiClient.GetLatestRatesAsync(src, dst);
        if (!result.Rates.ContainsKey(dst))
        {
            return StatusCode((int)HttpStatusCode.BadGateway, "Destination currency not found.");
        }

        return Ok(result.Rates[dst]);
    }

    [HttpGet("{currency}/history/{startDate:regex(^\\d{{4}}-\\d{{2}}-\\d{{2}}$)}/{endDate:regex(^\\d{{4}}-\\d{{2}}-\\d{{2}}$)?}")]
    public async Task<ActionResult<ForexResponse>> GetHistoryAsync(string currency, string startDate, string? endDate)
    {
        var date = startDate + ".." + endDate;
        var result = await _frankfurterApiClient.GetHistoricalRatesAsync(currency, date);

        return Ok(result);
    }

    private static bool IsBannedCurrency(string currency)
    {
        currency = currency.ToUpper();
        return BannedCurrencies.Contains(currency);
    }
}
