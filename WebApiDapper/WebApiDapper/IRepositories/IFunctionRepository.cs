using WebAPICoreDapper.Models;

namespace WebApiDapper.IRepositories
{
    public interface IFunctionRepository<K> : IRepository<Function, K> where K : class
    {
    }
}
