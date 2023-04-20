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
    public partial class DataForm : Form
    {
        MySqlDataReader _reader;
        User user = new User();
        public DataForm()
        {
            InitializeComponent();
        }

        private void DataForm_Load(object sender, EventArgs e)
        {

        }

        private void DataForm_Shown(object sender, EventArgs e)
        {
            
            HeaderOfTheTable();
            List<RowOfData> _data = new List<RowOfData>();

            DatabaseManager _manager = new DatabaseManager();
            MySqlCommand _command = new MySqlCommand("SELECT * FROM `orders`", _manager.GetConnection);
        

            try
            {
                _manager.OpenConnection();
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    RowOfData row = new RowOfData(_reader["id_ord"], _reader["service"], _reader["cost"], _reader["sale"], _reader["phone"], _reader["FIO"]);
                    _data.Add(row);
                }

                for (int i = 0; i < _data.Count; i++)
                {
                    AddDataGrid(_data[i]);
                }
            }
            catch 
            {
                MessageBox.Show("Ошибка работы с базой данных");
            }
            finally { _manager.CloseConnection(); }
        }

        private void AddDataGrid(RowOfData row)
        {
            dataGrid.Rows.Add(row.id_ord, row.service, row.cost, row.sale, row.phone, row.FIO);
        }

        private void HeaderOfTheTable()
        {
            var column1 = new DataGridViewColumn();
            column1.HeaderText= "Номер заказа";
            column1.Width= 75;
            column1.Name = "id_ord";
            column1.Frozen= true;
            column1.CellTemplate = new DataGridViewTextBoxCell();

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Телефон заказчика";
            column2.Width = 100;
            column2.Name = "phone";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Cкидка";
            column3.Width = 75;
            column3.Name = "sale";
            column3.CellTemplate = new DataGridViewTextBoxCell();

            var column4 = new DataGridViewColumn();
            column4.HeaderText = "Стоимость";
            column4.Width = 100;
            column4.Name = "cost";
            column4.CellTemplate = new DataGridViewTextBoxCell();

            var column5 = new DataGridViewColumn();
            column5.HeaderText = "ФИО";
            column5.Width = 200;
            column5.Name = "FIO";
            column5.CellTemplate = new DataGridViewTextBoxCell();

            var column6 = new DataGridViewColumn();
            column6.HeaderText = "Услуга";
            column6.Width = 200;
            column6.Name = "service";
            column6.CellTemplate = new DataGridViewTextBoxCell();


            dataGrid.Columns.Add(column1);
            dataGrid.Columns.Add(column2);
            dataGrid.Columns.Add(column3);
            dataGrid.Columns.Add(column4);
            dataGrid.Columns.Add(column5);
            dataGrid.Columns.Add(column6);

            dataGrid.AllowUserToAddRows= false;
            dataGrid.ReadOnly= true;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }

        private void выходИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void выходВОкноРегистрацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registration form = new Registration();
            this.Hide();
            form.Show();
        }

        private void выходВОкноВходаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Autorization form = new Autorization();
            this.Hide();
            form.Show();
        }

        private void редактироватьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DataUploadForm form = new DataUploadForm();
            this.Hide();
            form.Show();
        }

        private void обновитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<RowOfData> _data = new List<RowOfData>();


            DatabaseManager _database = new DatabaseManager();
            MySqlCommand _сommand = new MySqlCommand("SELECT * FROM `orders`",
                _database.GetConnection);
            MySqlDataReader _reader;


            _database.OpenConnection();
            _reader = _сommand.ExecuteReader();


            dataGrid.DataSource= null;
            dataGrid.Rows.Clear();


            try
            {

                while (_reader.Read())
                {
                    RowOfData row = new RowOfData(_reader["id_ord"], _reader["service"], _reader["cost"], _reader["sale"], _reader["phone"], _reader["FIO"]);
                    _data.Add(row);
                }

                for (int i = 0; i < _data.Count; i++)
                {
                    AddDataGrid(_data[i]);
                }
                MessageBox.Show("Данные обновлены");
            }
            catch
            {
                MessageBox.Show("Ошибка работы с базой данных");
            }
            finally { _database.CloseConnection(); }
        }
    }
}

//INSERT INTO orders (id_ord, service, cost, sale, phone, FIO)
// VALUES(1, 1, 1, 1, 1, 1);
