using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox1.Text);
            double b = Convert.ToDouble(textBox2.Text);
            double result;
            double switcher = x * b;

            if (switcher > 1 && switcher < 10)
            {
                result = Math.Pow(Math.E, x*x);
            }
            else if (switcher > 12 && switcher < 40)
            {
                result = Math.Sqrt(Math.Abs(x * x + 4 * b));
            }
            else
            {
                result = b * Math.Pow(x, 4);
            }

            textBox3.Text = result.ToString();

        }

    }
}
