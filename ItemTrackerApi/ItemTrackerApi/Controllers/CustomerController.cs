using ItemTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public ActionResult SaveCustomerDetails(CustomerRequest CustomerDto)
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

        [HttpGet]
        public ActionResult GetCustomerDetails()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_GetCustomerDetails",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            List<CustomerResponse> customerList = new List<CustomerResponse>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    CustomerResponse customer = new CustomerResponse();
                    customer.CustomerId = reader["CustomerId"].ToString();
                    customer.FirstName = reader["FirstName"].ToString();
                    customer.LastName = reader["LastName"].ToString();
                    customer.Email = reader["Email"].ToString();
                    customer.Mobile = reader["Mobile"].ToString();
                    customer.RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]);

                    customerList.Add(customer);
                }
            }
            
            connection.Close();

            return Ok(JsonConvert.SerializeObject(customerList));
        }

        [HttpDelete]
        public ActionResult DeleteCustomerDetailsById(int CustomerId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteCustomerDetailsById",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            command.Parameters.AddWithValue("@CustomerId", CustomerId);
            command.ExecuteNonQuery();
            connection.Close();
            return Ok(new { message = "Customer deleted successfully." });
        }
    }
}