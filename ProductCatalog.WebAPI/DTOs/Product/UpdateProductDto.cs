using ProductCatalog.WebAPI.DTOs.ProductInterfaces;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.Product
{
    public class UpdateProductDto : IUpdateProductDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
