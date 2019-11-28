using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication
{
    public class NewCustomerDetails
    {
        private int balance = 0;
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }

        public int Balance { get { return balance; } }
    }
}
