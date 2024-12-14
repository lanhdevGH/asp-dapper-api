using Dapper;
using System.Text;
using WebApiDapper.DbContext;

namespace WebApiDapper.IRepositories.Impl
{
    public class Repository<T, K> : IRepository<T, K> where T : class
    {
        protected readonly DapperDBContext _dbContext;
        protected readonly string _tableName;

        /// <summary>
        /// Rule: Các bản có tên theo cấu trúc = Tên entity + 's'
        /// Rule: Khóa của mỗi bảng phải có tên là Id
        /// </summary>
        /// <param name="context"></param>
        public Repository(DapperDBContext context)
        {
            var entityName = typeof(T).Name;
            _dbContext = context;
            _tableName = entityName + 's';
        }

        public async Task<K?> AddAsync(T entity)
        {
            var insertQuery = GenerateInsertQuery(["Id"]);

            using (var connection = _dbContext.CreateConnection())
            {
                var insertId = await connection.ExecuteScalarAsync<K>(insertQuery, entity);
                return insertId;
            }
        }

        public async Task DeleteAsync(K id)
        {
            var query = $"DELETE FROM {_tableName} WHERE Id = @Id";

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            };
        }

        public async Task<List<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {_tableName}";

            using (var conn = _dbContext.CreateConnection())
            {
                var result = await conn.QueryAsync<T>(query);
                return result.ToList();
            }
        }

        public async Task<T?> GetByIdAsync(K id)
        {
            var query = $"SELECT * FROM {_tableName} WHERE Id = @Id";

            using (var conn = _dbContext.CreateConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<T>(query, param: new { Id = id });
            }
        }

        public async Task<List<T>> GetPagingAsync(int pageNumber, int pageSize)
        {
            var offset = (pageNumber - 1) * pageSize;

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

        public async Task UpdateAsync(T entity)
        {
            var updateQuery = GenerateUpdateQuery(["Id", "CreateDate"]);

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(updateQuery, entity);
            }
        }

        /// <summary>
        /// Tạo câu lệnh INSERT tự động (tùy chỉnh theo nhu cầu)
        /// </summary>
        private string GenerateInsertQuery(List<string> excludedField)
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} (");
            var properties = typeof(T).GetProperties().Where(p => !excludedField.Contains(p.Name));

            properties.ToList().ForEach(p => insertQuery.Append($"[{p.Name}],"));

            insertQuery.Remove(insertQuery.Length - 1, 1)
                        .Append(") OUTPUT INSERTED.Id VALUES (");

            properties.ToList().ForEach(prop =>
            {
                if (prop.Name == "UpdateDate" || prop.Name == "CreateDate")
                {
                    insertQuery.Append($"GETDATE(),");
                }
                else
                {
                    insertQuery.Append($"@{prop.Name},");
                }
            });

            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");
            return insertQuery.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateUpdateQuery(List<string> excludedField)
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = typeof(T).GetProperties().Where(p => !excludedField.Contains(p.Name));

            properties.ToList().ForEach(prop =>
            {
                if (prop.Name == "UpdateDate")
                {
                    // Gán UpdateDate = GETDATE() trực tiếp trong câu lệnh SQL
                    updateQuery.Append($"{prop.Name} = GETDATE(),");
                }
                else
                {
                    updateQuery.Append($"{prop.Name} = @{prop.Name},");
                }
            });
            // Loại bỏ dấu phẩy cuối và thêm WHERE Id = @Id
            updateQuery
                .Remove(updateQuery.Length - 1, 1)
                .Append(" WHERE Id = @Id");

            return updateQuery.ToString();
        }
    }
}
