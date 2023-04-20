using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task_15
{
    public partial class DialogUpload : Form
    {
        List<string> _data = new List<string>();
        public DialogUpload()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (textBox_Id.Text != "" && textBox_Serv.Text != "" && textBox_Cost.Text != ""
                && textBox_Sale.Text != "" && textBox_FIO.Text != "")
                btn_Add.DialogResult = DialogResult.OK;
            else
                MessageBox.Show("Введены не все поля");
        }

        public List<string> Data
        {
            get
            {
                _data.Add(Convert.ToString(textBox_Id.Text));
                _data.Add(Convert.ToString(textBox_Serv.Text));
                _data.Add(Convert.ToString(textBox_Cost.Text));
                _data.Add(Convert.ToString(textBox_Sale.Text));
                _data.Add(Convert.ToString(textBox_FIO.Text));
                _data.Add(Convert.ToString(textBox_Phone.Text));
                return _data;
            }
        }
    }
}
