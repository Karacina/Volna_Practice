using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = DBUtils.Connection;
        private string tableName ="";
        public Form1()
        {
            InitializeComponent();
            connection.Open();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            // Редактирование записи в таблице
            if (e.ColumnIndex == dataGridView1.Columns[0].Index)
            {
                switch (tableName)
                {
                    case "AddTourist":
                        AddTourist tourist = new AddTourist($"idТуриста = {index}");
                        tourist.Show();
                        break;

                    //case 2:
                    //    AddReport report = new AddReport();
                    //    report.Show();
                    //    report.EditDate(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
                    //    break;
                    //case 3:
                    //    AddEmployee employee = new AddEmployee();
                    //    employee.Show();
                    //    employee.EditDate(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
                    //    break;
                }
                return;
            }
            // Удаление записей из таблицы
            if (e.ColumnIndex == dataGridView1.Columns[1].Index)
            {
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    switch (tableName)
                    {
                        case "AddTourist":
                            Tools.DeleteRecord("туристы", $"idТуриста = {index}");
                            break;
                        //case 2:
                        //    DeleteCell("рекламации", "idРекламации", Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
                        //    ShowTable("select * from рекламации");
                        //    break;
                        //case 3:
                        //    DeleteCell("сотрудники", "idСотрудника", Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
                        //    ShowTable("select * from сотрудники");
                        //    break;
                    }
                }
                return;
            }
        }

        private void туристыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.ShowTable("туристы", dataGridView1);
            tableName = "AddTourist";
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.ShowTable("сотрудники", dataGridView1);
            tableName = "AddEmployee";
        }
        #region Экспорт отчетов
        // Сохраняет отчет в Excel
        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!dataGridView1.Visible)
                return;
            Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();

            exApp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet wsh = (Microsoft.Office.Interop.Excel.Worksheet)exApp.ActiveSheet;

            wsh.Cells[1, 1] = "Отчет оконцен";

            for (int i = 2; i < dataGridView1.Columns.Count; i++)
            {
                wsh.Cells[2, i - 1] = dataGridView1.Columns[i].HeaderText;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 2; j < dataGridView1.ColumnCount; j++)
                {
                    wsh.Cells[i + 3, j - 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            exApp.Visible = true;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Calculate();
            //MessageBox.Show(time.Days.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Tools.FillComboBox(comboBox1, "Название", "страны");
        }

        // Обновление при выборе количества звезда
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Tools.FillComboBox(comboBox2, "Название", "отели", $"КодСтраны = {comboBox1.SelectedIndex+1} && КоличествоЗвезд = {trackBar1.Value}");
        }

        // Обновление при выборе страны
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tools.FillComboBox(comboBox2, "Название", "отели", $"КодСтраны = {comboBox1.SelectedIndex+1} && КоличествоЗвезд = {trackBar1.Value}");
        }

        private void Calculate()
        {
            int money = 0;

            int country = Convert.ToInt32(Tools.GetInfo("страны", "СтоимостьПоездки", $"idСтраны = {comboBox1.SelectedIndex+1}"));
            int hotel = Convert.ToInt32(Tools.GetInfo("отели", "СтоимостьЗаНочь", $"idОтеля = {comboBox2.SelectedIndex + 1}"));
            TimeSpan time = dateTimePicker2.Value - dateTimePicker1.Value;
            int people = Convert.ToInt32(textBox1.Text);

            MessageBox.Show(money.ToString($"country - {country}\nhotel - {hotel}\ntime - {time.Days}\npeople - {people}"));
        }
    }
}