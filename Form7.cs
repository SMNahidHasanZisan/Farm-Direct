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

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadProductData()
        {
            Database db = new Database();
            string query = "SELECT * FROM Products";
            dataGridView1.DataSource = db.GetData(query);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string productName = textBox1.Text.Trim();
            string priceText = textBox2.Text.Trim();
            string quantityText = textBox3.Text.Trim();
            string category = comboBox1.Text.Trim();
            int sell=1;

            if (string.IsNullOrWhiteSpace(productName) ||
                string.IsNullOrWhiteSpace(priceText) ||
                string.IsNullOrWhiteSpace(quantityText) ||
                string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Validate price and quantity as numbers
            if (!decimal.TryParse(priceText, out decimal price) || !int.TryParse(quantityText, out int quantity))
            {
                MessageBox.Show("Please enter valid numeric values for Price and Quantity.");
                return;
            }

            Database db = new Database();

            string insertQuery = @"INSERT INTO Products (ProductName, Price, Quantity,Sell, Category) 
                           VALUES (@ProductName, @Price, @Quantity,@Sell, @Category)";

            int rowsAffected = db.ExecuteCommand(insertQuery,
                new SqlParameter("@ProductName", productName),
                new SqlParameter("@Price", price),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@Sell", sell),
                new SqlParameter("@Category", category)
            );

            if (rowsAffected > 0)
            {
                MessageBox.Show("Product added successfully.");
                // Optionally clear fields
                LoadProductData();
            }
            else
            {
                MessageBox.Show("Failed to add product. Please try again.");
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
         LoadProductData();
     
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("All");
            f4.Show();
        }
    }
}
