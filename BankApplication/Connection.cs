using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication
{
    public class Connection
    {
        static SqlConnection con = null;
        public static SqlConnection Connectionstring
        {
            get
            {
                if (con == null)
                {
                    con = new SqlConnection("Data Source=DESKTOP-GB3ICV4;Initial Catalog=BankApp;Integrated Security=True");
                }

                return con;
            }
        }
    }
}
