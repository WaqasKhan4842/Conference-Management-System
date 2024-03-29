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

namespace Project
{
    public partial class Registered_Al_Attendee : Form
    {
        public Registered_Al_Attendee()
        {
            InitializeComponent();
        }
        private void Register_Click(object sender, EventArgs e)
        {

            string connectionString = UserInfo.connectionString;
            string email = textBox1.Text;
            string pass = textBox2.Text;

            // Check if the user exists in the Attendee table and is not already registered for the conference
            if (IsAttendeeExists(connectionString, email, pass) && !IsAttendeeRegisteredForConference(connectionString, email))
            {
                // User exists and is not registered for the conference, proceed with the insertion into At_Conference table
                string con_ID = UserInfo.ConferenceID; // Replace with the actual conference ID

                // SQL query for insertion
                string insertQuery = "INSERT INTO At_Conference (con_ID, email) VALUES (@con_ID, @email)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@con_ID", con_ID);
                        command.Parameters.AddWithValue("@email", email);

                        // Execute the SQL command
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record inserted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert the record.");
                        }
                    }
                }
            }
            else if (IsAttendeeRegisteredForConference(connectionString, email))
            {
                // User is already registered for the conference, display an error message
                MessageBox.Show("User is already registered for the conference.");
            }
            else
            {
                // User does not exist, display an error message
                MessageBox.Show("User does not exist. Please register as a new attendee first.");
            }
        }

        private bool IsAttendeeExists(string connectionString, string email, string password)
        {
            string query = "SELECT COUNT(*) FROM Attendee WHERE email = @email AND pass = @pass";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@pass", password);

                    // ExecuteScalar returns the first column of the first row
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        private bool IsAttendeeRegisteredForConference(string connectionString, string email)
        {
            string query = "SELECT COUNT(*) FROM At_Conference WHERE email = @email AND con_ID = @con_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@con_ID", UserInfo.ConferenceID);

                    // ExecuteScalar returns the first column of the first row
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Attendee na = new New_Attendee();
            na.Show();
        }
    }
}
