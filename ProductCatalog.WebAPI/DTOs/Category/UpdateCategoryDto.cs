using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
