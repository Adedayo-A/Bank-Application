using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankApplication.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        // GET: api/<controller>
        [HttpPost("createadmin")]
        public IActionResult CreateAdmin([FromBody] NewAdminDetails newAdmin)
        {
            SqlConnection dbConnect = Connection.Connectionstring;

            //object[] storeDetails = new object[6];
            List<string> storeDetails = new List<string>();

            string firstName = newAdmin.FirstName;
            string lastName = newAdmin.LastName;
            string email = newAdmin.Email;
            string type = newAdmin.AdminType;
            string username = newAdmin.Username;
            string password = newAdmin.Password;

            storeDetails.Add(firstName);
            storeDetails.Add(lastName);
            storeDetails.Add(email);
            storeDetails.Add(type);
            storeDetails.Add(username);
            storeDetails.Add(password);

            foreach (var item in storeDetails)
            {
                if (item == "")
                {
                    return NotFound("ALL Details must be complete");
                }
            }
            string sqlText = @"INSERT INTO Admin VALUES(@fName, @lName, @email, 
                                @password, @username, @type)";
            SqlCommand command = new SqlCommand(sqlText, dbConnect);

            command.Parameters.AddWithValue("@fName", firstName);
            command.Parameters.AddWithValue("@lName", lastName);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            try
            {
                dbConnect.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    dbConnect.Close();
                    return Ok("Admin Created Successfully");
                }
                else
                {
                    dbConnect.Close();
                    return NotFound("Error Occured");
                }
            }
            catch (Exception ex)
            {
                dbConnect.Close();
                return NotFound("The following error occurred during the write operation: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }





        [HttpPost("createCustomer")]
        public IActionResult CreateCustomer([FromBody] NewCustomerDetails newCustomer)
        {
            SqlConnection dbConnect = Connection.Connectionstring;

            //object[] storeDetails = new object[6];
            List<string> storeDetails = new List<string>();

            string firstName = newCustomer.FirstName;
            string lastName = newCustomer.LastName;
            string email = newCustomer.Email;
            string password = newCustomer.Password;
            string accountType = newCustomer.AccountType;
            int balance = newCustomer.Balance;
            string accNum = "00505";
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000, 99999);
            accNum += myRandomNo.ToString();
            int adminId = 1;

            storeDetails.Add(firstName);
            storeDetails.Add(lastName);
            storeDetails.Add(email);
            storeDetails.Add(password);
            storeDetails.Add(accountType);

            foreach (var item in storeDetails)
            {
                if (item == "")
                {
                    return NotFound("ALL details must be completed");
                }
            }
            string sqlText = @"INSERT INTO Customer VALUES(@fName, @lName, @email, 
                                @password, @accNum, @adminId, @balance, @accType )";
            SqlCommand command = new SqlCommand(sqlText, dbConnect);

            command.Parameters.AddWithValue("@fName", firstName);
            command.Parameters.AddWithValue("@lName", lastName);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@accNum", accNum);
            command.Parameters.AddWithValue("@adminId", adminId);
            command.Parameters.AddWithValue("@balance", balance);
            command.Parameters.AddWithValue("@accType", accountType);


            try
            {
                dbConnect.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    dbConnect.Close();
                    return Ok("Customer Created Successfully");
                }
                else
                {
                    dbConnect.Close();
                    return NotFound("Error Occured");
                }
            }
            catch (Exception ex)
            {
                dbConnect.Close();
                return NotFound("The following error occurred during the write operation: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }



        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
