using Dapper;
using System.Data;
using WebAPICoreDapper.ViewModels;
using WebApiDapper.DbContext;

namespace WebApiDapper.Services
{
    public class PermissionService
    {
        private readonly DapperDBContext _dapperDBContext;

        public PermissionService(DapperDBContext dapperDBContext)
        {
            _dapperDBContext = dapperDBContext;
        }

        public async Task<List<PermissionViewModel>> GetAllRolePermissions(Guid? roleId)
        {
            using (var conn = _dapperDBContext.CreateConnection())
            {
                var paramaters = new DynamicParameters();
                paramaters.Add("@roleId", roleId);
                var result = await conn.QueryAsync<PermissionViewModel>("Get_Permission_ByRoleId", paramaters, null, null, CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task SavePermissions(Guid roleId, List<PermissionViewModel> permissions)
        {
            using (var conn = _dapperDBContext.CreateConnection())
            {
                var dt = new DataTable();
                dt.Columns.Add("RoleId", typeof(Guid));
                dt.Columns.Add("FunctionId", typeof(string));
                dt.Columns.Add("ActionId", typeof(string));
                foreach (var item in permissions)
                {
                    dt.Rows.Add(roleId, item.FunctionId, item.ActionId);
                }
                var paramaters = new DynamicParameters();
                paramaters.Add("@permissions", dt.AsTableValuedParameter("dbo.PermissionTableType"));
                await conn.ExecuteAsync("Save_Permissions", paramaters, null, null, CommandType.StoredProcedure);
            }
        }

        public async Task<List<FunctionViewModel>> GetAllFunctionByPermission(Guid userId)
        {
            using (var conn = _dapperDBContext.CreateConnection())
            {
                var paramaters = new DynamicParameters();
                paramaters.Add("@userId", userId);

                var result = await conn.QueryAsync<FunctionViewModel>("Get_Function_ByPermission", paramaters, null, null, System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
