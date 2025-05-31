using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {   
        private string category;
        private Database db = new Database();
        DataTable cartTable = new DataTable();
        


        public Form4(string category)
        {
            InitializeComponent();
            this.category = category;
        }
        private void LoadAllProducts()
        {
            string query = "SELECT * FROM Products";
            dataGridView1.DataSource = db.GetData(query);
        }

        private void LoadProductsByCategory(string cat)
        {
            string query = "SELECT * FROM Products WHERE Category = @Category";
            SqlParameter param = new SqlParameter("@Category", cat);
            dataGridView1.DataSource = db.GetData(query, param);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pojectDataSet1.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter1.Fill(this.pojectDataSet1.Products);
           
            LoadProductsByCategory(this.category);
            if (this.category == "ALL")
            {
                LoadAllProducts();
            }
            else
            {
                LoadProductsByCategory(this.category);
            }
            cartTable.Columns.Add("ProductName");
            cartTable.Columns.Add("Price", typeof(decimal));
            cartTable.Columns.Add("Quantity", typeof(int));
            cartTable.Columns.Add("Category");

            dataGridView2.DataSource = cartTable;

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button2_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    try
                    {
                        string name = row.Cells["ProductName"].Value.ToString();
                        decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                        string category = row.Cells["Category"].Value.ToString();

                        bool alreadyExists = cartTable.AsEnumerable().Any(r => r.Field<string>("ProductName") == name);
                        if (!alreadyExists)
                        {
                            cartTable.Rows.Add(name, price, quantity, category);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row from the product list.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    dataGridView2.Rows.Remove(row);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.Trim();
            string item = comboBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm) && string.IsNullOrWhiteSpace(item))
            {
                MessageBox.Show("Please enter a product name or category to search.");
                return;
            }

            string query;
            SqlParameter param;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Search by ProductName or Category using searchTerm
                query = "SELECT * FROM Products WHERE ProductName LIKE @search OR Category LIKE @search";
                param = new SqlParameter("@search", $"%{searchTerm}%");
            }
            else
            {
                // Search by Category using item
                query = "SELECT * FROM Products WHERE Category LIKE @category";
                param = new SqlParameter("@category", $"%{item}%");
            }

            Database db = new Database();
            DataTable result = db.GetData(query, param);

            dataGridView1.DataSource = result;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            string name = textBox2.Text.Trim();
            string address = textBox3.Text.Trim();
            string number = textBox4.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(number))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database db = new Database();
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Name = @Name";
            var exists = (int)db.ExecuteScalar(checkQuery, new SqlParameter("@Name", name));

            if (exists > 0)
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    bool anySuccess = false;
                    foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                    {
                        string productName = row.Cells["ProductName"].Value?.ToString();
                        string priceStr = row.Cells["Price"].Value?.ToString();

                        string insertQuery = "INSERT INTO OrderSummary (ProductName, Price, CheckoutName, Address, PhoneNumber) VALUES (@ProductName, @Price, @CheckoutName, @Address, @PhoneNumber)";
                        int rowsAffected = db.ExecuteCommand(insertQuery,
                            new SqlParameter("@ProductName", productName),
                            new SqlParameter("@Price", priceStr),
                            new SqlParameter("@CheckoutName", name),
                            new SqlParameter("@Address", address),
                            new SqlParameter("@PhoneNumber", number)
                        );

                        if (rowsAffected > 0)
                        {
                            anySuccess = true;
                            dataGridView2.Rows.Remove(row);
                        }
                    }
                    if (anySuccess)
                    {
                        MessageBox.Show("Checkout successful.");
                    }
                    else
                    {
                        MessageBox.Show("Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select at least one product to checkout.");
                }
            }
            else
            {
                MessageBox.Show("User does not exist.");
            }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }
    }
}
