using ProductCatalog.WebAPI.DTOs.CategoryInterfaces;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.Category
{
    public class CreateCategoryDto : ICreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
