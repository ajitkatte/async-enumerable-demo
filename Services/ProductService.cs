using System.Runtime.CompilerServices;
using StreamingApiDemo.Models;
using StreamingApiDemo.Repositories;
using StreamingApiDemo.Dto;

namespace StreamingApiDemo.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    public async IAsyncEnumerable<ProductDto> StreamProductsAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var product in _repository.StreamAllAsync().WithCancellation(cancellationToken))
        {
            await Task.Delay(500, cancellationToken);

            yield return new ProductDto(product.Id, product.Name, product.Price);
        }
    }
}
