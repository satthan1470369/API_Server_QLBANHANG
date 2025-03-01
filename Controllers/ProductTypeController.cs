using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using API_Server_QLBANHANG.Model;
using Microsoft.AspNetCore.Authorization;

namespace API_Server_QLBANHANG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductTypeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductTypeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/ProductType
        [HttpGet]
        public JsonResult Get_ProductTypes()
        {
            string sql = "SELECT * FROM ProductType";
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

        // GET: api/ProductType/{id}
        [HttpGet("{id}")]
        public JsonResult Get_ProductType_By_Id(int id)
        {
            string sql = "SELECT * FROM ProductType WHERE TypeId = " + id;
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

        // POST: api/ProductType
        [HttpPost]
        public JsonResult Post_ProductType(ProductType productType)
        {
            string sql = @"INSERT INTO ProductType (TypeName) 
                           VALUES (N'" + productType.TypeName + "')";
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

        // PUT: api/ProductType/{id}
        [HttpPut("{id}")]
        public JsonResult Put_ProductType(int id, ProductType productType)
        {
            string sql = @"UPDATE ProductType 
                           SET TypeName = N'" + productType.TypeName + @"'
                           WHERE TypeId = " + id;
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

                return new JsonResult(new { message = "ProductType updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred", error = ex.Message });
            }
        }

        // DELETE: api/ProductType/{id}
        [HttpDelete("{id}")]
        public JsonResult Delete_ProductType(int id)
        {
            string sql = @"DELETE FROM ProductType WHERE TypeId = " + id;
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

                return new JsonResult(new { message = "ProductType deleted successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = "An error occurred", error = ex.Message });
            }
        }
    }
}
