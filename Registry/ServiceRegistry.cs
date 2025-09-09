using System.Text.Json;
using StreamingApiDemo.Services;
using StreamingApiDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using StreamingApiDemo.Repositories;

namespace StreamingApiDemo.Registry;

public static class ServiceRegistry
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddDbContext<ApplicationDbContext>((provider, options) =>
        {
            var env = provider.GetRequiredService<IWebHostEnvironment>();
            var dbPath = Path.Combine(env.ContentRootPath, "products.db");
            options.UseSqlite($"Data Source={dbPath}");
        });

        services.AddSingleton(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}