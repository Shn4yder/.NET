using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task_6
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
        int[] Mas = new int[30];

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Random rand = new Random();
            for (int i = 0; i < 30; i++)
            { 
                Mas[i] = rand.Next( -100, 100);
                listBox1.Items.Add("Mas[" + i.ToString() + "] = " + Mas[i].ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int counter = 0;
            int s = 0;
            listBox2.Items.Clear();
            listBox2.Items.Add("Элементы:"); 
            for (int i = 0; i < 30; i++)
            {
                if (Mas[i] % 5 == 0 && Mas[i] % 7 != 0)
                {
                    listBox2.Items.Add("Mas[" + Convert.ToString(i) + "] = " + Mas[i].ToString());
                    s += Mas[i];
                    counter++;
                }
            }
            listBox2.Items.Add($"Количество элементов: {counter.ToString()}\n");
            listBox2.Items.Add($"Сумма элементов: {s.ToString()}");

        }
    }
}

 

