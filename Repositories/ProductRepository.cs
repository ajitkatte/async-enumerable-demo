using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using StreamingApiDemo.Data;
using StreamingApiDemo.Models;

namespace StreamingApiDemo.Repositories;

public class ProductRepository(ApplicationDbContext db) : IProductRepository
{
    private readonly ApplicationDbContext _db = db;

    public IAsyncEnumerable<Product> StreamAllAsync()
    {
        return _db.Products.AsNoTracking().AsAsyncEnumerable();
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await _db.Database.EnsureCreatedAsync(cancellationToken);

        if (!await _db.Products.AnyAsync(cancellationToken))
        {
            List<Product> seed =
            [
                new(1, "Laptop", 59999.99m),
                new(2, "Mouse", 250.50m),
                new(3, "Keyboard", 450.00m),
                new(4, "Monitor", 15000.00m),
                new(5, "PSU", 1500.50m),
                new(6, "Headphones", 2599.99m)
            ];

            _db.Products.AddRange(seed);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
