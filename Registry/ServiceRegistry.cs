using System.Text.Json;
using StreamingApiDemo.Services;

namespace StreamingApiDemo.Registry;

public static class ServiceRegistry
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IProductService, HardcodedProductService>();
        services.AddSingleton(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}