using Store_1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using MassTransit;

namespace Store_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductController (IPublishEndpoint publish ,IConfiguration config)
        {
            _configuration = config;

        }

        [HttpGet]
        [Route("GetAll")]
        public List<Product> GetAllArticle()
        {
            List<Product> articles = new List<Product>();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select * from Product", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach(DataRow row in dt.Rows)
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


        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult CreateArticle(Product product)
        {
            using(SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                string insertQuery = "Insert into Product (ProductName, Price, Description, SellerID) Values (@ProductName, @Price, @Description, @SellerID);";

                using(SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", product.productName);
                    cmd.Parameters.AddWithValue("@Price", product.price);
                    cmd.Parameters.AddWithValue("@Description", product.description);
                    cmd.Parameters.AddWithValue("@SellerID", product.sellerId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Стаття створена");
                    }
                    else
                    {
                        return BadRequest("Стаття не була створена");
                    }
                }
            }
        }

        //[HttpGet]
        //[Route("GetAllArticleWithComments")]
        //public List<object> GetAllArticleWithComments()
        //{
        //    List<object> articlesWithComments = new List<object>();

        //    using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = new SqlCommand("AllArticleWithComments", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    object articleWithComments = new
        //                    {
        //                        ArticleID = reader.GetInt32(reader.GetOrdinal("id")),
        //                        Title = reader.GetString(reader.GetOrdinal("Title")),
        //                        Content = reader.GetString(reader.GetOrdinal("Content")),
        //                        CommentID = reader.GetInt32(reader.GetOrdinal("CommentID")),
        //                        CommentText = reader.GetString(reader.GetOrdinal("CommentText"))
        //                    };

        //                    articlesWithComments.Add(articleWithComments);
        //                }
        //            }
        //        }
        //    }

        //    return articlesWithComments;
        //}

        [HttpPut]
        [Route("UpadteProduct")]
        public IActionResult UpdateArticle(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                string query = "update Product set ProductName = @ProductName, Price = @Price, Description = @Description, SellerID = @SellerID where id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", product.id);
                    cmd.Parameters.AddWithValue("@ProductName", product.productName);
                    cmd.Parameters.AddWithValue("@Price", product.price);
                    cmd.Parameters.AddWithValue("@Description", product.description);
                    cmd.Parameters.AddWithValue("@SellerID", product.sellerId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(GetAllArticle());
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public IActionResult DeleteArticle(int ProductID)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("delete from Product where id=@id ", conn))
                {
                    cmd.Parameters.AddWithValue("@id", ProductID);

                    int rowAffected  = cmd.ExecuteNonQuery();

                    if(rowAffected > 0)
                    {
                        return Ok("Статтю Видалено");
                    }
                    else { return NotFound(); }
                }
            }

        }

    }
}
