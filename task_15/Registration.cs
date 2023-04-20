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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();

            textBoxName.Text = "Введите имя";
            textBoxName.ForeColor = Color.LightGray;

            textBoxSurname.Text = "Введите фамилию";
            textBoxSurname.ForeColor = Color.LightGray;

            textBoxLogin.Text = "Введите логин";
            textBoxLogin.ForeColor = Color.LightGray;

            textBoxPassword.PasswordChar = '\0';
            textBoxPassword.Text = "Введите пароль";
            textBoxPassword.ForeColor = Color.LightGray;
            
        }

        private void Regestration_Load(object sender, EventArgs e)
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

        private void labelEnter_Click(object sender, EventArgs e)
        {
            Autorization form = new Autorization();
            this.Hide();
            form.Show();
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
            {
                textBoxName.Text = "Введите имя";
                textBoxName.ForeColor = Color.LightGray;
            }
        }

        private void textBoxName_Enter(object sender, EventArgs e)
        {
            if (textBoxName.Text == "Введите имя")
            {
                textBoxName.Text = "";
                textBoxName.ForeColor = Color.Black;
            }
        }

        private void textBoxSurname_Leave(object sender, EventArgs e)
        {
            if (textBoxSurname.Text == "")
            {
                textBoxSurname.Text = "Введите фамилию";
                textBoxSurname.ForeColor = Color.LightGray;
            }
        }

        private void textBoxSurname_Enter(object sender, EventArgs e)
        {
            if (textBoxSurname.Text == "Введите фамилию")
            {
                textBoxSurname.Text = "";
                textBoxSurname.ForeColor = Color.Black;
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

        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "Введите логин")
            {
                textBoxLogin.Text = "";
                textBoxLogin.ForeColor = Color.Black;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Text = "Введите пароль";
                textBoxPassword.ForeColor = Color.LightGray;
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

        private void Registration_btn_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "Введите имя" || textBoxSurname.Text == "Введите фамилию" || textBoxLogin.Text == "Введите логин" 
                || textBoxPassword.Text == "Введите пароль")
            {
                MessageBox.Show("Заполнены не все поля");
                return;
            }

            if (!IsUser) 
            {
                DatabaseManager _databaseManager = new DatabaseManager();
                MySqlCommand _mySqlCommand = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `name`, `surname`)" +
                    "VALUES (@login, @password, @name, @surname)",
                    _databaseManager.GetConnection);
                try 
                {
                    _mySqlCommand.Parameters.Add("@login", MySqlDbType.VarChar).Value = textBoxLogin.Text;
                    _mySqlCommand.Parameters.Add("@password", MySqlDbType.VarChar).Value = textBoxPassword.Text;
                    _mySqlCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBoxName.Text;
                    _mySqlCommand.Parameters.Add("@surname", MySqlDbType.VarChar).Value = textBoxSurname.Text;

                    _databaseManager.OpenConnection();

                    if (_mySqlCommand.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Аккаунт создан", "Внимание");
                        DataForm form = new DataForm();
                        this.Hide();
                        form.Show();

                        User user = new User(textBoxLogin.Text);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка создания аккаунта", "Ошибка");
                    }
                } 
                catch 
                {
                    MessageBox.Show("Ошибка работы с базойт данных", "Ошибка");
                }
                finally { _databaseManager.CloseConnection(); }
            }
        }

        private bool IsUser 
        {
            get 
            {
                bool been = false;
                string nameUser = textBoxName.Text;
                string loginUser = textBoxLogin.Text;   
                string surnameUser = textBoxSurname.Text;

                DatabaseManager _databaseManager = new DatabaseManager();
                DataTable _dataTable= new DataTable();
                MySqlDataAdapter _mySqlDataAdapter = new MySqlDataAdapter();
                MySqlCommand _mySqlCommand = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @UserLogin AND `surname` = @UserSurname",
                    _databaseManager.GetConnection);

                _mySqlCommand.Parameters.Add("@UserLogin", MySqlDbType.VarChar).Value = loginUser;
                _mySqlCommand.Parameters.Add("@UserName", MySqlDbType.VarChar).Value = nameUser;
                _mySqlCommand.Parameters.Add("@UserSurname", MySqlDbType.VarChar).Value = surnameUser;

                _mySqlDataAdapter.SelectCommand= _mySqlCommand;
                _mySqlDataAdapter.Fill(_dataTable);

                if (_dataTable.Rows.Count > 0 )
                {
                    been = true; ;
                    if (MessageBox.Show("Такой пользователь уже есть\nПерейти на вкладку входа?","Внимание",  MessageBoxButtons.YesNo) == DialogResult.Yes) 
                    {
                        Autorization form = new Autorization();
                        this.Hide();
                        form.Show();
                    }
                }
                else 
                    been= false;

                return been;
            }
        }

        private bool IsLogin
        {
            get 
            {
                bool been = false;
                string passwordUser = textBoxPassword.Text;
                string loginUser = textBoxLogin.Text;

                DatabaseManager _databaseManager = new DatabaseManager();
                DataTable _dataTable = new DataTable();
                MySqlDataAdapter _mySqlDataAdapter = new MySqlDataAdapter();
                MySqlCommand _mySqlCommand = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @UserLogin AND `password` = @UserPassword",
                    _databaseManager.GetConnection);

                _mySqlCommand.Parameters.Add("@UserLogin", MySqlDbType.VarChar).Value = loginUser;
                _mySqlCommand.Parameters.Add("@UserPassword", MySqlDbType.VarChar).Value = passwordUser;

                _mySqlDataAdapter.SelectCommand = _mySqlCommand;
                _mySqlDataAdapter.Fill(_dataTable);

                if (_dataTable.Rows.Count > 0)
                {
                    been = true; ;
                    MessageBox.Show("Такой логин уже есть\nПопробуте другой логин", "Внимание");
                }
                else
                    been = false;

                return been;
            }
        }
    }
}
