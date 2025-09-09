using StreamingApiDemo.Models;

namespace StreamingApiDemo.Services;

public interface IProductService
{
    IAsyncEnumerable<Product> StreamProductsAsync(CancellationToken cancellationToken);
}