using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
