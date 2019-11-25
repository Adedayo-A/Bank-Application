using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication
{
    public class CustomerLog
    {
        public static int adminId { get; set; }
        public static string accountNum { get; set; }
        public static Random rand = new Random();
        public static string firstName { get; set; }
        public static string lastName { get; set; }
        public static string email { get; set; }
        public static string password { get; set; }
        public static int Balance { get; set; }
        public static string AdminType { get; set; }
        public static string AccType { get; set; }
    }
}
