using ProductCatalog.Service.Entities;
using ProductCatalog.WebAPI.DTOs.Category;

namespace ProductCatalog.WebAPI.Mappings
{
    public static class CategoryMappings
    {
        public static CategoryDto ToDto(this ProductCategory category) => new()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };

        public static ProductCategory ToEntity(this CreateCategoryDto dto) => new()
        {
            Name = dto.Name,
            Description = dto.Description
        };

        public static void UpdateEntity(this ProductCategory category, UpdateCategoryDto dto)
        {
            category.Name = dto.Name;
            category.Description = dto.Description;
        }
    }
}
