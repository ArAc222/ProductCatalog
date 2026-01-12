using Microsoft.EntityFrameworkCore;
using ProductCatalog.Service.Data;
using ProductCatalog.Service.Entities;
using ProductCatalog.Service.Interfaces;

namespace ProductCatalog.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext context;

        public CategoryService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<ProductCategory?> GetCategoryByIdAsync(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<ProductCategory> CreateCategoryAsync(ProductCategory category)
        {
            context.Categories.Add(category);

            await context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> UpdateCategoryAsync(ProductCategory category)
        {
            var exists = await context.Categories.AnyAsync(c => c.Id == category.Id);

            if (!exists)
            {
                return false;
            }

            context.Categories.Update(category);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null)
            {
                return false;
            }

            context.Categories.Remove(category);

            await context.SaveChangesAsync();
            
            return true;
        }
    }
}
