using WebApiDapper.DbContext;
using WebApiDapper.Entities;

namespace WebApiDapper.IRepositories.Impl
{
    public class CategoryRepository<K> : Repository<Category, K>, ICategoryRepository<K> where K : struct
    {
        public CategoryRepository(DapperDBContext context) : base(context)
        {
        }
    }
}
