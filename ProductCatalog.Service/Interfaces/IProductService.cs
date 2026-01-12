using ProductCatalog.Service.Entities;

namespace ProductCatalog.Service.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<Product> Items, int TotalItems)> GetAllProductsAsync(
            int? categoryId,
            double? minPrice,
            double? maxPrice,
            bool? isActive,
            bool? inStock,
            string? sortBy,
            int page,
            int pageSize
        );
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
