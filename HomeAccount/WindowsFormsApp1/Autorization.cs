
using System;

using System.Data;

using System.Windows.Forms;

using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Autorization : Form
    {
        DataSet ds;
        PurchaseList f;
        public Autorization()
        {
            InitializeComponent();
            //f.FormClosing += AppClose;
            string datasource = @"LAPTOP-CP8KRCNS";
            string database = "CostAccount";
            string connString = @"Data Source=" + datasource + ";Initial Catalog=" +
database + ";Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string query1 = "select * from users;";
            SqlCommand command = new SqlCommand(query1, conn);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            ds = new DataSet();
            adapt.Fill(ds);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if ((textBox1.Text == ds.Tables[0].Rows[i][1].ToString()) && (textBox2.Text == ds.Tables[0].Rows[i][2].ToString()))
                {
                    f = new PurchaseList(Convert.ToInt32( ds.Tables[0].Rows[i][0] ), this);
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
        }


    }
}
