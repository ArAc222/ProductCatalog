using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.CategoryInterfaces
{
    public interface IUpdateCategoryDto
    {
        [Required]
        string Name { get; set; }
        string? Description { get; set; }
    }
}
