using MySql.Data.MySqlClient;
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

namespace cringe
{
    public partial class ChangeData : Form
    {
        DatabaseManager _manager = new DatabaseManager();
        int counter;

        public ChangeData()
        {
            InitializeComponent();
        }

        private void просмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm form = new MainForm();
            this.Hide();
            form.Show();
            
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            counter = Convert.ToInt32(textBox1.Text);
            AddAttestat();
            AddAbiturient();
            AddEge(GetFirstExamId(), Convert.ToInt32(tB_score1.Text));
            AddEge(GetSecondExamId(), Convert.ToInt32(tB_score2.Text));
            AddEge(GetThirdExamId(), Convert.ToInt32(tB_score3.Text));
            AddEge(GetFourthExamId(), Convert.ToInt32(tB_score4.Text));
            counter++;
            textBox1.Text = counter.ToString();
        }

        

        private void AddAttestat()
        {
            try
            {
                bool add = true;
                _manager.OpenConnection();

                if (true)
                {
                    string att_commandString = "INSERT INTO attestat (id_att, number, seria, kolvo5, kolvo4, kolvo3)" +
                       " VALUES (@id_att, @number, @seria, @kolvo5, @kolvo4, @kolvo3)";
                    int counter = Convert.ToInt32(textBox1.Text) + 1;


                    MySqlCommand att_command = new MySqlCommand(att_commandString, _manager.GetConnection);



                    /////////////////////////////////////////////////////////////////////////////////////////
                    

                    
                    att_command.Parameters.Add("@id_att", MySqlDbType.Int32).Value = 60;
                    att_command.Parameters.Add("@number", MySqlDbType.VarChar).Value = tB_attNum.Text;
                    att_command.Parameters.Add("@seria", MySqlDbType.VarChar).Value = tB_attSer.Text;
                    att_command.Parameters.Add("@kolvo5", MySqlDbType.Int32).Value = Convert.ToUInt32(tB_mark5.Text);
                    att_command.Parameters.Add("@kolvo4", MySqlDbType.Int32).Value = Convert.ToUInt32(tB_mark4.Text);
                    att_command.Parameters.Add("@kolvo3", MySqlDbType.Int32).Value = Convert.ToUInt32(tB_mark3.Text);


                    if (att_command.ExecuteNonQuery() != 1)
                        add = false;
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены", "Внимание");
                    add = false;
                }


                if (!add)
                    MessageBox.Show("Ошибка добавления данных", "Внимание");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { _manager.CloseConnection(); }
        }

        private void AddAbiturient()
        {
            try
            {
                bool add = true;
                _manager.OpenConnection();

                if (true)
                {

                    string ab_commandString = "INSERT INTO abiturient (fio, prev_school, city, years, id_att)" +
             "VALUES (@FIO, @prev_school, @city, @years, @id_att)";
                    MySqlCommand ab_command = new MySqlCommand(ab_commandString, _manager.GetConnection);



                    /////////////////////////////////////////////////////////////////////////////////////////


                    ab_command.Parameters.Add("@id_att", MySqlDbType.Int32).Value = 60;
                    ab_command.Parameters.Add("@FIO", MySqlDbType.VarChar).Value = tB_FIO.Text;
                    ab_command.Parameters.Add("@prev_school", MySqlDbType.VarChar).Value = tB_prev_school.Text;
                    ab_command.Parameters.Add("@city", MySqlDbType.VarChar).Value = tB_city.Text;
                    ab_command.Parameters.Add("@years", MySqlDbType.Int32).Value = Convert.ToUInt32(tB_year.Text);

                    if (ab_command.ExecuteNonQuery() != 1)
                        add = false;
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены", "Внимание");
                    add = false;
                }


                if (add)
                    MessageBox.Show("Данные добавлены", "Внимание");
                else
                    MessageBox.Show("Ошибка добавления данных", "Внимание");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { _manager.CloseConnection(); }
        }


       private int GetFirstExamId()
       {
            int first_exam_id = 0;

            MySqlDataReader reader;
            _manager.OpenConnection();
            MySqlCommand avg_ege_сommand = new MySqlCommand("select id_exam from exam where exam_name =" + '"' + cB_sub1.Text + '"',
                _manager.GetConnection);
            reader = avg_ege_сommand.ExecuteReader();


            while (reader.Read())
            {
                first_exam_id = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();
            return first_exam_id;
       }

        private int GetSecondExamId()
        {
            int second_exam_id = 0;

            MySqlDataReader reader;
            _manager.OpenConnection();
            MySqlCommand avg_ege_сommand = new MySqlCommand("select id_exam from exam where exam_name =" + '"' + cB_sub2.Text + '"',
                _manager.GetConnection);
            reader = avg_ege_сommand.ExecuteReader();


            while (reader.Read())
            {
                second_exam_id = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();
            return second_exam_id;
        }

        private int GetThirdExamId()
        {
            int third_exam_id = 0;

            MySqlDataReader reader;
            _manager.OpenConnection();
            MySqlCommand avg_ege_сommand = new MySqlCommand("select id_exam from exam where exam_name =" + '"' + cB_sub3.Text + '"',
                _manager.GetConnection);
            reader = avg_ege_сommand.ExecuteReader();


            while (reader.Read())
            {
                third_exam_id = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();
            return third_exam_id;
        }

        private int GetFourthExamId()
        {
            int fourth_exam_id = 0;

            MySqlDataReader reader;
            _manager.OpenConnection();
            MySqlCommand avg_ege_сommand = new MySqlCommand("select id_exam from exam where exam_name =" + '"' + cB_sub4.Text + '"',
                _manager.GetConnection);
            reader = avg_ege_сommand.ExecuteReader();


            while (reader.Read())
            {
                fourth_exam_id = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();
            return fourth_exam_id;
        }

        private void AddEge(int SubjectId, int SubjectScore)
        {
            try
            {
                bool add = true;
                _manager.OpenConnection();

                if (true)
                {
                    string ab_commandString = "INSERT INTO ege (id_exam, id_ab, score)" +
             "VALUES (@id_exam, @id_ab, @score)";
                    MySqlCommand ege_command = new MySqlCommand(ab_commandString, _manager.GetConnection);

                    ege_command.Parameters.Add("@id_exam", MySqlDbType.Int32).Value = SubjectId;
                    ege_command.Parameters.Add("@id_ab", MySqlDbType.Int32).Value = 60;
                    ege_command.Parameters.Add("@score", MySqlDbType.Int32).Value = SubjectScore;


                    if (ege_command.ExecuteNonQuery() != 1)
                        add = false;
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены", "Внимание");
                    add = false;
                }


                if (add)
                    MessageBox.Show("Данные добавлены", "Внимание");
                else
                    MessageBox.Show("Ошибка добавления данных", "Внимание");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { _manager.CloseConnection(); }
        }
    }   
}
