using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NewGoslingCinema
{
    class SqlClass
    {
        public static string str = "";
        public static SqlConnection con;

        public static SqlConnection ConnectTo()
        {
            con = new SqlConnection(str);
            return con;
        }

    }
}
