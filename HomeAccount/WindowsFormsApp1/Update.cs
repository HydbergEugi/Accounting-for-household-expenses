using System;

using System.Data;

using System.Windows.Forms;

using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Update : Form
    {
        public DataGridViewRow d1;
        public Update(DataGridViewRow d  )
        {
            InitializeComponent();

            d1 = d;
            string datasource = @"LAPTOP-CP8KRCNS";
            string database = "CostAccount";
            string connString = @"Data Source=" + datasource + ";Initial Catalog=" +
            database + ";Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            string query1 = "select * from products_type;";
            SqlCommand command = new SqlCommand(query1, conn);
            //SqlDataReader dr = command.ExecuteReader();
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataSet dt = new DataSet();
            adapt.Fill(dt);

            comboBox1.DataSource = dt.Tables[0].DefaultView;
            comboBox1.Name = "type_product";
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";

            query1 = "select id from products_type where name = '" + d.Cells["type_product"].Value + "'";
            command = new SqlCommand(query1, conn);
            int value = Convert.ToInt32( command.ExecuteScalar() );
            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 1000000;
            textBox1.Text = d.Cells["name_product"].Value.ToString();
            textBox2.Text = d.Cells["place"].Value.ToString();
            numericUpDown1.Value = Convert.ToInt32( d.Cells["amount"].Value);
            numericUpDown2.Value = Convert.ToInt32(d.Cells["weight"].Value);
            numericUpDown3.Value = Convert.ToInt32(d.Cells["cost"].Value);
            comboBox1.SelectedValue = value;
            dateTimePicker1.Value = Convert.ToDateTime( d.Cells["date"].Value );


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string datasource = @"LAPTOP-CP8KRCNS";
            string database = "CostAccount";
            string connString = @"Data Source=" + datasource + ";Initial Catalog=" +
            database + ";Integrated Security=True";


            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string query = "update purchase set name_product = '" + textBox1.Text + "' where id = " + Convert.ToInt32(d1.Cells["id"].Value);

            string query1 = "update purchase set amount = "+ numericUpDown1.Value + " where id =" + Convert.ToInt32(d1.Cells["id"].Value);

            string query2 = "update purchase set weight = "+ numericUpDown2.Value +" where id =" + Convert.ToInt32(d1.Cells["id"].Value);

            string query3 = "update purchase set date = '"+  dateTimePicker1.Value + "' where id =" + Convert.ToInt32(d1.Cells["id"].Value);

            string query4 = "update purchase set place = '" + textBox2.Text + "' where id =" + Convert.ToInt32(d1.Cells["id"].Value);

            string query5 = "update purchase set cost = " + numericUpDown3.Value + " where id =" + Convert.ToInt32(d1.Cells["id"].Value);

            string query6 = "update purchase set type = "+ comboBox1.SelectedValue + " where id =" + Convert.ToInt32(d1.Cells["id"].Value);
            
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            command = new SqlCommand(query1, conn);
            command.ExecuteNonQuery();
            command = new SqlCommand(query2, conn);
            command.ExecuteNonQuery();
            command = new SqlCommand(query3, conn);
            command.ExecuteNonQuery();
            command = new SqlCommand(query4, conn);
            command.ExecuteNonQuery();
            command = new SqlCommand(query5, conn);
            command.ExecuteNonQuery();
            command = new SqlCommand(query6, conn);
            command.ExecuteNonQuery();
            

            conn.Close();

            MessageBox.Show("Запись успешно изменена");
        }
    }
}
