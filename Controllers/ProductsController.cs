using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using API_Server_QLBANHANG.Model;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Firebase.Storage;

namespace API_Server_QLBANHANG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        //Get: api/products
        [HttpGet]
        public JsonResult Get_Products()
        {
            string sql = "SELECT * FROM Products";
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductManagement");

            try
            {
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            dt.Load(sqlDataReader);
                            sqlDataReader.Close();
                        }
                    }
                    conn.Close();
                }

                return new JsonResult(dt);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred", error = ex.Message });
            }
        }

        //Get: api/products/id
        [HttpGet("{id}")]
        public JsonResult Get_Products_By_Id(int id) {
            string sql = "SELECT * FROM Products WHERE Products = " + id;
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductManagement");

            try
            {
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            dt.Load(sqlDataReader);
                            sqlDataReader.Close();
                        }
                    }
                    conn.Close();
                }

                return new JsonResult(dt);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred", error = ex.Message });
            }
        }
        //Post: api/products
        [HttpPost]
        public JsonResult Post_Products(Products product) {
            string sql = @"insert into Products values (" + product.ProductID+ ", N'" + product.Name + "', N'" + product.TypeId + "' ,'0" + product.Importday + "')";
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductManagement");

            try
            {
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            dt.Load(sqlDataReader);
                            sqlDataReader.Close();
                        }
                    }
                    conn.Close();
                }

                return new JsonResult(dt);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred", error = ex.Message });
            }
        }
        // PUT: api/products/id
        [HttpPut("{id}")]
        public JsonResult Put_Products(int id, Products product)
        {
            string sql = @"UPDATE Products 
                   SET Name = N'" + product.Name + @"',
                       TypeId = N'" + product.TypeId + @"',
                       Importday = '" + product.Importday + @"'
                   WHERE ProductID = " + id;
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductManagement");

            try
            {
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            dt.Load(sqlDataReader);
                            sqlDataReader.Close();
                        }
                    }
                    conn.Close();
                }

                return new JsonResult(new { message = "Product updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred", error = ex.Message });
            }
        }

        // DELETE: api/products/id
        [HttpDelete("{id}")]
        public JsonResult Delete_Products(int id)
        {
            string sql = @"DELETE FROM Products WHERE ProductID = " + id;
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProductManagement");

            try
            {
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            dt.Load(sqlDataReader);
                            sqlDataReader.Close();
                        }
                    }
                    conn.Close();
                }

                return new JsonResult(new { message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred", error = ex.Message });
            }
        }
        // POST: api/products/upload
        [Route("UploadFile")]
        [HttpPost]
        public async Task<JsonResult> UploadFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;

                // Khởi tạo Firebase Storage với tên bucket của bạn
                var storage = new FirebaseStorage("banhang-e9c0b.firebasestorage.app");

                // Upload file lên Firebase Storage
                var stream = postedFile.OpenReadStream();
                var uploadTask = await storage
                    .Child("images") // Thư mục trên Firebase Storage
                    .Child(filename)
                    .PutAsync(stream);

                // uploadTask là URL của file đã upload
                string downloadUrl = uploadTask;

                return new JsonResult(new { url = downloadUrl });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred", error = ex.Message });
            }
        }
    }
}
