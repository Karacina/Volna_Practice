using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BD
{
    static class DBUtils
    {
        public static MySqlConnection Connection = GetDBConnection();
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "practice";
            string username = "root";
            string password = "0000"; //1982022

            String connString = $"Server={host};Database={database};port={port};User Id={username};password={password}";
            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}