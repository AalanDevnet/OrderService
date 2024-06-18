using Microsoft.Data.SqlClient;
using System.Data;

namespace OrderService.Data
{
    public class OrderContext
    {
        private readonly string _connectionString;

        public OrderContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
