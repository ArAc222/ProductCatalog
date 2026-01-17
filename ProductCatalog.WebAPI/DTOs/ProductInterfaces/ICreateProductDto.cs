using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.ProductInterfaces
{
    public interface ICreateProductDto
    {
        string Name { get; set; }
        decimal Price { get; set; }
        int CategoryId { get; set; }
    }
}
