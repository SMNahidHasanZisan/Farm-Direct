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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        private Database db = new Database();
        DataTable dt = new DataTable();
        public Form5()
        {
            InitializeComponent();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Text = UserSession.UserName;
            label3.Text = UserSession.Role;

            if (UserSession.Role == "Admin")
            {
                
                button2.Enabled = true;
                button3.Enabled = true;
                dataGridView1.ReadOnly = false;
            }
            else if (UserSession.Role == "User")
            {
                
                button2.Enabled = true;
                button3.Enabled = false;

                dataGridView1.ReadOnly = false;
            }
            else if (UserSession.Role == "Seller")
            {
                
                button2.Enabled = false;
                button3.Enabled = false;

                dataGridView1.ReadOnly = true;
            }
            LoadUserData();
        }
        private void LoadUserData()
        {
            string checkQuery = "SELECT COUNT(*) FROM  UserInformation WHERE  Username = @Username";
            var exists = (int)db.ExecuteScalar(checkQuery, new SqlParameter("@Username", UserSession.UserName));

            if (exists == 0)
            {
                string insertQuery = "INSERT INTO UserInformation (Username, Role) VALUES (@Username, @Role)";

                int rowsAffected = db.ExecuteCommand(insertQuery,
                new SqlParameter("@Username", UserSession.UserName),
                new SqlParameter("@Role", UserSession.Role)
                );
                return;
            }
            
            string query;

            if (UserSession.Role == "Admin")
            {
                query = "SELECT * FROM UserInformation"; // See all
            }
            else
            {
                query = "SELECT * FROM UserInformation WHERE Username = @Username"; // Only self
            }

            SqlParameter param = new SqlParameter("@Username", UserSession.UserName);
            dataGridView1.DataSource = db.GetData(query, param);
        }
        private void LoadOrderData()
        {
            string query;

            if (UserSession.Role == "Admin")
            {
                query = "SELECT * FROM OrderSummary"; // All orders
            }
            else
            {
                query = "SELECT * FROM OrderSummary WHERE CheckoutName = @CheckoutName"; // Only user/seller orders
            }


            SqlParameter param = new SqlParameter("@CheckoutName", UserSession.UserName);
            dataGridView1.DataSource = db.GetData(query, param);

        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(UserSession.Role != "Admin")
        return;

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one user to delete.");
                return;
            }

            

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells["Username"].Value != null)
                {
                    string username = row.Cells["Username"].Value.ToString();

                    string deleteQuery = "DELETE FROM UserInformation WHERE Username = @Username";
                    db.ExecuteCommand(deleteQuery, new SqlParameter("@Username", username));
                }
            }

            MessageBox.Show("Selected user(s) removed from database.");
            LoadUserData(); // Reload the updated data into the DataGridView
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadOrderData();


        }

       

        private void button2_Click(object sender, EventArgs e)
        {

            string username = UserSession.UserName;
            string fullName = textBox1.Text.Trim();
            string ageText = textBox2.Text.Trim();
            string gender = radioButton1.Checked ? "Male" : (radioButton2.Checked ? "Female" : "");
            string address = textBox4.Text.Trim();

            // Validate fields
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(ageText) ||
                string.IsNullOrWhiteSpace(gender) ||
                string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("All fields must be filled out to update.");
                return;
            }

            // Validate Age as number
            if (!int.TryParse(ageText, out int age))
            {
                MessageBox.Show("Please enter a valid age.");
                return;
            }

            // Check if user data exists
            string checkQuery = "SELECT COUNT(*) FROM UserInformation WHERE Username = @Username";
            int exists = (int)db.ExecuteScalar(checkQuery, new SqlParameter("@Username", username));

            if (exists == 0)
            {
                MessageBox.Show("No existing information found. Please use Add instead.");
                return;
            }

            // Update query
            string updateQuery = @"
        UPDATE UserInformation 
        SET FullName = @FullName, Age = @Age, Gender = @Gender, Address = @Address 
        WHERE Username = @Username";

            int result = db.ExecuteCommand(updateQuery,
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Age", age),
                new SqlParameter("@Gender", gender),
                new SqlParameter("@Address", address),
                new SqlParameter("@Username", username));

            if (result > 0)
                MessageBox.Show("Information updated successfully.");
            else
                MessageBox.Show("Update failed. Please try again.");

            LoadUserData();
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

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("All");
            f4.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }
    }
}

