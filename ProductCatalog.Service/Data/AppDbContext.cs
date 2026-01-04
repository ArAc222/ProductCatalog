using Microsoft.EntityFrameworkCore;
using ProductCatalog.Service.Entities;

namespace ProductCatalog.Service.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
