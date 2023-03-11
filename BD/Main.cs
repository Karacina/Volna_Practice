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
            //Tools.FillComboBox(comboBox1, "Название", "страны");
        }

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

        //Добавление записи в таблицу. Открывает форму добавления записи
        private void button5_Click(object sender, EventArgs e)
        {
            // TODO Открытие формы добавления
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "Выберите страну";
            textBox1.ResetText();
            textBox2.ResetText();
            Tools.UncheckAllItems(checkedListBox1);
        }
    }
}
