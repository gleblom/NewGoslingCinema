﻿using System;
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
        static string str = "Data Source=DESKTOP-1U9FDH3;Initial Catalog=NewCinema;Integrated Security=True";
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
                com.ExecuteNonQuery();

            }
            con = null;
            com = null;

        }
        public static async Task<DataTable> Sessione()
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
                return table;
            }
        }

    }
}
