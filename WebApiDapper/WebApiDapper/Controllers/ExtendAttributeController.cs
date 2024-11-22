using Microsoft.AspNetCore.Mvc;
using WebApiDapper.ActionFilters;
using WebApiDapper.DTOs.ExtendAttribute;
using WebApiDapper.Entities;
using WebApiDapper.Services;

namespace WebApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtendAttributeController : ControllerBase
    {
        private readonly ExtendAttributeService<int> _extendAttributeService;
        public ExtendAttributeController(ExtendAttributeService<int> extendAttributeService)
        {
            _extendAttributeService = extendAttributeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExtendAttribute()
        {
            var extendAttributes = await _extendAttributeService.GetAllAttribute();
            return Ok(extendAttributes);
        }

        [HttpGet("code")]
        public async Task<IActionResult> GetExtendAttributeByCode([FromQuery] string extendAttCode)
        {
            var extendAttributes = await _extendAttributeService.GetExtendAttributeByCodeAsync(extendAttCode);
            return Ok(extendAttributes);
        }

        //[HttpGet("page")]
        //[ExceptionHandleFilter]
        //public async Task<IActionResult> GetProductByPaging([FromQuery] int pageNumber, [FromQuery] int pageSize)
        //{
        //    var products = await _productRepo.GetPagingAsync(pageNumber, pageSize);
        //    return Ok(products);
        //}

        [HttpGet("id")]
        public async Task<IActionResult> GetExtendAttributeById(int id)
        {
            var result = await _extendAttributeService.GetExtendAttributeByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateExtendAttribute([FromBody] ExtendAttributeCreateRequestDTO extendAttribute)
        {
            var extendAttributeId = await _extendAttributeService.CreateExtendAttribute(extendAttribute);
            return CreatedAtAction(nameof(GetExtendAttributeById), extendAttributeId);
        }

        [HttpPut("id")]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<ExtendAttribute, int>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateExtendAttribute(int id, [FromBody] ExtendAttributeCreateRequestDTO extendAttribute)
        {
            var existingExtendAttribute = HttpContext.Items["Entity"] as ExtendAttribute;
            if (existingExtendAttribute == null)
                return NotFound();
            await _extendAttributeService.UpdateExtendAttribute(existingExtendAttribute.Id, extendAttribute);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<ExtendAttribute, int>))]
        public async Task<IActionResult> DeleteExtendAttribute(int id)
        {
            var extendAttribute = HttpContext.Items["Entity"] as ExtendAttribute;
            if (extendAttribute != null)
            {
                await _extendAttributeService.DeleteExtendAttribute(extendAttribute.Code);
            }
            return NoContent();
        }
    }
}
