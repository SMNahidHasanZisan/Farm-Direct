using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
        //Check if you are a farmer or not, if yes then you can access the form7
        private void button13_Click(object sender, EventArgs e)
        {
            if (UserSession.Role == "Farmer")         // check if the user is a farmer from UserSession
            {
                this.Hide();
                Form7 f7 = new Form7();
                f7.Show();
            }
            else                                     // if the user is not a farmer, show a message box
            {
                MessageBox.Show("You are not a Farmer ");
            }
        }
        // take you in the form5 which is the user information form or account settings form 
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }
        // take you in the wearher form which is the form6
        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }
        // take you in the form4 which is the product form or cart form
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("All");
            f4.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("Rice");
            f4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("Vegetables");
            f4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("Fruits");
            f4.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("Pesticides");
            f4.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("Tractor");
            f4.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("Land");
            f4.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4("Manpower");
            f4.Show();
        }
    }
}
