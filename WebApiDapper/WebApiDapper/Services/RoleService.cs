using Dapper;
using Microsoft.AspNetCore.Identity;
using WebAPICoreDapper.Models;
using WebApiDapper.DbContext;

namespace WebApiDapper.Services
{
    public class RoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly DapperDBContext _dbContext;

        public RoleService(RoleManager<AppRole> roleManager, DapperDBContext context)
        {
            _dbContext = context;
            _roleManager = roleManager;
        }

        public async Task<List<AppRole>> GetAllRole()
        {
            var result = new List<AppRole>();
            using (var conn = _dbContext.CreateConnection())
            {
                var paramaters = new DynamicParameters();
                var queryResult = await conn.QueryAsync<AppRole>("Role_GetAll", paramaters, null, null, System.Data.CommandType.StoredProcedure);
                result.AddRange(queryResult);
            }
            return result;
        }

        public async Task<AppRole?> GetRoleById(Guid id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<IdentityResult> CreateRole(AppRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> UpdateRole(Guid idRole, AppRole roleUpdate)
        {
            roleUpdate.Id = idRole;
            return await _roleManager.UpdateAsync(roleUpdate);
        }

        public async Task<IdentityResult> DeleteRole(string idRole)
        {
            var role = await _roleManager.FindByIdAsync(idRole);
            return await _roleManager.DeleteAsync(role);
        }
    }
}
