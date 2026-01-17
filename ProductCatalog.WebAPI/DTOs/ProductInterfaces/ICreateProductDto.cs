using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.ProductInterfaces
{
    public interface ICreateProductDto
    {
        [Required]
        string Name { get; set; }

        [Range(0, double.MaxValue)]
        decimal Price { get; set; }

        [Required]
        int CategoryId { get; set; }
    }
}
