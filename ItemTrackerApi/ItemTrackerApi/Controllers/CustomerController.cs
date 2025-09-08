using ItemTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ItemTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly string _connectionString;

        public CustomerController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public ActionResult SaveCustomerDetails(CustomerRequestDto CustomerDto)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_SaveCustomerDetails",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            command.Parameters.AddWithValue("@CustomerId", CustomerDto.CustomerId);
            command.Parameters.AddWithValue("@FirstName", CustomerDto.FirstName);
            command.Parameters.AddWithValue("@LastName", CustomerDto.LastName);
            command.Parameters.AddWithValue("@Email", CustomerDto.Email);
            command.Parameters.AddWithValue("@Mobile", CustomerDto.Mobile);
            command.Parameters.AddWithValue("@RegistrationDate", CustomerDto.RegistrationDate);
            command.ExecuteNonQuery();
            connection.Close();

            return Ok(new { message = "Customer added successfully." });
        }
    }
}
