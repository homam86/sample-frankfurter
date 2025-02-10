using Frankfurter.App;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddApiOutputCache(this IServiceCollection services)
    {
        var expireDuration = TimeSpan.FromMinutes(5); // TODO: Make it configurable

        return services.AddOutputCache(options =>
        {
            options.AddPolicy(PolicyNames.Default, builder => builder
                        .Tag("api-all")
                        .Expire(expireDuration));
        });
    }
}
