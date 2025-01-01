using Microsoft.AspNetCore.Mvc;
using WebApiDapper.DTOs.CategoryDTO;
using WebApiDapper.Entities;
using WebApiDapper.Filter.ActionFilters;
using WebApiDapper.Filter.Auth;
using WebApiDapper.Services;

namespace WebApiDapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private CategoryService _categoryService { get; set; }

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ClaimRequirementFilter(Enums.FunctionCode.SYSTEM_USER, Enums.ActionCode.VIEW)]
        public async Task<IActionResult> GetAllCategory()
        {
            var categorys = await _categoryService.GetAllCategory();
            return Ok(categorys);
        }

        //[HttpGet("page")]
        //[ExceptionHandleFilter]
        //public async Task<IActionResult> GetcategoryByPaging([FromQuery] int pageNumber, [FromQuery] int pageSize)
        //{
        //    var categorys = await _categoryRepo.GetPagingAsync(pageNumber, pageSize);
        //    return Ok(categorys);
        //}

        [HttpGet("id")]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<Category, int>))]
        public IActionResult GetcategoryById(int id)
        {
            var category = HttpContext.Items["Entity"] as Category;
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationIsExistEntity<CategoryRequestDTO>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Createcategory([FromBody] CategoryRequestDTO category)
        {
            var categoryId = await _categoryService.CreateCategory(category);
            return CreatedAtAction(nameof(GetcategoryById), categoryId);
        }

        [HttpPut("id")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<Category, int>))]
        public async Task<IActionResult> Updatecategory(int id, [FromBody] CategoryRequestDTO categoryRequest)
        {
            var existingcategory = HttpContext.Items["Entity"] as Category;
            if (existingcategory == null)
                return NotFound();
            await _categoryService.UpdateCategory(categoryRequest, existingcategory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationNotExistEntityAttribute<Category, int>))]
        public async Task<IActionResult> Deletecategory(int id)
        {
            var category = HttpContext.Items["Entity"] as Category;
            if (category == null) return NotFound();
            await _categoryService.DeleteCategory(category.Id);
            return NoContent();
        }
    }
}
