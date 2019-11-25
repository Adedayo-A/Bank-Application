using System;


namespace Connection
{
    public class ConnectionDb
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
