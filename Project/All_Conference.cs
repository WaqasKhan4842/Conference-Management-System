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

// for timer
using System;

namespace Project
{
    public partial class All_Conferences : Form
    {
        private int currentConferenceIndex = 0;
     
        public All_Conferences()
        {
            InitializeComponent();
            LoadConferenceDetails();
           

            // Start the timer
            conferenceTimer.Start();
        }
        private void LoadConferenceDetails()
        {
            pictureBox11.Hide();
            pictureBox12.Hide();
            Speaker_1.Hide();
            speaker_2.Hide();
            Name1.Text = "";
            degree.Text = "";
            works_at.Text = "";
            profession.Text = "";
            name_2.Text = "";
            degree_2.Text = "";
            works_at2.Text = "";
            profession2.Text = "";
            string connectionString = UserInfo.connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM ConferenceWithOrganizersView";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Move to the record corresponding to the current index
                        for (int i = 0; i <= currentConferenceIndex && reader.Read(); i++)
                        {

                            if (i == currentConferenceIndex)
                            {
                                label18.Text = reader["con_Name"].ToString();
                                UserInfo.ConferenceID = reader["ID"].ToString();
                                label5.Text = Convert.ToDateTime(reader["con_Date"]).ToString("yyyy-MM-dd");
                                label3.Text = reader["con_location"].ToString();
                                label8.Text = Convert.ToDateTime(reader["reg_start_date"]).ToString("yyyy-MM-dd");
                                label10.Text = Convert.ToDateTime(reader["reg_end_date"]).ToString("yyyy-MM-dd");
                                label13.Text = Convert.ToDateTime(reader["sub_start_date"]).ToString("yyyy-MM-dd");
                                label11.Text = Convert.ToDateTime(reader["sub_end_date"]).ToString("yyyy-MM-dd");
                                string type = reader["con_type"].ToString();
                                textBox1.Text = reader["about"].ToString();
                                label17.Text = reader["org_name"].ToString();

                                // Check the number of speakers for the current conference
                                int speakerCount = GetSpeakerCountForConference(UserInfo.ConferenceID);

                                if (speakerCount == 0)
                                {
                                    // Hide labels and images for speakers
                                    pictureBox11.Hide();
                                    pictureBox12.Hide();
                                    Speaker_1.Hide();
                                    speaker_2.Hide();
                                    Name1.Text = "";
                                    degree.Text = "";
                                    works_at.Text = "";
                                    profession.Text = "";
                                    name_2.Text = "";
                                    degree_2.Text = "";
                                    works_at2.Text = "";
                                    profession2.Text = "";
                                }
                                else if (speakerCount == 1)
                                {
                                    // Display information for the first speaker
                                   // DisplaySpeakerInformation(UserInfo.ConferenceID, 1);
                                }
                                else if (speakerCount >= 2)
                                {
                                    // Display information for both speakers
                                    //DisplaySpeakerInformation(UserInfo.ConferenceID, 1);
                                   // DisplaySpeakerInformation(UserInfo.ConferenceID, 2);
                                }
                                switch (type)
                                {
                                    case "Medical Research Conference":
                                        pictureBox5.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\m1.jpg");
                                        pictureBox6.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\m2.jpg");
                                        pictureBox8.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\m3.jpg");
                                        pictureBox9.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\m4.jpg");
                                        pictureBox10.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\m5.jpg");
                                        break;
                                    case "Environmental Science Conference":
                                        pictureBox5.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\e1.jpg");
                                        pictureBox6.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\e2.jpg");
                                        pictureBox8.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\e3.jpg");
                                        pictureBox9.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\e4.jpg");
                                        pictureBox10.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\e5.jpg");
                                        // Handle other conference types
                                        break;
                                    case "Educational Research Conference":
                                        pictureBox5.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s1.jpg");
                                        pictureBox6.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s2.jpg");
                                        pictureBox8.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s3.jpg");
                                        pictureBox9.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s4.jpg");
                                        pictureBox10.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s5.jpg");
                                        // Handle other conference types
                                        break;
                                    case "Social Sciences Research Conference":
                                        pictureBox5.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss1.jpg");
                                        pictureBox6.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss2.jpg");
                                        pictureBox8.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss3.jpg");
                                        pictureBox9.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss4.jpg");
                                        pictureBox10.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss5.jpg");
                                        // Handle other conference types
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
        private int GetSpeakerCountForConference(string conferenceID)
        {
            int speakerCount = 0;

            // Retrieve the count of speakers for the current conference from the database
            string connectionString = UserInfo.connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Speakers WHERE conf_ID = @ConferenceID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ConferenceID", conferenceID);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        speakerCount = (int)result;
                    }
                }
            }

            return speakerCount;
        }
        private void DisplaySpeakerInformation(string conferenceID, int speakerNumber)
        {
            string connectionString = UserInfo.connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM Speakers WHERE conf_ID = @ConferenceID AND ID = @SpeakerNumber";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ConferenceID", conferenceID);
                    command.Parameters.AddWithValue("@SpeakerNumber", speakerNumber);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Display information for the speaker based on the speakerNumber
                            if (speakerNumber == 1)
                            {
                                pictureBox11.Show();
                                Speaker_1.Show();
                                Name1.Text = reader["Sp_Name"].ToString();
                                degree.Text = reader["Degree"].ToString();
                                works_at.Text = reader["Works_at"].ToString();
                                profession.Text = reader["Profession"].ToString();
                            }
                            else if (speakerNumber == 2)
                            {
                                pictureBox12.Show();
                                speaker_2.Show();
                                name_2.Text = reader["Sp_Name"].ToString();
                                degree_2.Text = reader["Degree"].ToString();
                                works_at2.Text = reader["Works_at"].ToString();
                                profession2.Text = reader["Profession"].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentConferenceIndex++;

            // Load details for the next conference
            LoadConferenceDetails();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentConferenceIndex > 0)
            {
                currentConferenceIndex--;

                // Load details for the previous conference
                LoadConferenceDetails();
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Register_Click(object sender, EventArgs e)
        {
            // Check the count of attendees for the current conference
            

            if (GetCurrentCapacity() >= GetConferenceCapacity())  // Replace 0 with the actual capacity of the conference
            {
                // Display a message indicating that the conference is full
                MessageBox.Show("Sorry, the conference is full. Registration is closed.", "Registration Closed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Open the Registered_Al_Attendee form
                Registered_Al_Attendee ra = new Registered_Al_Attendee();
                ra.Show();
            }
        }

        private int GetConferenceCapacity()
        {
            int currentCapacity = 0;

            // Retrieve the conference capacity from the database based on the currentConferenceIndex
            string connectionString = UserInfo.connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT capacity FROM Conference WHERE ID = @ConferenceID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ConferenceID", UserInfo.ConferenceID);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        currentCapacity = (int)result;
                    }
                }
            }

            return currentCapacity;
        }
        private int GetCurrentCapacity()
        {
            int currentCapacity = 0;

            // Retrieve the count of attendees for the current conference from the database based on the currentConferenceIndex
            string connectionString = UserInfo.connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM At_Conference WHERE con_ID = @ConferenceID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ConferenceID", UserInfo.ConferenceID);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        currentCapacity = (int)result;
                    }
                }
            }

            return currentCapacity;
        }


        private void Abstract_Click(object sender, EventArgs e)
        {
            Registered_Al_Authors ral = new Registered_Al_Authors();
            ral.Show();
        }

        private void conferenceTimer_Tick(object sender, EventArgs e)
        {
            // Check the remaining time until the conference date and update the UI
            DateTime conferenceDate = Convert.ToDateTime(label5.Text); // Assuming label5 is the label displaying con_date
            TimeSpan timeRemaining = conferenceDate - DateTime.Now;

            // Update the UI with the remaining time
            labelTimer.Text = $"{timeRemaining.Days} days, {timeRemaining.Hours} hours, {timeRemaining.Minutes} minutes, {timeRemaining.Seconds} seconds";

            // Optionally, you can perform additional actions based on the remaining time
            if (timeRemaining.TotalSeconds <= 0)
            {
                // Conference date has passed, stop the timer or perform other actions
                conferenceTimer.Stop();
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            this.Close();
            HomePage pg = new HomePage();
            pg.Show();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
