using ProductCatalog.WebAPI.DTOs.Interfaces;

namespace ProductCatalog.WebAPI.DTOs.Category
{
    public class CategoryDto : ICategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
