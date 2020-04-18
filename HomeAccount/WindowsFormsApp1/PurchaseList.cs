using System;

using System.Data;

using System.Windows.Forms;

using System.Data.SqlClient;

using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class PurchaseList : Form
    {
        int userId;
        Autorization BaseVariable;
        int fullBalance = 50000;
        int currentBalance;
        public void update_purchase()
        {
            string datasource = @"LAPTOP-CP8KRCNS";
            string database = "CostAccount";
            string connString = @"Data Source=" + datasource + ";Initial Catalog=" +
            database + ";Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();



            string query1 = "select purchase.id, name_product, type, amount, weight, date, place, cost, customer, p.name as type_product from purchase join products_type p on" +
            " purchase.type = p.id where customer = " + userId + ";";
            SqlCommand command = new SqlCommand(query1, conn);
            SqlDataReader dr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;


            currentBalance = fullBalance;
            query1 = "select sum(cost) as fullCost from purchase";
            command = new SqlCommand(query1, conn);
            currentBalance = Convert.ToInt32(command.ExecuteScalar());
            currentBalance = fullBalance - currentBalance;
            textBox2.Text = "Ваш баланс: " + currentBalance.ToString();
        }

        public PurchaseList(int user_id, Autorization variable)
        {
            this.FormClosing += AppClose;
            InitializeComponent();

            userId = user_id;
            BaseVariable = variable;

            string datasource = @"LAPTOP-CP8KRCNS";
            string database = "CostAccount";
            string connString = @"Data Source=" + datasource + ";Initial Catalog=" +
database + ";Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string query1 = "select purchase.id, name_product, type, amount, weight, date, place, cost, customer, p.name as type_product  from purchase join products_type p on" +
                " purchase.type = p.id where customer = " + user_id + ";";
            SqlCommand command = new SqlCommand(query1, conn);
            SqlDataReader dr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;

            DataGridViewComboBoxColumn comboBoxColumn =
new DataGridViewComboBoxColumn();

            query1 = "select * from products_type;";
            command = new SqlCommand(query1, conn);
            dr = command.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);

            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["type"].Visible = false;
            dataGridView1.Columns["customer"].Visible = false;
            dataGridView1.Columns["name_product"].HeaderText = "Наименование покупки";
            dataGridView1.Columns["amount"].HeaderText = "Количество";
            dataGridView1.Columns["weight"].HeaderText = "Вес";
            dataGridView1.Columns["date"].HeaderText = "Дата";
            dataGridView1.Columns["place"].HeaderText = "Место";
            dataGridView1.Columns["cost"].HeaderText = "Стоимость";
            dataGridView1.Columns["type_product"].HeaderText = "Тип покупки";

            query1 = "select username from users where id = " + user_id + ";";
            command = new SqlCommand(query1, conn);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            currentBalance = fullBalance;

            query1 = "select sum(cost) as fullCost from purchase";
            command = new SqlCommand(query1, conn);
            currentBalance = Convert.ToInt32( command.ExecuteScalar() );
            currentBalance = fullBalance - currentBalance;
            textBox1.Text = "Добро пожаловать, " + ds.Tables[0].Rows[0]["username"].ToString();
            textBox2.Text = "Ваш баланс: " + currentBalance.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            Insert enterRow = new Insert(userId);
            enterRow.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно желаете удалить данную запись?", "Удаление записи",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string datasource = @"LAPTOP-CP8KRCNS";
                string database = "CostAccount";
                string connString = @"Data Source=" + datasource + ";Initial Catalog=" +
                database + ";Integrated Security=True";


                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                string query = "DELETE FROM purchase WHERE id = " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id"].Value.ToString();

                SqlCommand command = new SqlCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();
                update_purchase();
            }          

        }

        private void button3_Click(object sender, EventArgs e)
        {
            update_purchase();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update change_purchase = new Update(dataGridView1.CurrentRow);
            change_purchase.Show();

        }


        private void AppClose(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход",
               MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                BaseVariable.Close();
            }
        }
    }
}
