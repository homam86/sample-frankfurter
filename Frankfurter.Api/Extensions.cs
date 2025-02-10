﻿using Frankfurter.Api;
using Refit;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    const string BaseUrl = "https://api.frankfurter.app";
    public static IServiceCollection AddFrankfurterApi(this IServiceCollection services)
    {
        services.AddRefitClient<IFrankfurterApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(BaseUrl));

        return services;
    }
}
