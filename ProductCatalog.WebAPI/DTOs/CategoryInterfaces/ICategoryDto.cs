namespace ProductCatalog.WebAPI.DTOs.Interfaces
{
    public interface ICategoryDto
    {
        int Id { get; set; }
        string Name { get; set; }
        string? Description { get; set; }
    }
}
