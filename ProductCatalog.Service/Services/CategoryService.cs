using Microsoft.EntityFrameworkCore;
using ProductCatalog.Service.Data;
using ProductCatalog.Service.Entities;
using ProductCatalog.Service.Interfaces;

namespace ProductCatalog.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<ProductCategory?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<ProductCategory> CreateAsync(ProductCategory category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(ProductCategory category)
        {
            var exists = await _context.Categories.AnyAsync(c => c.Id == category.Id);
            if (!exists)
                return false;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
