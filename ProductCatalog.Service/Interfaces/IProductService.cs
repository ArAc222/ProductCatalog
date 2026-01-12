using ProductCatalog.Service.Entities;

namespace ProductCatalog.Service.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<Product> Items, int TotalItems)> GetAllAsync(
            int? categoryId,
            double? minPrice,
            double? maxPrice,
            bool? isActive,
            bool? inStock,
            string? sortBy,
            int page,
            int pageSize
        );
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
