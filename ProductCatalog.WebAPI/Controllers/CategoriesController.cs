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
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET /api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories.Select(c => c.ToDto()));
        }

        // GET /api/categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            return Ok(category.ToDto());
        }

        // POST /api/categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = dto.ToEntity();
            var created = await _categoryService.CreateAsync(category);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDto());
        }

        // PUT /api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            category.UpdateEntity(dto);
            var updated = await _categoryService.UpdateAsync(category);
            if (!updated) return StatusCode(500, "Failed to update category");

            return NoContent();
        }

        // DELETE /api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
