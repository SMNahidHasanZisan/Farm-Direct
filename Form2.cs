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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            string name = textBox1.Text.Trim();
            string pass = textBox3.Text.Trim();
            string mail = textBox2.Text.Trim();
            string user = comboBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pass) ||
                string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(user))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database db = new Database();

            // Check if username already exists
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Name = @Name";
            var exists = (int)db.ExecuteScalar(checkQuery, new SqlParameter("@Name", name));

            if (exists > 0)
            {
                MessageBox.Show("Username already exists. Please choose a different one.");
                return;
            }

            // Insert new user
            string insertQuery = "INSERT INTO Users (Name, Pass, Mail, [User]) VALUES (@Name, @Pass, @Mail, @User)";
            int rowsAffected = db.ExecuteCommand(insertQuery,
                new SqlParameter("@Name", name),
                new SqlParameter("@Pass", pass),
                new SqlParameter("@Mail", mail),
                new SqlParameter("@User", user)
            );

            if (rowsAffected > 0)
            {
                MessageBox.Show("Signup successful! You can now log in.");
                this.Close(); // Optionally close the signup form
                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
            }
            else
            {
                MessageBox.Show("Signup failed. Please try again.");
            }

        }
    }
}
