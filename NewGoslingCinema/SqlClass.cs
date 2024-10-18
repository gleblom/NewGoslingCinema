using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;

namespace NewGoslingCinema
{
    class SqlClass
    {
        static string str = "Data Source=510-013;Initial Catalog=NewCinema;Integrated Security=True;Encrypt=False";
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
            com = null;
            return -2;
        }
        public static async void AddSession(string film, string time, string date)
        {
            con = ConnectTo(con);
            using (con)
            {
                await con.OpenAsync();
                com = new SqlCommand("AddSession", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Film", film);
                com.Parameters.AddWithValue("@Time", time);
                com.Parameters.AddWithValue("@Date", date);
                await com.ExecuteNonQueryAsync();

            }
            con = null;
            com = null;
            

        }
        public static async void Sessione(DataGrid grid)
        {
            con = ConnectTo(con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            using (con)
            {
                await con.OpenAsync();
                com = new SqlCommand("Sessione", con);
                com.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand = com;
                adapter.Fill(table);
                grid.DataContext = table;
            }
            con = null;
            com = null;
        }
        public static async void Delete(object id)
        {
            con = ConnectTo(con);
            using (con) 
            {
                await con.OpenAsync();
                com = new SqlCommand("DeleteSession", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", id);
                await com.ExecuteNonQueryAsync();
            }
            con = null;
            com = null;
        }
        public static async void ShowSessions(string film, ListBox list)
        {
            con = ConnectTo(con);
            using (con) 
            {
                await con.OpenAsync();
                com = new SqlCommand("FilmPage", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Film", film);
                reader = await com.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync()) 
                    {
                        TimeSpan timeValue = reader.GetTimeSpan(0);
                        DateTime date = reader.GetDateTime(1);
                        string t = timeValue.ToString();
                        string d = date.ToShortDateString();
                        list.Items.Add(d + t);
                    }
                }
                await reader.CloseAsync();
                con = null;
                com = null;
            }
        }
        public static async void Tickets(string session, string userName)
        {
            con = ConnectTo(con);
            using (con)
            {
                await con.OpenAsync();
                com = new SqlCommand("Ticket", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Session", session);
                com.Parameters.AddWithValue("User", userName);
                await com.ExecuteNonQueryAsync();
            }
            con = null;
            com = null;
        }
        public static async void SelectTickets(string name, ListBox list)
        {
            con = ConnectTo(con);
            using (con)
            {
                await con.OpenAsync();
                com = new SqlCommand("SelectTickets", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@User", name);
                reader = await com.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        list.Items.Add(reader.GetString(0));
                    }
                }
            }
            await reader.CloseAsync();
            con = null;
            com = null;
        }

    }
}
