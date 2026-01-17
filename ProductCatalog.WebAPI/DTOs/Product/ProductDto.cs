using ProductCatalog.WebAPI.DTOs.ProductInterfaces;

namespace ProductCatalog.WebAPI.DTOs.Product
{
    public class ProductDto : IProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}
