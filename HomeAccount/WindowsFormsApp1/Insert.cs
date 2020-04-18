using System;

using System.Data;

using System.Windows.Forms;

using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Insert : Form
    {
        int userId;
        public Insert(int user_id)
        {
            InitializeComponent();
            userId = user_id;
            string datasource = @"LAPTOP-CP8KRCNS";
            string database = "CostAccount";
            string connString = @"Data Source=" + datasource + ";Initial Catalog=" +
            database + ";Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            string query1 = "select * from products_type;";
            SqlCommand command = new SqlCommand(query1, conn);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataSet dt = new DataSet();
            adapt.Fill(dt);

            comboBox1.DataSource = dt.Tables[0].DefaultView;
            comboBox1.Name = "type_product";
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";


           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string datasource = @"LAPTOP-CP8KRCNS";
            string database = "CostAccount";
            string connString = @"Data Source=" + datasource + ";Initial Catalog=" +
            database + ";Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            int selected_type = Convert.ToInt32(comboBox1.SelectedValue);
            
            string query = "INSERT INTO purchase(name_product, type, amount, weight, date, place, cost, customer) VALUES (" + "'" + textBox1.Text + "'" + "," + Convert.ToInt32(comboBox1.SelectedValue) + "," +
                numericUpDown1.Value + "," + numericUpDown2.Value + "," + "'" + dateTimePicker1.Value.Date + "'" + "," + "'" + textBox2.Text + "'" + "," + numericUpDown3.Value + "," + userId + ");";
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();

            MessageBox.Show("Запись успешно добавлена");
            
        }
    }

}
