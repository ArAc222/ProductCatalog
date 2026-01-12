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
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET /api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync(
            int? categoryId = null,
            double? minPrice = null,
            double? maxPrice = null,
            bool? isActive = null,
            bool? inStock = null,
            string? sortBy = null,
            int page = 1,
            int pageSize = 10)
        {

            (IEnumerable<Product> items, int totalItems) = await productService.GetAllProductsAsync(
                categoryId, minPrice, maxPrice, isActive, inStock, sortBy, page, pageSize);

            var products = items.Select(p => p.ToDto());

            return Ok(new { Items = products, TotalItems = totalItems });
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.ToDto());
        }

        // POST /api/products
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = createProductDto.ToEntity();

            var createdProduct = await productService.CreateProductAsync(product);

            return StatusCode(201, createdProduct.ToDto());
        }

        // PUT /api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] UpdateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.UpdateEntity(updateProductDto);

            bool isProductUpdated = await productService.UpdateProductAsync(product);

            if (!isProductUpdated)
            {
                return StatusCode(500, "Failed to update product");
            }

            return NoContent();
        }

        // DELETE /api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            bool isDeleted = await productService.DeleteProductAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
