using ProductCatalog.Service.Interfaces;

namespace ProductCatalog.Service.Entities
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; } = null!;
    }
}
