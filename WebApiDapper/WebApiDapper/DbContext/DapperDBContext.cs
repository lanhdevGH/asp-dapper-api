using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApiDapper.DbContext
{
    public class DapperDBContext 
    {
        private readonly IConfiguration _configuration;
        private readonly string _connection;

        public DapperDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public IDbConnection CreateConnection()
        => new SqlConnection(_connection);
    }
}
