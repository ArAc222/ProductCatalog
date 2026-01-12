using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.WebAPI.DTOs.CategoryInterfaces
{
    public interface ICreateCategoryDto
    {
        [Required]
        string Name { get; set; }
        string? Description { get; set; }
    }
}
