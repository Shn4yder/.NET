using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task_1
{
    public partial class Form1 : Form
    {
        string text;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            text = textBox1.Text;
            Text = text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            text = textBox2.Text;
            Text = text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            text = textBox3.Text;
            Text = text;
        }
    }
}
