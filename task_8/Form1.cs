using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Считываем с формы требуемые значения 
            double Xmin = double.Parse(textBox1.Text);
            double Xmax = double.Parse(textBox2.Text);
            double Step = double.Parse(textBox3.Text);
            double b = double.Parse(textBox4.Text);
            // Количество точек графика 
            int count = (int)Math.Ceiling((Xmax - Xmin) / Step);
            //double y = 9 * (x + 15 * Math.Sqrt(Math.Pow(x, 3) + Math.Pow(b, 3)));
            // Массив значений X – общий для обоих графиков 
            double[] x = new double[count];
            double[] y1 = new double[count];
            double[] y2 = new double[count];
           


            for (int i = 0; i < count; i++)
            {
                x[i] = Xmin + Step * i;
                y1[i] = 100;
                y2[i] = 9 * (x[i] + 15 * Math.Sqrt(Math.Pow(x[i], 3) + Math.Pow(b, 3)));
                

            }

            chart1.ChartAreas[0].AxisX.Minimum = Xmin;
            chart1.ChartAreas[0].AxisX.Maximum = Xmax;

            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Step;
            chart1.Series[0].Points.DataBindXY(x, y1);
            chart1.Series[1].Points.DataBindXY(x, y2);

        }
    }
}
