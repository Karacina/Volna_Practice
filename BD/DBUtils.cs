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

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
        public static string GetDateFromTable(string table,string condition, string column, int id)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM " + table + " WHERE " + condition + " = " + id.ToString();

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string value = reader.GetString(column);

            reader.Close();

            return value;
        }
        public static List<string> GetItemsList(string table,string column)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM " + table;
            List<string> items = new List<string>();

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(reader.GetString(column));
            }

            reader.Close();
            return items;
        }
    }
}