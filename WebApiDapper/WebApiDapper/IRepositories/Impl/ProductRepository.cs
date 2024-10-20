using WebApiDapper.DbContext;
using WebApiDapper.Entities;

namespace WebApiDapper.IRepositories.Impl
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DapperDBContext context) : base(context)
        {
        }
    }
}
