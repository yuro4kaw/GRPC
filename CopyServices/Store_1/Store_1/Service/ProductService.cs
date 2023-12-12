using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Store_1.Model;
using Store_1.Service.interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Store_1.Service
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            List<Product> articles = new List<Product>();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select * from Product", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                Product obj = new Product();
                obj.id = int.Parse(row["id"].ToString());
                obj.productName = row["ProductName"].ToString();
                obj.price = decimal.Parse(row["Price"].ToString());
                obj.description = row["Description"].ToString();
                obj.sellerId = int.Parse(row["SellerID"].ToString());

                articles.Add(obj);
            }

            return articles;
        }

        public async Task<Product> GetAsync(string name)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var product = await connection.QueryFirstAsync<Product>("select * from Product where productName = @productName",
                new { productName = name });
            return product;
        }
    }
}
