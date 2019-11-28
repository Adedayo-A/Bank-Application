using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BankApplication.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Login([FromBody] User incomingUser)
        {
            string username = incomingUser.UserName;
            string password = incomingUser.Password;

            SqlConnection dbConnect = Connection.Connectionstring;
            if (password == "" || username =="")
            {
                return Content("Wrong Details");
            }
            else
            {
                try
                {
                    if (username.StartsWith("a") && username[1] == 'd' && username[2] == 'm')
                    {
                        dbConnect.Open();
                        string sqlText = @"SELECT * FROM Admin
                            WHERE AdminUsername = @username AND AdminPassword = @password";

                        SqlCommand command = new SqlCommand(sqlText, dbConnect);

                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        SqlDataReader result = command.ExecuteReader();
                        if (result.Read())
                        {
                            List<AdminLog> userLog = new List<AdminLog>();
                            AdminLog adminUser = new AdminLog();
                            adminUser.Id = int.Parse(result.GetValue(0).ToString());
                            adminUser.Email = result.GetValue(3).ToString();
                            adminUser.AdminType = result.GetValue(6).ToString();
                            userLog.Add(adminUser);
                            dbConnect.Close();
                            return Ok(userLog);
                            // return Content("Signed in");
                        }
                        else
                        {
                            dbConnect.Close();
                            return Content("Details is Incorrect");
                        }
                    }
                    else
                    {
                        string sqlText = @"SELECT * FROM Customer
                            WHERE CustomerAccountNumber = @AccNo AND CustomerPassword = @Password";
                        SqlCommand command = new SqlCommand(sqlText, dbConnect);
                        command.Parameters.AddWithValue("@AccNo", username);
                        command.Parameters.AddWithValue("@Password", password);
                        SqlDataReader result = command.ExecuteReader();
                        if (result.Read())
                        {
                            CustomerLog.AccType = result.GetValue(8).ToString();
                            CustomerLog.accountNum = result.GetValue(5).ToString();
                            CustomerLog.adminId = int.Parse(result.GetValue(0).ToString());
                            CustomerLog.Balance = int.Parse(result.GetValue(7).ToString());
                            dbConnect.Close();

                            return Content("Signed in");
                        }
                        else
                        {
                            dbConnect.Close();
                            return Content("Details is Incorrect");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Content("Error : Wrong details " + ex.ToString());
                }
            }
        }

        // GET: api/<controller>
    }
}