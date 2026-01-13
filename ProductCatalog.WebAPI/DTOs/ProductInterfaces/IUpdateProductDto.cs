using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.ProductInterfaces
{
    public interface IUpdateProductDto
    {
        [Required]
        string Name { get; set; }

        [Range(0, double.MaxValue)]
        double? Price { get; set; }
        int? StockQuantity { get; set; }
        bool? IsActive { get; set; }

        [Required]
        int CategoryId { get; set; }
    }
}
