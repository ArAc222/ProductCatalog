using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Service.Entities;
using ProductCatalog.Service.Interfaces;
using ProductCatalog.WebAPI.DTOs.Product;
using ProductCatalog.WebAPI.Mappings;

namespace ProductCatalog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET /api/products
        [HttpGet]
        public async Task<IActionResult> GetAll(
            int? categoryId = null,
            double? minPrice = null,
            double? maxPrice = null,
            bool? isActive = null,
            bool? inStock = null,
            string? sortBy = null,
            int page = 1,
            int pageSize = 10)
        {

            (IEnumerable<Product> items, int totalItems) = await _productService.GetAllAsync(
                categoryId, minPrice, maxPrice, isActive, inStock, sortBy, page, pageSize);

            var products = items.Select(p => p.ToDto());

            return Ok(new { Items = products, TotalItems = totalItems });
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.ToDto());
        }

        // POST /api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = dto.ToEntity();
            var created = await _productService.CreateAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDto());
        }

        // PUT /api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.UpdateEntity(dto);

            var updated = await _productService.UpdateAsync(product);

            if (!updated)
            {
                return StatusCode(500, "Failed to update product");
            }

            return NoContent();
        }

        // DELETE /api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
