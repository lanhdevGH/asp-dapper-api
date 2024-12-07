using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using WebApiDapper.DbContext;
using WebApiDapper.DTOs.ProductDTO;
using WebApiDapper.Entities;

namespace WebApiDapper.IRepositories.Impl
{
    public class ProductRepository<K> : Repository<Product, K>, IProductRepository<K> where K : struct
    {
        public ProductRepository(DapperDBContext context) : base(context)
        {

        }

        public async Task<List<ProductCreateResponseDTO>> GetAllProductWithProcedure()
        {
            var procedureName = "GetAllProducts";
            var result = new List<ProductCreateResponseDTO>();  
            using (var connection = _dbContext.CreateConnection())
            {
                var resultQuery = await connection.QueryAsync(procedureName, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<bool> IsSkuExist(string sku, K? id)
        {
            var queryStr = new StringBuilder($"SELECT COUNT(1) FROM {_tableName} WHERE SKU = @Sku ");

            // Kiểm tra nếu id không null, thêm điều kiện vào câu lệnh SQL
            if (id != null)
            {
                queryStr.Append($" AND Id <> @Id"); // Thêm điều kiện Id khác với id hiện tại
            }

            using (var conn = _dbContext.CreateConnection())
            {
                var result = await conn.ExecuteScalarAsync<int>(queryStr.ToString(), new { Sku = sku, Id = id });
                return result > 0; // Trả về true nếu tìm thấy SKU tồn tại
            }
        }
    }
}
