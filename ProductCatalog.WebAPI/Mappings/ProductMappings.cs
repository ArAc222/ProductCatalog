using ProductCatalog.Service.Entities;
using ProductCatalog.WebAPI.DTOs.Product;

namespace ProductCatalog.WebAPI.Mappings
{
    public static class ProductMappings
    {
        public static ProductDto ToDto(this Product product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            IsActive = product.IsActive,
            CategoryId = product.CategoryId
        };

        public static Product ToEntity(this CreateProductDto dto) => new()
        {
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            IsActive = true
        };

        public static void UpdateEntity(this Product product, UpdateProductDto dto)
        {
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.IsActive = dto.IsActive;
            product.CategoryId = dto.CategoryId;
        }
    }
}
