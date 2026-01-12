using ProductCatalog.WebAPI.DTOs.CategoryInterfaces;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.Category
{
    public class UpdateCategoryDto : IUpdateCategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
