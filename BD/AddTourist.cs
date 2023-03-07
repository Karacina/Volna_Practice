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
        public AddTourist(string condition)
        {
            InitializeComponent();
            Tools.FillFormInfo("туристы", "idТуриста Фамилия Имя Отчество Телефон ПаспортСерия ПаспортНомер",
                condition, Controls.OfType<TextBox>());
        }

        private void AddTourist_Load(object sender, EventArgs e)
        {
        }
    }
}
