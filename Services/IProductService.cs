using StreamingApiDemo.Dto;

namespace StreamingApiDemo.Services;

public interface IProductService
{
    IAsyncEnumerable<ProductDto> StreamProductsAsync(CancellationToken cancellationToken);
}