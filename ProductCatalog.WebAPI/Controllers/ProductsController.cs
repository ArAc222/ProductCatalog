using Microsoft.AspNetCore.Mvc;
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
            [FromQuery] int? categoryId,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool? isActive,
            [FromQuery] string? sortBy,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetAllAsync();

            // FILTERING
            if (categoryId.HasValue)
                products = products.Where(p => p.CategoryId == categoryId.Value);

            if (minPrice.HasValue)
                products = products.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                products = products.Where(p => p.Price <= maxPrice.Value);

            if (isActive.HasValue)
                products = products.Where(p => p.IsActive == isActive.Value && p.StockQuantity > 0);

            // SORTING
            products = sortBy?.ToLower() switch
            {
                "price" => products.OrderBy(p => p.Price),
                "name" => products.OrderBy(p => p.Name),
                "createdat" => products.OrderBy(p => p.CreatedAt),
                _ => products
            };

            // PAGING
            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var pagedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => p.ToDto());

            var result = new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = pagedProducts
            };

            return Ok(result);
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product.ToDto());
        }

        // POST /api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = dto.ToEntity();
            var created = await _productService.CreateAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDto());
        }

        // PUT /api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            product.UpdateEntity(dto);
            var updated = await _productService.UpdateAsync(product);
            if (!updated)
                return StatusCode(500, "Failed to update product");

            return NoContent();
        }

        // DELETE /api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
