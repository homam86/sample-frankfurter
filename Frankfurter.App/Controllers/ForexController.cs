using Frankfurter.Api;
using Microsoft.AspNetCore.Mvc;

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
}
