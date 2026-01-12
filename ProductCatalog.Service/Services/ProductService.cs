using Microsoft.EntityFrameworkCore;
using ProductCatalog.Service.Data;
using ProductCatalog.Service.Entities;
using ProductCatalog.Service.Interfaces;

namespace ProductCatalog.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Product> Items, int TotalItems)> GetAllAsync
            (   int? categoryId,
                double? minPrice,
                double? maxPrice,
                bool? isActive,
                bool? inStock,
                string? sortBy,
                int page,
                int pageSize
            )
        {
            //filtering
            var query = _context.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (isActive.HasValue)
            {
                query = query.Where(p => p.IsActive == isActive.Value);
            }

            if (inStock.HasValue)
            {
                if (inStock.Value)
                    query = query.Where(p => p.StockQuantity > 0);
                else
                    query = query.Where(p => p.StockQuantity == 0);
            }

            //sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "price":
                        query = query.OrderBy(p =>  p.Price);
                        break;
                    case "name":
                        query = query.OrderBy(p => p.Name);
                        break;
                    case "createdat":
                        query = query.OrderBy(p => p.CreatedAt);
                        break;
                    default:
                        break;
                }
            }

            //paging
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            int TotalItems = await query.CountAsync();

            int skip = (page - 1) * pageSize;
            var items = await query.Skip(skip)
                                   .Take(pageSize)
                                   .ToListAsync();

            return (items, TotalItems);

        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);
            if (categoryExists)
            {
                product.CreatedAt = DateTime.UtcNow;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            else
            {
                throw new KeyNotFoundException($"Category with id {product.CategoryId} does not exist");
            }

        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var exists = await _context.Products.AnyAsync(p => p.Id == product.Id);
            if (!exists)
                return false;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
