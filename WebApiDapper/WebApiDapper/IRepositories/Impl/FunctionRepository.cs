using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAPICoreDapper.Models;
using WebAPICoreDapper.ViewModels;
using WebApiDapper.DbContext;

namespace WebApiDapper.IRepositories.Impl
{
    public class FunctionRepository<K> : Repository<Function, K>, IFunctionRepository<K> where K : class
    {
        public FunctionRepository(DapperDBContext context) : base(context)
        {
            
        }

        public async Task<List<FunctionActionViewModel>> GetFunctionWithAction()
        {
            using (var conn = _dbContext.CreateConnection())
            {
                var result = await conn.QueryAsync<FunctionActionViewModel>("Get_Function_WithActions", null, null, null,CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
