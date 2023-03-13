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
    public partial class AddHotel : Form
    {
        private bool isEdited = false;
        public AddHotel()
        {
            InitializeComponent();

        }
        public AddHotel(string condition, bool edit)
        {
            InitializeComponent();
            isEdited = edit;
            Tools.FillFormInfo("отели", "idОтеля КодСтраны Название КоличествоЗвезд СтоимостьЗаНочь КоличествоКомнат КоличествоМест",
                condition, Controls.OfType<TextBox>());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string request = "";
            if (isEdited)
            {
                request = "UPDATE отели SET " +
                    "idОтеля = @param1," +
                    "КодСтраны = @param2," +
                    "Название = @param3," +
                    "КоличествоЗвезд = @param4," +
                    "СтоимостьЗаНочь = @param5," +
                    "КоличествоКомнат = @param6," +
                    "КоличествоМест = @param7 " +
                    "WHERE idОтеля = @param1";
            }
            else
            {
                request = "INSERT INTO отели" +
                    "(idОтеля, КодСтраны, Название, КоличествоЗвезд, СтоимостьЗаНочь, КоличествоКомнат, КоличествоМест)" +
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
