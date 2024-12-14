using Microsoft.AspNetCore.Mvc;
using WebApiDapper.ActionFilters;
using WebApiDapper.DTOs.ProductDTO;
using WebApiDapper.Entities;
using WebApiDapper.Services;

namespace WebApiDapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService producService)
        {
            _productService = producService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productService.GetAllProductAsync();
            return Ok(products);
        }

        //[HttpGet("page")]
        //[ExceptionHandleFilter]
        //public async Task<IActionResult> GetProductByPaging([FromQuery] int pageNumber, [FromQuery] int pageSize)
        //{
        //    var products = await _productRepo.GetPagingAsync(pageNumber, pageSize);
        //    return Ok(products);
        //}

        [HttpGet("id")]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<Product, int>))]
        public IActionResult GetProductById(int id)
        {
            var product = HttpContext.Items["Entity"] as Product;
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationIsExistEntity<ProductCreateRequestDTO>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequestDTO product)
        {
            var productId = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), productId);
        }

        [HttpPut("id")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<Product, int>))]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateRequestDTO product)
        {
            var existingProduct = HttpContext.Items["Entity"] as Product;
            if (existingProduct == null)
                return NotFound();
            existingProduct.Name = product.Name;
            await _productService.UpdateProduct(existingProduct.Id, product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<Product, int>))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = HttpContext.Items["Entity"] as Product;
            if (product == null) return NotFound();
            await _productService.DeleteProductByIdAsync(product.Id);
            return NoContent();
        }
    }
}
