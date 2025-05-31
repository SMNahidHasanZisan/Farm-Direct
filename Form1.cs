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
using System.Data.SqlTypes;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string userPass = textBox2.Text;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userPass))
            {
                MessageBox.Show("Please enter both Name and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database db = new Database();
            string query = "SELECT COUNT(*) FROM Users WHERE Name = @Name AND Pass = @Pass";
            var result = db.ExecuteScalar(query,
                new SqlParameter("@Name", userName),
                new SqlParameter("@Pass", userPass));

            if ((int)result > 0)
            {
                string roleQuery = "SELECT [User] FROM Users WHERE Name = @Name";
                var roleResult = db.ExecuteScalar(roleQuery, new SqlParameter("@Name", userName));
                string role = roleResult?.ToString();
                UserSession.Role = role;

                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UserSession.UserName = textBox1.Text.Trim();
                this.Hide();
                Form3 f3 = new Form3();
                f3.Show();
            }
            else
            {
                MessageBox.Show("Invalid Id or Name.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
