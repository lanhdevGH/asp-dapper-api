using Microsoft.AspNetCore.Mvc;
using WebApiDapper.Entities;
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

        [HttpGet("id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productRepo.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = await _productRepo.GetById(id);
            if (existingProduct == null)
                return NotFound();
            existingProduct.Name = product.Name;
            await _productRepo.Update(existingProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepo.Delete(id);
            return NoContent();
        }
    }
}
