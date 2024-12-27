using Microsoft.AspNetCore.Mvc;
using WebAPICoreDapper.Models;
using WebApiDapper.ActionFilters;
using WebApiDapper.DTOs.UserDTO;
using WebApiDapper.Services;

namespace WebApiDapper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var roles = await _userService.GetAllUser();
            return Ok(roles);
        }


        //[HttpGet("paging")]
        //public async Task<IActionResult> GetPaging(string condision, int pageIndex, int pageSize)
        //{
        //    using (var conn = new SqlConnection(_connectionString))
        //    {
        //        if (conn.State == System.Data.ConnectionState.Closed)
        //            await conn.OpenAsync();

        //        var paramaters = new DynamicParameters();
        //        paramaters.Add("@keyword", keyword);
        //        paramaters.Add("@pageIndex", pageIndex);
        //        paramaters.Add("@pageSize", pageSize);
        //        paramaters.Add("@totalRow", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        //        var result = await conn.QueryAsync<AppRole>("Get_Role_AllPaging", paramaters, null, null, System.Data.CommandType.StoredProcedure);

        //        int totalRow = paramaters.Get<int>("@totalRow");

        //        var pagedResult = new PagedResult<AppRole>()
        //        {
        //            Items = result.ToList(),
        //            TotalRow = totalRow,
        //            PageIndex = pageIndex,
        //            PageSize = pageSize
        //        };
        //        return Ok(pagedResult);
        //    }
        //}

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var role = await _userService.GetUserById(id.ToString());
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDTO userRequest)
        {
            var result = await _userService.CreateUser(userRequest);
            if (result.Succeeded)
            {
                //return CreatedAtAction(nameof(GetUserById), new { id = result.Id.ToString() }, result);
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UserUpdateDTO userRequest)
        {
            var result = await _userService.UpdateUser(id, userRequest);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationIsExistEntity<AppRole>))]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _userService.DeleteUser(id.ToString());
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
