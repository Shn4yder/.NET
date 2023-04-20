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

namespace task_15
{
    public partial class Autorization : Form
    {
        public Autorization()
        {
            InitializeComponent();

            textBoxLogin.Text = "Введите логин";
            textBoxLogin.ForeColor = Color.LightGray;

            textBoxPassword.PasswordChar = '\0';
            textBoxPassword.Text = "Введите пароль";
            textBoxPassword.ForeColor = Color.LightGray;
        }

        private void Autorization_Load(object sender, EventArgs e)
        {

        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked)
            {
                textBoxPassword.PasswordChar = '\0';
            }
            else
                textBoxPassword.PasswordChar = '*';
        }

        private void labelRegistration_Click(object sender, EventArgs e)
        {
            Registration form = new Registration();
            this.Hide();
            form.Show();
        }

        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "Введите логин")
            {
                textBoxLogin.Text = "";
                textBoxLogin.ForeColor = Color.Black;
            }
        }

        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "")
            {
                textBoxLogin.Text = "Введите логин";
                textBoxLogin.ForeColor = Color.LightGray;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "Введите пароль")
            {
                textBoxPassword.Text = "";
                textBoxPassword.ForeColor = Color.Black;
                textBoxPassword.PasswordChar = '*';
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.PasswordChar = '\0';
                textBoxPassword.Text = "Введите пароль";
                textBoxPassword.ForeColor = Color.LightGray;
            }
        }

        private void SignIn_btn_Click(object sender, EventArgs e)
        {
            string loginUser = textBoxLogin.Text;   
            string passwordUser = textBoxPassword.Text;

            DatabaseManager _databaseManager = new DatabaseManager();
            DataTable _dataTable = new DataTable();
            MySqlDataAdapter _mySqlDataAdapter = new MySqlDataAdapter();
            MySqlCommand _mySqlCommand = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @UserLogin AND `password` = @UserPassword",
                _databaseManager.GetConnection);


            try
            {

                _mySqlCommand.Parameters.Add("@UserLogin", MySqlDbType.VarChar).Value = loginUser;
                _mySqlCommand.Parameters.Add("@UserPassword", MySqlDbType.VarChar).Value = passwordUser;

                _databaseManager.OpenConnection();

                _mySqlDataAdapter.SelectCommand = _mySqlCommand;
                _mySqlDataAdapter.Fill(_dataTable);

                if(_dataTable.Rows.Count > 0)
                {
                    DataForm form = new DataForm();
                    this.Hide();
                    form.Show();

                    User user = new User(); 
                }
                else
                {
                    if (IsLogin)
                        MessageBox.Show("пароль введне неверно", "Внимание");
                    else
                    {
                        if(MessageBox.Show("Пользователь с данным логином не найден\n Зарегистрироваться?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Registration form = new Registration(); 
                            this.Hide();
                            form.Show();
                        }
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Ошибка работы с базой данных", "Ошибка");
            }
            finally
            {
                _databaseManager.CloseConnection();
            }
        }

        public bool IsLogin
        {
            get
            {
                bool been = false;
                string loginUser = textBoxLogin.Text;
                string passwordUser = textBoxPassword.Text;

                DatabaseManager _databaseManager = new DatabaseManager();
                DataTable _dataTable = new DataTable();
                MySqlDataAdapter _mySqlDataAdapter = new MySqlDataAdapter();
                MySqlCommand _mySqlCommand = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @UserLogin", _databaseManager.GetConnection);

                _mySqlCommand.Parameters.Add("@UserLogin", MySqlDbType.VarChar).Value = loginUser;
                _mySqlCommand.Parameters.Add("@UserPassword", MySqlDbType.VarChar).Value = passwordUser;
            

                _mySqlDataAdapter.SelectCommand = _mySqlCommand;
                _mySqlDataAdapter.Fill(_dataTable);

                if (_dataTable.Rows.Count > 0)
                {
                    been = true; ;
                }
                else
                    been = false;

                return been;
            }
        }
    }
}
