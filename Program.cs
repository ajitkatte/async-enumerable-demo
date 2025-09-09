using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using StreamingApiDemo.Registry;
using StreamingApiDemo.Repositories;
using StreamingApiDemo.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices();


var app = builder.Build();
var stoppingToken = app.Lifetime.ApplicationStopping;

using var scope = app.Services.CreateScope();
var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
await repo.InitializeAsync(stoppingToken);

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/products/stream", static async (HttpContext context,
                                             IProductService service,
                                             [FromServices] JsonSerializerOptions jsonOptions,
                                             [FromServices] ILogger<Program> logger,
                                             CancellationToken cancellationToken) =>
{
    context.Response.ContentType = "application/x-ndjson";

    try
    {
        await foreach (var product in service.StreamProductsAsync(cancellationToken))
        {
            var json = JsonSerializer.Serialize(product, jsonOptions);
            await context.Response.WriteAsync(json + "\n", cancellationToken);
            await context.Response.Body.FlushAsync(cancellationToken);
        }
    }
    catch (OperationCanceledException)
    {
        logger.LogError("Stream was canceled by the client.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while processing the request");
    }
});

await app.RunAsync(stoppingToken);