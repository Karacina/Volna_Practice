using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BD
{
    public class Tools
    {
        static public string GetInfo(string table, string columnName, string id)
        {
            MySqlConnection connection = DBUtils.Connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {table} WHERE {id}";

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string value = reader.GetString(columnName);

            reader.Close();

            return value;
        }
        static public void FillFormInfo(string table, string columnNames, string idColumn, IEnumerable<TextBox> textBoxes)
        {
            string[] parameters = columnNames.Split(' ');
            Array.Reverse(parameters);

            if (textBoxes.Count() != parameters.Count())
            {
                MessageBox.Show("Ошибка в количестве параметром и полей для данных");
                return;
            }
            int id = 0;
            foreach (TextBox box in textBoxes)
            {
                box.Text = GetInfo(table, parameters[id], idColumn);
                id++;
            }
        }
        static public void ShowTable(string tableName, DataGridView dataGrid)
        {
            MySqlConnection connection = DBUtils.Connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            if (!dataGrid.Visible)
            {
                dataGrid.Visible = true;
            }

            MySqlCommand cmd = connection.CreateCommand();
            MySqlDataAdapter adapter = null;
            string request = $"SELECT * FROM {tableName}";
            try
            {
                cmd.CommandText = request;
                adapter = new MySqlDataAdapter(cmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGrid.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static public void DeleteRecord(string table,string contidion)
        {
            MySqlConnection connection = DBUtils.Connection;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"DELETE FROM {table} WHERE {contidion}";

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка удаления");
            }
        }
        static public string GetAllColumnNames(string table)
        {
            string columnNames = "";
            MySqlConnection connection = DBUtils.Connection;
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'";

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            while (reader.Read())
            {
                columnNames+=(reader.GetValue(0));
            }

            reader.Close();
            MessageBox.Show(columnNames);
            return columnNames;
        }
        static public void FillComboBox(ComboBox box,string column,string table)
        {
            box.Items.Clear();
            MySqlConnection connection = DBUtils.Connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT {column} FROM {table}";

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                box.Items.Add(reader.GetValue(0));
            }
            reader.Close();
        }
        static public void FillComboBox(ComboBox box, string column, string table,string condition)
        {
            box.Items.Clear();
            MySqlConnection connection = DBUtils.Connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT {column} FROM {table} WHERE {condition}";

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                box.Items.Add(reader.GetValue(0));
            }
            reader.Close();
        }
        public static void UncheckAllItems(CheckedListBox list)
        {
            foreach (int i in list.CheckedIndices)
            {
                list.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
    }
}