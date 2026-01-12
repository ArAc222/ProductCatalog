using Microsoft.EntityFrameworkCore;
using ProductCatalog.Service.Data;
using ProductCatalog.Service.Entities;
using ProductCatalog.Service.Interfaces;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductCatalog.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext context;

        public ProductService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<(IEnumerable<Product> Items, int TotalItems)> GetAllProductsAsync
            (   int? categoryId,
                double? minPrice,
                double? maxPrice,
                bool? isActive,
                bool? inStock,
                string? sortBy,
                int pageNumber,
                int pageSize
            )
        {
            //filtering
            var query = context.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(product => product.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(product => product.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(product => product.Price <= maxPrice.Value);
            }

            if (isActive.HasValue)
            {
                query = query.Where(product => product.IsActive == isActive.Value);
            }

            if (inStock.HasValue)
            {
                if (inStock.Value)
                {
                    query = query.Where(product => product.StockQuantity > 0);
                }
                else
                {
                    query = query.Where(product => product.StockQuantity == 0);
                }
            }

            //sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "price":
                        query = query.OrderBy(product => product.Price);
                        break;
                    case "name":
                        query = query.OrderBy(product => product.Name);
                        break;
                    case "createdat":
                        query = query.OrderBy(product => product.CreatedAt);
                        break;
                    default:
                        break;
                }
            }

            //paging
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            int totalItems = await query.CountAsync();

            int numberOfProductsToSkip = (pageNumber - 1) * pageSize;

            var items = await query.Skip(numberOfProductsToSkip).Take(pageSize).ToListAsync();

            return (items, totalItems);

        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var categoryExists = await context.Categories.AnyAsync(c => c.Id == product.CategoryId);

            if (!categoryExists)
            {
                throw new KeyNotFoundException($"Category with id {product.CategoryId} does not exist");
            }
            
            product.CreatedAt = DateTime.UtcNow;

            context.Products.Add(product);

            await context.SaveChangesAsync();

            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var productExists = await context.Products.AnyAsync(p => p.Id == product.Id);

            if (!productExists)
            {
                return false;
            }

            context.Products.Update(product);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            context.Products.Remove(product);

            await context.SaveChangesAsync();
            
            return true;
        }
    }
}
