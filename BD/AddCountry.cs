using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class AddCountry : Form
    {
        private bool isEdited = false;
        public AddCountry()
        {
            InitializeComponent();

        }
        public AddCountry(string condition, bool edit)
        {
            InitializeComponent();
            isEdited = edit;
            Tools.FillFormInfo("страны", "idСтраны Название СтоимостьПоездки",
                condition, Controls.OfType<TextBox>());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string request = "";
            if (isEdited)
            {
                request = "UPDATE страны SET " +
                    "idСтраны = @param1," +
                    "Название = @param2," +
                    "СтоимостьПоездки = @param3 " +
                    "WHERE idСтраны = @param1";
            }
            else
            {
                request = "INSERT INTO страны" +
                    "(idСтраны, Название, СтоимостьПоездки)" +
                    "VALUES (@param1,@param2,@param3)";
            }
            try
            {
                MySqlConnection connection = DBUtils.Connection;
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = request;

                cmd.Parameters.AddWithValue("@param1", textBox1.Text);
                cmd.Parameters.AddWithValue("@param2", textBox2.Text);
                cmd.Parameters.AddWithValue("@param3", textBox3.Text);

                cmd.ExecuteNonQuery();
                if (isEdited)
                {
                    MessageBox.Show("Запись обновлена");
                    isEdited = false;
                    Close();
                }
                else
                {
                    MessageBox.Show("Запись добавлена");
                    Close();
                    //ClearAllFields();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("Дубликат ID Рекламации");
                }
                MessageBox.Show(ex.Message);
            }
        }
    }
}
