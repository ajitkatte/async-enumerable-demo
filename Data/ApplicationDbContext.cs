using Microsoft.EntityFrameworkCore;
using StreamingApiDemo.Models;

namespace StreamingApiDemo.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}
