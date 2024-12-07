using WebApiDapper.DTOs.ProductDTO;
using WebApiDapper.Entities;

namespace WebApiDapper.IRepositories
{
    public interface IProductRepository<K> : IRepository<Product, K> where K : struct
    {
        public Task<List<ProductCreateResponseDTO>> GetAllProductWithProcedure();
        public Task<bool> IsSkuExist(string sku, K? id );
    }
}
