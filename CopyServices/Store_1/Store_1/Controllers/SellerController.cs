using Store_1.Model;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Store_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SellerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Seller>>> GetAllSeller()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            IEnumerable<Seller> seller = await SelelAllSeller(connection);
            return Ok(seller);

        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<ActionResult<List<Seller>>> GetAuthor(int sellerId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var seller = await connection.QueryFirstAsync<Seller>("select * from Seller where id = @id",
                new {id = sellerId});
            return Ok(seller);

        }

        [HttpPost]
        public async Task<ActionResult<List<Seller>>> CreatedAuthor(Seller seller)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("insert into Seller (SellerName, Email) values (@SellerName, @Email)",seller);
            return Ok(await SelelAllSeller(connection));

        }
        [HttpPut]
        public async Task<ActionResult<List<Seller>>> UpdateAuthor(Seller seller)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(
                "update Seller set SellerName = @SellerName, Email = @Email where id = @id",
                 new{id = seller.id , SellerName = seller.SellerName, Email = seller.email});
            return Ok(await SelelAllSeller(connection));

        }

        [HttpDelete]
        [Route("DeleteSellerByID")]
        public async Task<ActionResult<List<Seller>>> DeleteAuthor(int sellerId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from Seller where id = @id", new {id = sellerId});
            return Ok(await SelelAllSeller(connection));

        }

        private static async Task<IEnumerable<Seller>> SelelAllSeller(SqlConnection connection)
        {
            return await connection.QueryAsync<Seller>("select * from Seller");
        }
    }
}
