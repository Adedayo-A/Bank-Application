using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication
{
    public class UserLoginDetails
    {
        public static List<User> userLog = new List<User>();

        public static List<User> AddUser(User userIn)
        {
            userLog.Add(userIn);
            return userLog;
        }
    }
}
