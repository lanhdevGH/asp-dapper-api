
using Dapper;
using System.Text;
using WebApiDapper.DbContext;

namespace WebApiDapper.IRepositories.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DapperDBContext _dbContext;
        protected readonly string _tableName;

        /// <summary>
        /// Rule: Các bản có tên theo cấu trúc = Tên entity + 's'
        /// </summary>
        /// <param name="context"></param>
        public Repository(DapperDBContext context)
        {
            var entityName = typeof(T).Name;
            _dbContext = context;
            _tableName = entityName + 's';
        }
        public async Task Add(T entity)
        {
            var insertQuery = GenerateInsertQuery();

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, entity);
            }
        }

        public async Task Delete(int id)
        {
            var query = $"DELETE FROM {_tableName} WHERE Id = @Id";

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            };
        }

        public async Task<List<T>> GetAll()
        {
            var query = $"SELECT * FROM {_tableName}";

            using (var conn = _dbContext.CreateConnection())
            {
                var result = await conn.QueryAsync<T>(query);
                return result.ToList();
            }
        }

        public async Task<T?> GetById(int id)
        {
            var query = $"SELECT * FROM {_tableName} WHERE Id = @Id";

            using (var conn = _dbContext.CreateConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<T>(query, param: new { Id = id });
            }
        }

        public async Task<List<T>> GetPaging(int pageNumber, int pageSize)
        {
            var offset = (pageNumber -1) * pageSize;

            var query = $@"
            SELECT * 
            FROM {_tableName}
            ORDER BY Id
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY";

            using (var connection = _dbContext.CreateConnection())
            {
                var result = await connection.QueryAsync<T>(query, new { Offset = offset, PageSize = pageSize });
                return result.ToList();
            }
        }

        public async Task Update(T entity)
        {
            var updateQuery = GenerateUpdateQuery();

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(updateQuery, entity);
            }
        }

        /// <summary>
        /// Tạo câu lệnh INSERT tự động (tùy chỉnh theo nhu cầu)
        /// </summary>
        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} (");
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");

            properties.ToList().ForEach(p => insertQuery.Append($"[{p.Name}],"));

            insertQuery.Remove(insertQuery.Length - 1, 1)
                        .Append(") VALUES (");

            properties.ToList().ForEach(prop => insertQuery.Append($"@{prop.Name},"));
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");
            return insertQuery.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");

            properties.ToList().ForEach(prop => { updateQuery.Append($"{prop.Name} = @{prop.Name},"); });

            updateQuery
                .Remove(updateQuery.Length - 1, 1)
                .Append(" WHERE Id = @Id");

            return updateQuery.ToString();
        }
    }
}
