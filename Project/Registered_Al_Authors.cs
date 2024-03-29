using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project
{

    public partial class Registered_Al_Authors : Form
    {
      
        public Registered_Al_Authors()
        {
            InitializeComponent();
            panel6.Hide();
            label5.Hide();
            pictureBox6.Hide();

        }


        private void submit_abstract_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string pass = textBox2.Text;

            // Validate if email and password are not empty
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

            string connectionString = UserInfo.connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID FROM Authors WHERE email = @Email AND pass = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", pass);

                    // Execute the command and retrieve the ID
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        UserInfo.AuthorID = result.ToString();
                        label5.Text = "Welcome !\n";
                        panel6.Show();
                        pictureBox6.Show();
                        label5.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.");
                        label5.Hide();
                    }
                }
            }
        }




        private void new_author_Click(object sender, EventArgs e)
        {
            New_Authors na = new New_Authors();
            na.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Submit_Abstract sa = new Submit_Abstract();
            sa.Show();
            this.Hide();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
