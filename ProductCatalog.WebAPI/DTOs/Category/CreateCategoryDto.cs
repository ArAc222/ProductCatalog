using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
