using ProductCatalog.Service.Entities;

namespace ProductCatalog.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task<ProductCategory?> GetByIdAsync(int id);
        Task<ProductCategory> CreateAsync(ProductCategory category);
        Task<bool> UpdateAsync(ProductCategory category);
        Task<bool> DeleteAsync(int id);
    }
}
