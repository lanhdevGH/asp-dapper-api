using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebAPICoreDapper.Models;
using WebApiDapper.Services;

namespace WebApiDapper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var roles = await _roleService.GetAllRole();
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
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] AppRole role)
        {
            var result = await _roleService.CreateRole(role);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] AppRole role)
        {
            var result = await _roleService.UpdateRole(id, role);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleService.DeleteRole(id);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
