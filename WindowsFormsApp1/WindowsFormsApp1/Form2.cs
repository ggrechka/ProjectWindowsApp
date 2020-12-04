using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void Get_Table(string table_name, int num_dG)
        {
            Form1 f1 = new Form1();
            string CommandText = "SELECT * FROM ";
            CommandText += table_name;
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, f1.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, table_name);
            if (num_dG == 1) dataGridView1.DataSource = ds.Tables[table_name].DefaultView;
            if (num_dG == 2) dataGridView2.DataSource = ds.Tables[table_name].DefaultView;
            if (num_dG == 3) dataGridView3.DataSource = ds.Tables[table_name].DefaultView;
            if (num_dG == 4) dataGridView4.DataSource = ds.Tables[table_name].DefaultView;
            if (num_dG == 5) dataGridView5.DataSource = ds.Tables[table_name].DefaultView;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Get_Table("Маршрут", 1); // заполнение таблицы "Маршрут"
            Get_Table("Билет", 2); // заполнение таблицы "Билет"
            Get_Table("Водитель", 3);
            Get_Table("Автобус", 4);
            Get_Table("Диспетчер", 5);
            textBox1.Text = "0";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            // данные из ячеек таблицы "Маршрут"
            row = dataGridView1.CurrentCell.RowIndex;
            label2.Text = "Маршрут - " + "№" + Convert.ToString(dataGridView1[1, row].Value) +
                "/" + Convert.ToString(dataGridView1[2, row].Value) +
                "/" + Convert.ToString(dataGridView1[3, row].Value) +
                "/" + Convert.ToString(dataGridView1[4, row].Value) +
                "/" + Convert.ToString(dataGridView1[7, row].Value) +
                " - " + Convert.ToString(dataGridView1[8, row].Value);
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            // данные из таблицы "Билет"
            row = dataGridView2.CurrentCell.RowIndex;
            label3.Text = "Билет: место №" + Convert.ToString(dataGridView2[1, row].Value) +
                " / " + Convert.ToString(dataGridView2[2, row].Value) +
                " / " + Convert.ToString(dataGridView2[3, row].Value) +
                " / " + Convert.ToString(dataGridView2[4, row].Value) +
                " / " + Convert.ToString(dataGridView2[5, row].Value) +
                " / " + Convert.ToString(dataGridView2[6, row].Value);
        }

        private void dataGridView3_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            // данные о водителе
            row = dataGridView3.CurrentCell.RowIndex;
            label4.Text = "Водитель: " + Convert.ToString(dataGridView3[1, row].Value);
        }

        private void dataGridView4_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            // информация об автобусе
            row = dataGridView4.CurrentCell.RowIndex;
            label5.Text = "Автобус: №" + Convert.ToString(dataGridView4[1, row].Value) +
                " / " + Convert.ToString(dataGridView4[2, row].Value) +
                " / " + Convert.ToString(dataGridView4[3, row].Value) +
                " / " + Convert.ToString(dataGridView4[4, row].Value);
        }

        private void dataGridView5_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            // информация о диспетчере
            row = dataGridView5.CurrentCell.RowIndex;
            label6.Text = "Диспетчер: " + Convert.ToString(dataGridView5[1, row].Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // фильтр к таблице "Маршрут"
            string CommandText = "SELECT * FROM [Маршрут]";
            // формируем переменную CommandText
            if (textBox2.Text == "")
                CommandText = "SELECT * FROM [Маршрут]";
            else if (comboBox1.SelectedIndex == 0) // № перевозки
                CommandText = "SELECT * FROM [Маршрут] WHERE [Номер маршрута] = '" + textBox2.Text + "'"; // работает
            else if (comboBox1.SelectedIndex == 1) // пункт назначения
                CommandText = "SELECT * FROM [Маршрут] WHERE [Пункт назначения] LIKE '" + textBox2.Text + "%'";
            else if (comboBox1.SelectedIndex == 2) // район
                CommandText = "SELECT * FROM [Маршрут] WHERE Район LIKE '" + textBox2.Text + "%'";
            else if (comboBox1.SelectedIndex == 3) // область
                CommandText = "SELECT * FROM [Маршрут] WHERE Область LIKE '" + textBox2.Text + "%'";

            Form1 f = new Form1();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, f.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "[Маршрут]");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // фильтр к таблице "Билет"
            string CommandText = "SELECT * FROM [Билет]";
            // формируем переменную CommandText
            if (textBox3.Text == "")
                CommandText = "SELECT * FROM [Билет]";
            else if (comboBox2.SelectedIndex == 0)
                CommandText = "SELECT * FROM [Билет] WHERE [Место] = " + textBox3.Text;
            else if (comboBox2.SelectedIndex == 1)
                CommandText = "SELECT * FROM [Билет] WHERE [Ф_И_О] LIKE '" + textBox3.Text + "%'";

            Form1 f = new Form1();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, f.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "[Маршрут]");
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
        }
    }
}
