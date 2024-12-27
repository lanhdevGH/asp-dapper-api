using Microsoft.AspNetCore.Mvc;
using WebApiDapper.DTOs.FunctionDTO;
using WebApiDapper.Services;

namespace WebApiDapper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly FunctionService _functionService;

        public FunctionController(FunctionService functionService)
        {
            _functionService = functionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _functionService.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var entity = await _functionService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FunctionRequestDTO entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity cannot be null.");
            }

            var createdEntityId = await _functionService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntityId }, createdEntityId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] FunctionRequestDTO entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity cannot be null.");
            }

            var existingEntity = await _functionService.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _functionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
