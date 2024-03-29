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

namespace Project
{
    public partial class New_Attendee : Form
    {
        public New_Attendee()
        {
            InitializeComponent();
            panel4.Hide();
            pictureBox7.Hide();
            button1.Hide();
            panel5.Hide();
            pictureBox1.Hide();
            button2.Hide();

        }

        private void Register_Click(object sender, EventArgs e)
        {

            string connectionString = UserInfo.connectionString;

            // Sample data for the new attendee
            string name = textBox3.Text;
            string email = textBox1.Text;
            string password = textBox2.Text;
            string profession = textBox4.Text;
            string worksAt = textBox5.Text;
            string degree = comboBox1.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the email already exists
                string checkEmailSql = "SELECT COUNT(*) FROM Attendee WHERE email = @Email";
                using (SqlCommand checkEmailCommand = new SqlCommand(checkEmailSql, connection))
                {
                    checkEmailCommand.Parameters.AddWithValue("@Email", email);

                    int existingEmailCount = (int)checkEmailCommand.ExecuteScalar();

                    if (existingEmailCount > 0)
                    {
                        panel5.Show();
                        pictureBox1.Show();
                        button2.Show();
                        return;
                    }
                }

                // Insert data into the Attendee table
                string insertSql = "INSERT INTO Attendee (Att_Name, email, pass, profession, works_at, Degree) " +
                                   "VALUES (@Name, @Email, @Password, @Profession, @WorksAt, @Degree)";

                using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                {
                    // Use parameters to prevent SQL injection
                    insertCommand.Parameters.AddWithValue("@Name", name);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@Password", password);
                    insertCommand.Parameters.AddWithValue("@Profession", profession);
                    insertCommand.Parameters.AddWithValue("@WorksAt", worksAt);
                    insertCommand.Parameters.AddWithValue("@Degree", degree);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        panel4.Show();
                        pictureBox7.Show();
                        button1.Show();
                      
                    }
                    else
                    {
                        MessageBox.Show("Failed to register. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel5.Hide();
            pictureBox1.Hide();
            button2.Hide();
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registered_Al_Attendee form = new Registered_Al_Attendee();
            this.Close();
            form.Show();
        }
    }
}

