using WebAPICoreDapper.Models;
using WebAPICoreDapper.ViewModels;

namespace WebApiDapper.IRepositories
{
    public interface IFunctionRepository<K> : IRepository<Function, K> where K : class
    {
        public Task<List<FunctionActionViewModel>> GetFunctionWithAction();
    }
}
