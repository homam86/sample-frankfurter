using Frankfurter.Api;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Frankfurter.App.Controllers;

[ApiController]
[Route("[controller]")]
public class ForexController : ControllerBase
{
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
        var result = await _frankfurterApiClient.GetLatestRatesAsync(src, dst);
        if (!result.Rates.ContainsKey(dst))
        {
            return StatusCode((int)HttpStatusCode.BadGateway, "Destination currency not found.");
        }

        return Ok(result.Rates[dst]);
    }
}
