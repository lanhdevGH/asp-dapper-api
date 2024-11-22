using Dapper;
using WebApiDapper.DbContext;
using WebApiDapper.Entities;

namespace WebApiDapper.IRepositories.Impl
{
    public class ExtendAttributeRepository<K> : Repository<ExtendAttribute, K>, IExtendAttributeRepository<K>
    {

        public ExtendAttributeRepository(DapperDBContext context) : base(context) { }

        public async Task<ExtendAttribute?> GetAttributeByCodeAsync(string attributeCode)
        {
            var query = $"SELECT * FROM {_tableName} WHERE Code = @Code";

            using (var conn = _dbContext.CreateConnection())
            {
                var attributeRecord = await conn.QueryFirstAsync<ExtendAttribute>(query, param: new { Code = attributeCode });
                return attributeRecord;
            }

        }

        public async Task<bool> IsExistAttributeAsync(string attributeCode)
        {
            var query = $"SELECT COUNT(1) FROM {_tableName} WHERE AttributeCode = @AttributeCode";

            using (var conn = _dbContext.CreateConnection())
            {
                int quantity = await conn.QuerySingleAsync<int>(query, new { AttributeCode = attributeCode });
                return quantity > 0;
            }
        }
    }

    public class AttributeValueNVarcharRepository<K> : Repository<AttributeValueNVarchar, K>, IAttributeValueNVarcharRepository<K>
    {
        public AttributeValueNVarcharRepository(DapperDBContext context) : base(context)
        {
        }

        public async Task DeleteProductAttributeValueAsync(int productID)
        {
            var queryStr = $"DELETE FROM {_tableName} WHERE ProductId = @Id";
            using (var conn = _dbContext.CreateConnection())
            {
                await conn.ExecuteAsync(queryStr, param: new { Id = productID });
            }
        }

        public async Task<List<AttributeValueNVarchar>> GetAttributeValueByProdIDAsync(int productID)
        {
            var query = $"Select * From {_tableName} Where ProductId = @Id";
            using(var conn = _dbContext.CreateConnection())
            {
                var result = await conn.QueryAsync<AttributeValueNVarchar>(query, new { Id = productID });
                return result.ToList();
            }
        }


    }
    public class AttributeValueTextRepository<K> : Repository<AttributeValueText, K>, IAttributeValueTextRepository<K>
    {
        public AttributeValueTextRepository(DapperDBContext context) : base(context)
        {
        }

        public async Task DeleteProductAttributeValueAsync(int productID)
        {
            var queryStr = $"DELETE FROM {_tableName} WHERE ProductId = @Id";
            using (var conn = _dbContext.CreateConnection())
            {
                await conn.ExecuteAsync(queryStr, param: new { Id = productID });
            }
        }

        public async Task<List<AttributeValueText>> GetAttributeValueByProdIDAsync(int productID)
        {
            var query = $"Select * From {_tableName} Where ProductId = @Id";
            using (var conn = _dbContext.CreateConnection())
            {
                var result = await conn.QueryAsync<AttributeValueText>(query, new { Id = productID });
                return result.ToList();
            }
        }
    }
    public class AttributeValueIntRepository<K> : Repository<AttributeValueInt, K>, IAttributeValueIntRepository<K>
    {
        public AttributeValueIntRepository(DapperDBContext context) : base(context)
        {
        }

        public async Task DeleteProductAttributeValueAsync(int productID)
        {
            var queryStr = $"DELETE FROM {_tableName} WHERE ProductId = @Id";
            using (var conn = _dbContext.CreateConnection())
            {
                await conn.ExecuteAsync(queryStr, param: new { Id = productID });
            }
        }

        public async Task<List<AttributeValueInt>> GetAttributeValueByProdIDAsync(int productID)
        {
            var query = $"Select * From {_tableName} Where ProductId = @Id";
            using (var conn = _dbContext.CreateConnection())
            {
                var result = await conn.QueryAsync<AttributeValueInt>(query, new { Id = productID });
                return result.ToList();
            }
        }
    }
    public class AttributeValueDecimalRepository<K> : Repository<AttributeValueDecimal, K>, IAttributeValueDecimalRepository<K>
    {
        public AttributeValueDecimalRepository(DapperDBContext context) : base(context)
        {
        }

        public async Task DeleteProductAttributeValueAsync(int productID)
        {
            var queryStr = $"DELETE FROM {_tableName} WHERE ProductId = @Id";
            using (var conn = _dbContext.CreateConnection())
            {
                await conn.ExecuteAsync(queryStr, param: new { Id = productID });
            }
        }

        public async Task<List<AttributeValueDecimal>> GetAttributeValueByProdIDAsync(int productID)
        {
            var query = $"Select * From {_tableName} Where ProductId = @Id";
            using (var conn = _dbContext.CreateConnection())
            {
                var result = await conn.QueryAsync<AttributeValueDecimal>(query, new { Id = productID });
                return result.ToList();
            }
        }
    }
    public class AttributeValueDateTimeRepository<K> : Repository<AttributeValueDateTime, K>, IAttributeValueDateTimeRepository<K>
    {
        public AttributeValueDateTimeRepository(DapperDBContext context) : base(context)
        {
        }

        public async Task DeleteProductAttributeValueAsync(int productID)
        {
            var queryStr = $"DELETE FROM {_tableName} WHERE ProductId = @Id";
            using (var conn = _dbContext.CreateConnection())
            {
                await conn.ExecuteAsync(queryStr, param: new { Id = productID });
            }
        }

        public async Task<List<AttributeValueDateTime>> GetAttributeValueByProdIDAsync(int productID)
        {
            var query = $"Select * From {_tableName} Where ProductId = @Id";
            using (var conn = _dbContext.CreateConnection())
            {
                var result = await conn.QueryAsync<AttributeValueDateTime>(query, new { Id = productID });
                return result.ToList();
            }
        }
    }
}
