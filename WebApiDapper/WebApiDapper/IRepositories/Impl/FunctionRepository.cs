using WebAPICoreDapper.Models;
using WebApiDapper.DbContext;

namespace WebApiDapper.IRepositories.Impl
{
    public class FunctionRepository<K> : Repository<Function, K>, IFunctionRepository<K> where K : class
    {
        public FunctionRepository(DapperDBContext context) : base(context)
        {
            
        }
    }
}
