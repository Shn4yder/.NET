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
    public partial class DataUploadForm : Form
    {
        MySqlDataReader _reader;
        private List<RowOfData> _data = new List<RowOfData>();
        private User user;
        int row_counter = 0;

        public DataUploadForm()
        {
            InitializeComponent();
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


            dataGrid.DataSource = null;
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


        private void AddDataGrid(RowOfData row)
        {
            dataGrid.Rows.Add(row.id_ord, row.service, row.cost, row.sale, row.phone, row.FIO);
        }

        private void DataUploadForm_Shown(object sender, EventArgs e)
        {
            User user = new User();
            //dataGrid.Columns[0].ReadOnly= true;
            HeaderOfTheTable();
            
        }


        private void HeaderOfTheTable()
        {
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Номер заказа";
            column1.Width = 75;
            column1.Name = "id_ord";
            column1.Frozen = true;
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

            dataGrid.AllowUserToAddRows = false;
            dataGrid.ReadOnly = true;
        }


        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _data.Clear();

            DatabaseManager _database = new DatabaseManager();
            MySqlCommand _сommand = new MySqlCommand("SELECT * FROM `orders`",
                _database.GetConnection);
            MySqlDataReader _reader;

            dataGrid.DataSource = null;
            dataGrid.Rows.Clear();

            try
            {
                _database.OpenConnection();
                _reader = _сommand.ExecuteReader();

                while (_reader.Read())
                {
                    RowOfData row = new RowOfData(_reader["id_ord"], _reader["service"], _reader["cost"], _reader["sale"], _reader["phone"], _reader["FIO"]);
                    _data.Add(row);
                }

                for (int i = 0; i < _data.Count; i++)
                {
                    AddDataGrid(_data[i]);
                    dataGrid.Rows[i].Cells[0].ReadOnly= true;
                }   
            }
            catch
            {
                MessageBox.Show("Ошибка работы с базой данных");
            }
            finally { _database.CloseConnection(); }
        }

        private void выгрузитьНаСерверToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Добавить данные в базу данных?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DatabaseManager _manager = new DatabaseManager();

                try 
                {
                    bool add = true;
                    _manager.OpenConnection();

                    int row = dataGrid.Rows.Count;

                    for (int i = row_counter - 1; i >= 0; i--)
                    {
                        if (Convert.ToString(this.dataGrid.Rows[row - i -1].Cells[0].Value) != "" &&
                            Convert.ToString(this.dataGrid.Rows[row - i - 1].Cells[1].Value) != "" &&
                            Convert.ToString(this.dataGrid.Rows[row - i - 1].Cells[2].Value) != "" &&
                            Convert.ToString(this.dataGrid.Rows[row - i - 1].Cells[3].Value) != "" &&
                            Convert.ToString(this.dataGrid.Rows[row - i - 1].Cells[4].Value) != "" &&
                            Convert.ToString(this.dataGrid.Rows[row - i - 1].Cells[5].Value) != "")
                        {
                            string _commandString = "INSERT INTO `orders` (`id_ord`, `service`, `cost`, `sale`, `FIO`, `phone`) " +
                                "VALUES (@id_ord, @service, @cost, @sale, @FIO, @phone)";

                            MySqlCommand _command = new MySqlCommand(_commandString, _manager.GetConnection);

                            _command.Parameters.Add("@id_ord", MySqlDbType.Int32).Value = this.dataGrid.Rows[row - i - 1].Cells[0].Value;
                            _command.Parameters.Add("@service", MySqlDbType.VarChar).Value = this.dataGrid.Rows[row - i - 1].Cells[5].Value;
                            _command.Parameters.Add("@cost", MySqlDbType.VarChar).Value = this.dataGrid.Rows[row - i - 1].Cells[3].Value;
                            _command.Parameters.Add("@sale", MySqlDbType.VarChar).Value = this.dataGrid.Rows[row - i - 1].Cells[2].Value;
                            _command.Parameters.Add("@FIO", MySqlDbType.VarChar).Value = this.dataGrid.Rows[row - i - 1].Cells[4].Value;
                            _command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = this.dataGrid.Rows[row - i - 1].Cells[1].Value;

                            if (_command.ExecuteNonQuery() != 1)
                                add = false;
                        }
                        else
                        {
                            MessageBox.Show("Не все поля заполнены", "Внимание");
                            add = false;
                        }

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
            row_counter = 0;
        }

        private void добавитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> new_data = new List<string>();


            DialogUpload dialog = new DialogUpload();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                new_data = dialog.Data;
                textBox1.Text = row_counter.ToString();
                int rowNumber = dataGrid.Rows.Add();

                dataGrid.Rows[rowNumber].Cells[0].Value = new_data[0].ToString();
                dataGrid.Rows[rowNumber].Cells[1].Value = new_data[5].ToString();
                dataGrid.Rows[rowNumber].Cells[2].Value = new_data[3].ToString();
                dataGrid.Rows[rowNumber].Cells[3].Value = new_data[2].ToString();
                dataGrid.Rows[rowNumber].Cells[4].Value = new_data[4].ToString();
                dataGrid.Rows[rowNumber].Cells[5].Value = new_data[1].ToString();

                row_counter += 1;
                textBox1.Text = row_counter.ToString();

            }
        }
    }
}
