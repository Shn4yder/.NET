using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace cringe
{
    public partial class MainForm : Form
    {
        int record_index = 1;
        DatabaseManager _database = new DatabaseManager();
        MySqlDataReader reader;

        public MainForm()
        {
            InitializeComponent();
            FillData();
        }

        private void добавитьИзменитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeData form = new ChangeData();
            this.Hide();
            form.Show();
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            if (record_index == 1)
            {
                MessageBox.Show("Это первая запись", "Внимание");
            }
            else
            {
                record_index--;
                FillData();
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            int counter = AbiturientCounter();
            if (counter == record_index)
            {
                MessageBox.Show("Это последняя запись", "Внимание");
            }
            else
            {
                record_index++;
                FillData();
            }
        }

        private void FillData()
        {
            MySqlCommand all_сommand = new MySqlCommand("select * FROM abiturient, attestat where abiturient.id_att = attestat.id_att and id_ab =" + record_index.ToString(),
                _database.GetConnection);
            _database.OpenConnection();
            reader = all_сommand.ExecuteReader();

            while (reader.Read())
            {
                lbl_numberAb.Text = reader.GetString(0);
                tB_FIO.Text = reader.GetString(1);
                tB_prev_school.Text = reader.GetString(2) + ", " + reader.GetString(3) + ", " + reader.GetString(4);
                tB_attSer.Text = reader.GetString(7);
                tB_attNum.Text = reader.GetString(8);
                tB_mark5.Text = reader.GetString(10);
                tB_mark4.Text = reader.GetString(11);
                tB_mark3.Text = reader.GetString(12);
                tB_AVGmark.Text = ((Convert.ToDouble(tB_mark5.Text) * 5 + Convert.ToDouble(tB_mark3.Text) * 3 + Convert.ToDouble(tB_mark4.Text) * 4) / 19).ToString();
            }

            reader.Close();

            MySqlCommand exams_сommand = new MySqlCommand("select exam_name, score from exam, ege where ege.id_exam = exam.id_exam and id_ab =" + record_index.ToString(),
               _database.GetConnection);
            reader = exams_сommand.ExecuteReader();

            List<object> list = new List<object>();

            while (reader.Read())
            {
                list.Add(reader["exam_name"]);
                list.Add(reader["score"]);
            }

            reader.Close();

            tB_subj1.Text = list[0].ToString();
            tB_subj2.Text = list[2].ToString();
            tB_subj3.Text = list[4].ToString();
            tB_subj4.Text = list[6].ToString();
            tB_score1.Text = list[1].ToString();
            tB_score2.Text = list[3].ToString();
            tB_score3.Text = list[5].ToString();
            tB_score4.Text = list[7].ToString();


            MySqlCommand avg_ege_сommand = new MySqlCommand("select sum(score) from ege where id_ab =" + record_index.ToString(),
               _database.GetConnection);
            reader = avg_ege_сommand.ExecuteReader();

            while (reader.Read())
            {
                tB_EGEsum.Text = reader[0].ToString();
            }

            reader.Close();

            MySqlCommand date_att_сommand = new MySqlCommand("SELECT adate FROM `attestat` where id_att =" + record_index.ToString(),
               _database.GetConnection);
            reader = date_att_сommand.ExecuteReader();

            string date = "";

            while (reader.Read())
            {
                date = reader[0].ToString();
            }
            reader.Close();

            string[] date_parts = date.Split(new char[] { '.' });
            int day = Convert.ToInt32(date_parts[0]);
            int month = Convert.ToInt32(date_parts[1]);
            int year = Convert.ToInt32(date_parts[2].Substring(0, 4));

            dT_attGiven.Value = new DateTime(year, month, day);
        }

        private int AbiturientCounter()
        {
            MySqlCommand count_сommand = new MySqlCommand("SELECT count(id_ab) FROM `abiturient`",
                _database.GetConnection);
            _database.OpenConnection();
            MySqlDataReader _reader;
            _reader = count_сommand.ExecuteReader();
            int ab_counter = 0;
            while (_reader.Read())
            {
                ab_counter = Convert.ToInt32(_reader.GetString(0));
            }
            _reader.Close();
            return ab_counter;
        }
    }
}
