using HandmadeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace HandmadeHub.Data;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}