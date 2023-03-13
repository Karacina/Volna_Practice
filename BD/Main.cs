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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Tools.FillComboBox(comboBox2, "Название", "страны");
        }
        #region Вкладки
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[2];
        }
        #endregion

        // Отображение таблицы
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tools.ShowTable(comboBox1.Text, dataGridView3);
        }

        // Добавление записей в таблицу
        private void button5_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "отели":
                    AddHotel form = new AddHotel();
                    form.Show();
                    break;

                case "туристы":
                    AddTourist form1 = new AddTourist();
                    form1.Show();
                    break;
                case "страны":
                    AddCountry form2 = new AddCountry();
                    form2.Show();
                    break;
                default:
                    MessageBox.Show("Выберите таблицу");
                    break;
            }
        }

        // Очистка
        private void button4_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "Выберите страну";
            foreach (int box in checkedListBox1.CheckedIndices)
                checkedListBox1.SetItemChecked(box, false);

            textBox1.ResetText();
            textBox2.ResetText();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Tools.ShowTable("отели", "Название,СтоимостьЗаНочь,КоличествоЗвезд", $"КодСтраны = {comboBox2.SelectedIndex+1} AND КоличествоЗвезд in ({Tools.GetAllCheckedItems(checkedListBox1)})", dataGridView2);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[2].Value);
            // Редактирование записи в таблице
            if (e.ColumnIndex == dataGridView3.Columns[0].Index)
            {
                switch (comboBox1.Text)
                {
                    case "туристы":
                        AddTourist tourist = new AddTourist($"idТуриста = {index}", true);
                        tourist.Show();
                        break;
                    case "отели":
                        AddHotel hotel = new AddHotel($"idОтеля = {index}", true);
                        hotel.Show();
                        break;
                    case "страны":
                        AddCountry country = new AddCountry($"idСтраны = {index}", true);
                        country.Show();
                        break;
                }
                return;
            }
            // Удаление записей из таблицы
            if (e.ColumnIndex == dataGridView3.Columns[1].Index)
            {
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    switch (comboBox1.Text)
                    {
                        case "туристы":
                            Tools.DeleteRecord("туристы", $"idТуриста = {index}");
                            break;
                        case "отели":
                            Tools.DeleteRecord("отели", $"idОтеля = {index}");
                            break;
                        case "страны":
                            Tools.DeleteRecord("страны", $"idСтраны = {index}");
                            break;
                    }
                }
                return;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns[0].Index)
            {
                Calculate(Convert.ToInt32(dataGridView2[2,e.RowIndex].Value));
            }
        }
        private void Calculate(int price)
        {
            int people = Convert.ToInt32(textBox1.Text);
            int nights = Convert.ToInt32(textBox2.Text);

            int t = (people * nights * price);
            label6.Text = $"Стоймость тура: {t} рублей";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tools.ShowTable("отели", "Название,СтоимостьЗаНочь", $"КодСтраны = {comboBox2.SelectedIndex + 1}"+Tools.GetAllCheckedItems(checkedListBox1), dataGridView2);
        }
    }
}
