using System.Runtime.CompilerServices;
using StreamingApiDemo.Models;

namespace StreamingApiDemo.Services;

public class HardcodedProductService : IProductService
{
    public async IAsyncEnumerable<Product> StreamProductsAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<Product> products =
        [
            new(1, "Laptop", 59999.99m),
            new(2, "Mouse", 250.50m),
            new(3, "Keyboard", 450.00m),
            new(4, "Monitor", 15000.00m),
            new(5, "PSU", 1500.50m),
            new(6, "Headphones", 2599.99m)
        ];

        foreach (var product in products)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(500, cancellationToken);
            yield return product;
        }
    }
}