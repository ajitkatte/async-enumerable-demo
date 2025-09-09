using StreamingApiDemo.Models;

namespace StreamingApiDemo.Repositories;

public interface IProductRepository
{
    IAsyncEnumerable<Product> StreamAllAsync();
    Task InitializeAsync(CancellationToken cancellationToken = default);
}
