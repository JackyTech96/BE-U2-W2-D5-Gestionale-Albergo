using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BE_U2_W2_D5_Albergo.Models
{
    public class Utility
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AlbergoDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }

        public static SqlCommand GetCommand(string query, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            return cmd;
        }
    }
}