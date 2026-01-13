using ProductCatalog.WebAPI.DTOs.ProductInterfaces;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.Product
{
    public class UpdateProductDto : IUpdateProductDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public double? Price { get; set; }

        public int? StockQuantity { get; set; }
        public bool? IsActive { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
