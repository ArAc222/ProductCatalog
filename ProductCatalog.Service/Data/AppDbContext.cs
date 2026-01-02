using Microsoft.EntityFrameworkCore;
using ProductCatalog.Service.Entities;
using ProductCatalog.Service.Models; // Ovdje staviš svoje modele Product i ProductCategory

namespace ProductCatalog.Service.Data
{
    public class AppDbContext : DbContext
    {
        // Konstruktor prima DbContextOptions preko DI
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet za svaku tablicu
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
