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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project
{
    public partial class Add_Event_Form : Form
    {
        public Add_Event_Form()
        {
            InitializeComponent();


        }

        private void ChildForm_1_Load(object sender, EventArgs e)
        {

        }



        private void Submit_Form_Click(object sender, EventArgs e)
        {
            // Validate that all required fields are filled
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text) ||
                string.IsNullOrWhiteSpace(dateTimePicker1.Text) ||
                string.IsNullOrWhiteSpace(dateTimePicker2.Text) ||
                string.IsNullOrWhiteSpace(dateTimePicker3.Text) ||
                string.IsNullOrWhiteSpace(dateTimePicker4.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(richTextBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox10.Text))
            {
                MessageBox.Show("Please fill in all the required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Stop further processing if validation fails
            }
            // Validate capacity is an integer
            if (!int.TryParse(textBox10.Text, out _))
            {
                MessageBox.Show("Please enter a valid integer for Capacity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Stop further processing if validation fails
            }
            // Validate date conditions
            DateTime conferenceDate = dateTimePicker5.Value;
            DateTime registrationStartDate = dateTimePicker1.Value;
            DateTime registrationEndDate = dateTimePicker2.Value;
            DateTime submissionStartDate = dateTimePicker3.Value;
            DateTime submissionEndDate = dateTimePicker4.Value;

            if (conferenceDate <= DateTime.Now.AddDays(28))
            {
                MessageBox.Show("The conference date must be at least 4 weeks in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Stop further processing if validation fails
            }

            // Ensure registration dates are at least 3 days before the conference date
            if (registrationStartDate >= conferenceDate || registrationEndDate >= conferenceDate || (conferenceDate - registrationStartDate).Days < 3 || (conferenceDate - registrationEndDate).Days < 3)
            {
                MessageBox.Show("Registration dates must be at least 3 days before the conference date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Stop further processing if validation fails
            }

            // Ensure submission dates are at least 3 days before the conference date
            if (submissionStartDate >= conferenceDate || submissionEndDate >= conferenceDate || (conferenceDate - submissionStartDate).Days < 3 || (conferenceDate - submissionEndDate).Days < 3)
            {
                MessageBox.Show("Submission dates must be at least 3 days before the conference date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Stop further processing if validation fails
            }
            string connectionString = UserInfo.connectionString;
            string nextConferenceID = GetNextConferenceID(connectionString);
            string org_Id = get_ID(connectionString);
            if (ConferenceExists(connectionString, textBox1.Text, org_Id))
            {
                MessageBox.Show("A conference with the same name and organizer already exists.", "Duplicate Conference", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Stop further processing if a duplicate conference is found
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to insert a new conference
                string query = "INSERT INTO Conference (ID, con_Name, con_Date, con_location, reg_start_date, reg_end_date, sub_start_date, sub_end_date, con_type, about, capacity,org_ID) " +
                               "VALUES (@ID, @con_Name, @con_Date, @con_location, @reg_start_date, @reg_end_date, @sub_start_date, @sub_end_date, @con_type, @about, @capacity,@OrgID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@ID", nextConferenceID);
                    command.Parameters.AddWithValue("@con_Name", textBox1.Text);
                    command.Parameters.AddWithValue("@con_location", textBox11.Text);
                    command.Parameters.AddWithValue("@con_Date", dateTimePicker5.Value);
                    command.Parameters.AddWithValue("@reg_start_date", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@reg_end_date", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@sub_start_date", dateTimePicker3.Value);
                    command.Parameters.AddWithValue("@sub_end_date", dateTimePicker4.Value);
                    command.Parameters.AddWithValue("@con_type", comboBox1.Text);
                    command.Parameters.AddWithValue("@about", richTextBox1.Text);
                    command.Parameters.AddWithValue("@capacity", textBox10.Text);
                    command.Parameters.AddWithValue("@OrgID", org_Id);


                    // Execute the query
                    command.ExecuteNonQuery();
                }
                // Add speakers if provided
                AddSpeakers(connection, nextConferenceID, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                AddSpeakers(connection, nextConferenceID, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text);
            }
            MessageBox.Show("Conference successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

          
        }

        private string GetNextConferenceID(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to get the max conference ID
                string query = "SELECT MAX(CONVERT(INT, SUBSTRING(ID, 4, LEN(ID)))) FROM Conference";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int maxID = Convert.ToInt32(result);
                        return "CON" + (maxID + 1).ToString("D3");
                    }
                    else
                    {
                        return "CON100"; // Default if no conferences exist
                    }
                }
            }
        }
        private string get_ID(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT org_ID FROM Organizers WHERE email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Email", UserInfo.Email);

                    // Execute the query and return the org_ID
                    object result = command.ExecuteScalar();

                    return result != null ? result.ToString() : null;
                }
            }

        }
        private bool ConferenceExists(string connectionString, string con_Name, string org_ID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Conference WHERE con_Name = @ConName AND org_ID = @OrgID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@ConName", con_Name);
                    command.Parameters.AddWithValue("@OrgID", org_ID);

                    // Execute the query and check if any conferences match the criteria
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }
        private void AddSpeakers(SqlConnection connection, string conferenceID, string speakerName, string degree, string worksAt, string profession)
        {
            if (!string.IsNullOrWhiteSpace(speakerName))
            {
                // Query to get the max speaker ID
                string maxSpeakerIdQuery = "SELECT MAX(CONVERT(INT, SUBSTRING(ID, 4, LEN(ID)))) FROM Speakers";

                int maxSpeakerId;

                using (SqlCommand getMaxSpeakerIdCommand = new SqlCommand(maxSpeakerIdQuery, connection))
                {
                    object result = getMaxSpeakerIdCommand.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        maxSpeakerId = Convert.ToInt32(result);
                    }
                    else
                    {
                        maxSpeakerId = 100; // Default if no speakers exist
                    }
                }
                // Generate the new speaker ID
                string newSpeakerID = "SPK" + (maxSpeakerId + 1).ToString("D3");
                // Query to insert a new speaker
                string speakerQuery = "INSERT INTO Speakers (ID, Sp_Name, Degree, Works_at, Profession, conf_ID) " +
                                 "VALUES (@ID, @Sp_Name, @Degree, @Works_at, @Profession, @ConfID)";


                using (SqlCommand speakerCommand = new SqlCommand(speakerQuery, connection))
                {
                    // Add parameters for speaker
                    speakerCommand.Parameters.AddWithValue("@ID", newSpeakerID);
                    speakerCommand.Parameters.AddWithValue("@Sp_Name", speakerName);
                    speakerCommand.Parameters.AddWithValue("@Degree", degree);
                    speakerCommand.Parameters.AddWithValue("@Works_at", worksAt);
                    speakerCommand.Parameters.AddWithValue("@Profession", profession);
                    speakerCommand.Parameters.AddWithValue("@ConfID", conferenceID);

                    // Execute the query to insert speaker
                    speakerCommand.ExecuteNonQuery();
                }
            }
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
