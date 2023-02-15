using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task_5
{
    public partial class Form1 : Form
    {
        int countButton = 0;
        int countLabel = 0;
        int countTextBox = 0;
        Button b;
        Button b_label;
        Label lbl;
        TextBox tb;
        int top = 40;
        int left = 225;

        public Form1()
        {
            InitializeComponent();

            b = new Button();
            b.Top = 15;
            b.Left = 15;
            b.Width = 150;
            b.Text = "Добавить кнопку";
            b.Click += b_Click;
            Controls.Add(b);

            b_label = new Button();
            b_label.Top = 15;
            b_label.Left = 170;
            b_label.Width = 150;
            b_label.Text = "Добавить метку";
            b_label.Click += lbl_Click;
            Controls.Add(b_label);

            tb = new TextBox();
            tb.Top = 17;
            tb.Left = 325;
            tb.Width = 90;
            tb.Text = "Добавить поле";
            tb.Click += tb_Click;
            Controls.Add(tb);
        }

        private void b_Click(object sender, EventArgs e)
        {
            int top = b.Bottom;
            int left = b.Left;
            b = new Button();
            b.Top = top;
            b.Left = left;
            b.Text = "Button" + (++countButton).ToString();
            b.Click += b_Click;
            Controls.Add(b);
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            lbl = new Label();
            lbl.Top = top;
            lbl.Left = left;
            lbl.Text = "Label" + (++countLabel).ToString();
            b_label.Click += lbl_Click;
            Controls.Add(lbl);
            top += 25;
        }

        private void tb_Click(object sender, EventArgs e)
        {
            int top = tb.Bottom;
            int left = tb.Left;
            tb = new TextBox();
            tb.Top = top;
            tb.Left = left;
            tb.Text = "Textbox" + (++countTextBox).ToString();
            tb.Click += tb_Click;
            Controls.Add(tb);
        }
    }
}
