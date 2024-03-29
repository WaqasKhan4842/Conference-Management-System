using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project
{
    public partial class Login : Form
    {
        private bool successfulLogin = false;
        private string selectedRole;
        public Login()
        {
            InitializeComponent();
            label5.Hide();
            panel6.Hide();
            panel6_Dimension();
            pictureBox6.Hide();
        }
        public void panel6_Dimension()
        {
            panel6.Width = 171;
            panel6.Height = 100;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            
            HomePage form = new HomePage();
            string email = textBox1.Text;
            string pass = textBox2.Text;
            string table_name = comboBox1.Text;
            // Check if both email and password are provided
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Email or Password missing");
                return;
            }
           
            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM " +  comboBox1.Text + " WHERE email = @Email AND pass = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", pass);

                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {

                        UserInfo.Email = email;

                        label5.Text = "Welcome !\n";
                       
                        panel6.Show();
                        pictureBox6.Show();
                        label5.Show();
                        button1.Show();
                        successfulLogin = true;
                        selectedRole = table_name;
                        



                    }
                    else
                    {
                       
                        label5.Text = "Invalid !";
                        panel6.Show();
                        label5.Show();
                        button1.Hide();
                        pictureBox6.Hide();

                    }
                       
                }
            }
           

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            pictureBox2.BackColor = Color.White;
            panel4.BackColor = SystemColors.Control;
            textBox2.BackColor = SystemColors.Control;
            pictureBox3.BackColor = SystemColors.Control;
            comboBox1.BackColor = SystemColors.Control;

            panel5.BackColor = SystemColors.Control;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            panel4.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            pictureBox3.BackColor = Color.White;
            pictureBox4.BackColor = Color.White;
            panel2.BackColor = SystemColors.Control;
            textBox1.BackColor = SystemColors.Control;
            pictureBox2.BackColor = SystemColors.Control;
            comboBox1.BackColor = SystemColors.Control;
            panel5.BackColor = SystemColors.Control;

        }

        private void comboBox1_Clicked(object sender, EventArgs e)
        {
            comboBox1.BackColor = Color.White;
            panel5.BackColor = Color.White;
            pictureBox5.BackColor = Color.White;
            panel2.BackColor = SystemColors.Control;
            textBox1.BackColor = SystemColors.Control;
            pictureBox2.BackColor = SystemColors.Control;
            panel4.BackColor = SystemColors.Control;
            textBox2.BackColor = SystemColors.Control;
            pictureBox3.BackColor = SystemColors.Control;

        }
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;

        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (successfulLogin)
            {
                // Trigger actions based on the selected role
                switch (selectedRole)
                {
                    case "Admin":
                        Admin_Dashboard ad = new Admin_Dashboard();
                        ad.Show();
                        this.Hide();
                        break;
                    case "Organizers":
                        Organizer_Dashboard od = new Organizer_Dashboard();
                        od.Show();
                        this.Hide();
                        break;
                    case "Reviewer":
                       Reviewer_Window rw = new Reviewer_Window();
                        rw.Show();
                        this.Hide();
                        break;
                       
                }
            }
            else
            {
                // Handle the case where the user hasn't successfully logged in yet
                MessageBox.Show("Please log in first.");
            }
        }

        private void cross_it_Click(object sender, EventArgs e)
        {
            label5.Hide();
            panel6.Hide();
            pictureBox6.Hide();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            hp.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            this.Close();
            HomePage pg = new HomePage();
            pg.Show();

        }
    }
}
