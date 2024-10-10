using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace NewGoslingCinema
{
    class SqlClass
    {
        static string str = "Data Source=510-013;Initial Catalog=Cinema;Integrated Security=True;Encrypt=True; TrustServerCertificate=True";
        static SqlConnection con;
        static SqlCommand com;
        static SqlDataReader reader;

        public static SqlConnection ConnectTo(SqlConnection cnn)
        {
            cnn = new SqlConnection(str);
            return cnn;
        }
        public static async Task<int> Auth(string login, string password)
        {
            con = ConnectTo(con);
            using (con)
            {
                await con.OpenAsync();
                com = new SqlCommand("Auth", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Login", login);
                com.Parameters.AddWithValue("@Password", password);
                reader = await com.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if(await reader.ReadAsync())
                    {
                        con = null;
                        return reader.GetInt32(0);
                    }
                }
                await reader.CloseAsync();
            }
            con = null;
            return -2;
        }

    }
}
