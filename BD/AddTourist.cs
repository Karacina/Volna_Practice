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
    public partial class AddTourist : Form
    {
        private bool isEdited=false;
        public AddTourist()
        {
            InitializeComponent();
            
        }
        public AddTourist(string condition, bool edit)
        {
            InitializeComponent();
            isEdited = edit;
            Tools.FillFormInfo("туристы", "idТуриста Фамилия Имя Отчество Телефон ПаспортСерия ПаспортНомер",
                condition, Controls.OfType<TextBox>());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string request = "";
            if (isEdited)
            {
                request = "UPDATE туристы SET " +
                    "idТуриста = @param1," +
                    "Фамилия = @param2," +
                    "Имя = @param3," +
                    "Отчество = @param4," +
                    "Телефон = @param5," +
                    "ПаспортСерия = @param6," +
                    "ПаспортНомер = @param7 " +
                    "WHERE idТуриста = @param1";
            }
            else
            {
                request = "INSERT INTO туристы" +
                    "(idТуриста, Фамилия, Имя, Отчество, Телефон, ПаспортСерия, ПаспортНомер)" +
                    "VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7)";
            }
            try
            {
                MySqlConnection connection = DBUtils.Connection;
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = request;

                cmd.Parameters.AddWithValue("@param1", textBox1.Text);
                cmd.Parameters.AddWithValue("@param2", textBox2.Text);
                cmd.Parameters.AddWithValue("@param3", textBox3.Text);
                cmd.Parameters.AddWithValue("@param4", textBox4.Text);
                cmd.Parameters.AddWithValue("@param5", textBox5.Text);
                cmd.Parameters.AddWithValue("@param6", textBox6.Text);
                cmd.Parameters.AddWithValue("@param7", textBox7.Text);

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
