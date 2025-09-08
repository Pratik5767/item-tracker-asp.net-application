using ItemTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ItemTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly string _connectionString;

        public InventoryController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public ActionResult SaveInventoryData(InventoryRequest InventoryReq)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_SaveInventoryData",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            command.Parameters.AddWithValue("@ProductId", InventoryReq.ProductId);
            command.Parameters.AddWithValue("@ProductName", InventoryReq.ProductName);
            command.Parameters.AddWithValue("@StockAvailable", InventoryReq.StockAvailable);
            command.Parameters.AddWithValue("@ReorderStock", InventoryReq.ReorderStock);
            command.ExecuteNonQuery();
            connection.Close();

            return Ok(new { message = "Inventory data saved successfully." });
        }

        [HttpGet]
        public ActionResult GetInventoryData()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_GetInventoryData",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            List<InventoryResponse> inventoryList = new List<InventoryResponse>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    InventoryResponse inventoryDto = new InventoryResponse();
                    inventoryDto.ProductId = Convert.ToInt32(reader["ProductId"]);
                    inventoryDto.ProductName = reader["ProductName"].ToString();
                    inventoryDto.StockAvailable = Convert.ToInt32(reader["StockAvailable"]);
                    inventoryDto.ReorderStock = Convert.ToInt32(reader["ReorderStock"]);
                    inventoryList.Add(inventoryDto);
                }
            }
            connection.Close();
            return Ok(JsonConvert.SerializeObject(inventoryList));
        }

        [HttpDelete]
        public ActionResult DeleteInventoryData(int ProductId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteInventoryDataById",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            command.Parameters.AddWithValue("@ProductId", ProductId);
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateInventoryData(InventoryRequest InventoryDto)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_UpdateInventoryData",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            command.Parameters.AddWithValue("@ProductId", InventoryDto.ProductId);
            command.Parameters.AddWithValue("@ProductName", InventoryDto.ProductName);
            command.Parameters.AddWithValue("@StockAvailable", InventoryDto.StockAvailable);
            command.Parameters.AddWithValue("@ReorderStock", InventoryDto.ReorderStock);
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }
    }
}
