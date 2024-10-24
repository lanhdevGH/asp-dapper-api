using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDapper.ActionFilters;
using WebApiDapper.Entities;
using WebApiDapper.ExceptionFilters;
using WebApiDapper.IRepositories;
using WebApiDapper.IRepositories.Impl;

namespace WebApiDapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productRepo.GetAll();
            return Ok(products);
        }

        [HttpGet("page")]
        [ExceptionHandleFilter]
        public async Task<IActionResult> GetProductByPaging([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            throw new NotImplementedException();
            var products = await _productRepo.GetPaging(pageNumber, pageSize);
            return Ok(products);
        }

        [HttpGet("id")]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<Product>))]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = HttpContext.Items["Entity"] as Product;
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productRepo.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("id")]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<Product>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = HttpContext.Items["Entity"] as Product;
            if (existingProduct == null)
                return NotFound();
            existingProduct.Name = product.Name;
            await _productRepo.Update(existingProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<Product>))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = HttpContext.Items["Entity"] as Product;
            await _productRepo.Delete(product.Id);
            return NoContent();
        }
    }
}
