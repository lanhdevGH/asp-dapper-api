using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAPICoreDapper.ViewModels;
using WebApiDapper.Extensions;
using WebApiDapper.Services;

namespace WebApiDapper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly FunctionService _functionService;
        private readonly PermissionService _permissionService;
        public PermissionController(FunctionService functionService, PermissionService permissionService)
        {
            _functionService = functionService;
            _permissionService = permissionService;
        }

        [HttpGet("function-actions")]
        public async Task<IActionResult> GetAllWithPermission()
        {
            var result = await _functionService.GetFunctionWithAction();
            return Ok(result);
        }

        [HttpGet("{role}/role-permissions")]
        public async Task<IActionResult> GetAllRolePermissions(Guid? roleId)
        {
            var result = await _permissionService.GetAllRolePermissions(roleId);
            return Ok(result);
        }

        [HttpPost("{role}/save-permissions")]
        public async Task<IActionResult> SavePermissions(Guid roleId, [FromBody] List<PermissionViewModel> permissions)
        {
            await _permissionService.SavePermissions(roleId, permissions);
            return Ok();
        }

        [HttpGet("functions-view")]
        public async Task<IActionResult> GetAllFunctionByPermission()
        {
            var userId = User.GetUserId();    
            var result = await _permissionService.GetAllFunctionByPermission(Guid.Parse(HttpContext.User.Identity.Name));
            return Ok(result);
        }
    }
}
