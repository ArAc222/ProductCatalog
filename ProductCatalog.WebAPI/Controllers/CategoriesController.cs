using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Service.Interfaces;
using ProductCatalog.WebAPI.DTOs.Category;
using ProductCatalog.WebAPI.Mappings;

namespace ProductCatalog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET /api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await categoryService.GetAllCategoriesAsync();

            return Ok(categories.Select(c => c.ToDto()));
        }

        // GET /api/categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category.ToDto());
        }

        // POST /api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = categoryDto.ToEntity();

            var created = await categoryService.CreateCategoryAsync(category);

            return CreatedAtAction(nameof(GetCategoryByIdAsync), new { id = created.Id }, created.ToDto());
        }

        // PUT /api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await categoryService.GetCategoryByIdAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }

            category.UpdateEntity(updateCategoryDto);

            var updated = await categoryService.UpdateCategoryAsync(category);

            if (!updated)
            {
                return StatusCode(500, "Failed to update category");
            }

            return NoContent();
        }

        // DELETE /api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var deleted = await categoryService.DeleteCategoryAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
