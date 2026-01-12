namespace ProductCatalog.WebAPI.DTOs.CategoryInterfaces
{
    public interface ICreateCategoryDto
    {
        string Name { get; set; }
        string? Description { get; set; }
    }
}
