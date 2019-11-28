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
    public class CustomerController : Controller
    {
        // GET: api/<controller>
        [HttpPut("deposit")]
        public IActionResult Deposit([FromBody] Deposit depositAmount)
        {
            List<string> storeDetails = new List<string>();
            SqlConnection dbConnect = Connection.Connectionstring;
            string depositAmt = depositAmount.DepositAmount;
            string accNum = depositAmount.AccountNumber;
            int accountBalance = 0;
            int depositamt = 0;
            int depositNo;

            storeDetails.Add(depositAmt);
            storeDetails.Add(accNum);

                bool success = int.TryParse(depositAmt, out depositNo);
                depositamt += depositNo;
                if (depositAmt == "")
                {
                    return NotFound("Deposit amount cannot be empty");
                }
                else if (!success)
                {
                    return NotFound("Deposit amount must be a number");
                }

            string sqlText = @"SELECT * FROM Customer
                            WHERE CustomerAccountNumber = @accNum";

            SqlCommand command = new SqlCommand(sqlText, dbConnect);

            command.Parameters.AddWithValue("@accNum", accNum);

            try
            {
                dbConnect.Open();
                SqlDataReader result = command.ExecuteReader();
                if (result.Read())
                {
                    int balance = int.Parse(result.GetValue(7).ToString());
                    accountBalance += balance;
                }
                else
                {
                    dbConnect.Close();
                    return Content("Error");
                }
            }
            catch (Exception ex)
            {
                dbConnect.Close();
                return NotFound("The following error occurred during the write operation 2: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            
            accountBalance += depositamt;

            string sqlTxt = @"UPDATE Customer SET CustomerBalance = @deposit WHERE CustomerAccountNumber = @accNo";
            SqlCommand cmd = new SqlCommand(sqlTxt, dbConnect); ;

            cmd.Parameters.AddWithValue("@deposit", accountBalance);
            cmd.Parameters.AddWithValue("@accNo", accNum);

            try
            {
                dbConnect.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    dbConnect.Close();
                    return Ok("Deposit Successful");
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


        [HttpPut("withdraw")]

        public IActionResult Withdraw([FromBody] Withdrawcs withdrawal)
        {
            SqlConnection dbConnect = Connection.Connectionstring;
            string withdrawAmt = withdrawal.WithdrawAmount;
            string accNum = withdrawal.AccountNumber;
            int accountBalance = 0;
            int withdrawamt = 0;
            int withdrawalNo;

            bool success = int.TryParse(withdrawAmt, out withdrawalNo);

            if (withdrawAmt == "")
            {
                return NotFound("Withdraw amount cannot be empty");
            }
            else if (!success)
            {
                return NotFound("Withdraw amount must be a number");
            }

            withdrawamt += withdrawalNo;

            string sqlText = @"SELECT * FROM Customer
                            WHERE CustomerAccountNumber = @accNum";

            SqlCommand command = new SqlCommand(sqlText, dbConnect);

            command.Parameters.AddWithValue("@accNum", accNum);

            try
            {
                dbConnect.Open();
                SqlDataReader result = command.ExecuteReader();
                if (result.Read())
                {
                    int balance = int.Parse(result.GetValue(7).ToString());
                    accountBalance += balance;
                }
                else
                {
                    dbConnect.Close();
                    return Content("Error");
                }
            }
            catch (Exception ex)
            {
                dbConnect.Close();
                return NotFound("The following error occurred during the write operation 2: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }

            accountBalance -= withdrawamt;

            string sqlTxt = @"UPDATE Customer SET CustomerBalance = @withdraw WHERE CustomerAccountNumber = @accNo";
            SqlCommand cmd = new SqlCommand(sqlTxt, dbConnect); ;

            cmd.Parameters.AddWithValue("@withdraw", accountBalance);
            cmd.Parameters.AddWithValue("@accNo", accNum);

            try
            {
                dbConnect.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    dbConnect.Close();
                    return Ok("Withdraw Successful");
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
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

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
