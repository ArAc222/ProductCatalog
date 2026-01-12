namespace ProductCatalog.WebAPI.DTOs.ProductInterfaces
{
    public interface IProductDto
    {
        int Id { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        int StockQuantity { get; set; }
        bool IsActive { get; set; }
        int CategoryId { get; set; }
    }
}
