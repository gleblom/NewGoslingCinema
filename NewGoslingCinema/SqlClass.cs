using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Collections.Generic;

namespace NewGoslingCinema
{
    class SqlClass
    {
        static string str = "Data Source=DESKTOP-1U9FDH3;Initial Catalog=NewCinema;Integrated Security=True";
        // Data Source=DESKTOP-1U9FDH3;Initial Catalog=NewCinema;Integrated Security=True
        // Data Source=510-013;Initial Catalog=NewCinema;Integrated Security=True;Encrypt=False

        static SqlConnection con;
        static SqlCommand com;
        static SqlDataReader reader;


        static SqlConnection ConnectTo(SqlConnection cnn)
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

        }
        public static async Task<List<DateTime>> Sessione()
        {
            con = ConnectTo(con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            using (con)
            {
                await con.OpenAsync();
                com = new SqlCommand("GetDateTime", con);
                com.CommandType = CommandType.StoredProcedure;
                reader = await com.ExecuteReaderAsync();
                List<DateTime> dateTimes = new List<DateTime>();
                if (reader.HasRows)
                {
                    while(await reader.ReadAsync())
                    {
                        TimeSpan time = reader.GetTimeSpan(0);
                        DateTime date = reader.GetDateTime(1);
                        DateTime result = date + time;
                        dateTimes.Add(result);
                    }
                }
                await reader.CloseAsync();
                return dateTimes;
            }

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
                        t = SessionParser.TimeParse(t);
                        string d = date.ToShortDateString();
                        list.Items.Add("Дата: " + d + " Время: " + t);
                    }
                }
                await reader.CloseAsync();

            }
        }
        public static async void Tickets(string userName, string date, string time, string film)
        {
            con = ConnectTo(con);
            using (con)
            {
                await con.OpenAsync();
                com = new SqlCommand("Ticket", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@User", userName);
                com.Parameters.AddWithValue("@Date", date);
                com.Parameters.AddWithValue("@Time", time);
                com.Parameters.AddWithValue("@Film", film);
                await com.ExecuteNonQueryAsync();
            }
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
                        string d = reader.GetString(0);
                        string t = reader.GetString(1);
                        string f = reader.GetString(2);
                        list.Items.Add("Фильм: " + f + ", " + "Дата: " + d + " Время: " + t );
                    }
                }
                await reader.CloseAsync();
            }
        }
        public static async Task<int> Registration(string login, string password)
        {
            con = ConnectTo(con);
            using (con)
            {
                await con.OpenAsync();
                com = new SqlCommand("Registration", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Login", login);
                com.Parameters.AddWithValue("@Password", password);
                reader = await com.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        return 1;
                    }
                }
                await reader.CloseAsync();
                await com.ExecuteNonQueryAsync();
            }
            return 0;
        }
        public static async void DeleteDate(string date, string time)
        {
            con = ConnectTo(con);
            using (con)
            {
                await con.OpenAsync();
                com = new SqlCommand("DeleteDate", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Date", date);
                com.Parameters.AddWithValue("@Time", time);
                com.ExecuteNonQuery();
            }

        }

    }
}
