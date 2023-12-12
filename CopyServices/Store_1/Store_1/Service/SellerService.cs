using Dapper;
using Store_1.Model;
using Store_1.Service.interfaces;
using System.Data.SqlClient;

namespace Store_1.Service
{
    public class SellerService : ISellerService
    {
        private readonly IConfiguration _configuration;
        public SellerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Seller>> GetAllAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            IEnumerable<Seller> seller = await SelelAllSeller(connection);
            return seller.ToList();
        }
        private static async Task<IEnumerable<Seller>> SelelAllSeller(SqlConnection connection)
        {
            return await connection.QueryAsync<Seller>("select * from Seller");
        }

        public async Task<Seller> GetAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var seller = await connection.QueryFirstAsync<Seller>("select * from Seller where id = @id",
                new { id = id });
            return seller;
        }


    }
}
