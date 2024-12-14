using WebApiDapper.Entities;

namespace WebApiDapper.IRepositories
{
    public interface ICategoryRepository<K> : IRepository<Category,K> where K : struct
    {
    }
}
