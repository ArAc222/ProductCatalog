namespace ProductCatalog.WebAPI.DTOs.ProductInterfaces
{
    public interface IUpdateProductDto
    {
        string Name { get; set; }
        decimal Price { get; set; }
        int StockQuantity { get; set; }
        bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}
