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
    public partial class Admin_View_Conferences : Form
    {
        private int currentConferenceIndex = 0;
        string connectionString = UserInfo.connectionString;
        public Admin_View_Conferences()
        {
            InitializeComponent();
            LoadConferenceDetails();
        }
        private void LoadConferenceDetails()
        {
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT 
                            con.*, 
                            (SELECT COUNT(*) FROM Abstracts WHERE con_ID = con.ID) AS AbstractCount 
                        FROM Conference con";

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

                                // Retrieve the abstract count from the subquery
                                int abstractCount = Convert.ToInt32(reader["AbstractCount"]);
                                no_abstracts.Text = $"{abstractCount}";

                                // Update the rest of the labels with conference details
                                label5.Text = Convert.ToDateTime(reader["con_Date"]).ToString("yyyy-MM-dd");
                                label3.Text = reader["con_location"].ToString();
                                label8.Text = Convert.ToDateTime(reader["reg_start_date"]).ToString("yyyy-MM-dd");
                                label10.Text = Convert.ToDateTime(reader["reg_end_date"]).ToString("yyyy-MM-dd");
                                label13.Text = Convert.ToDateTime(reader["sub_start_date"]).ToString("yyyy-MM-dd");
                                label11.Text = Convert.ToDateTime(reader["sub_end_date"]).ToString("yyyy-MM-dd");
                                string type = reader["con_type"].ToString();
                                label21.Text = reader["capacity"].ToString();
                                // Display the count of attendees for the current conference
                                label15.Text = GetAttendeeCount(reader["ID"].ToString()).ToString();



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
                                      
                                        break;
                                    case "Educational Research Conference":
                                        pictureBox5.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s1.jpg");
                                        pictureBox6.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s2.jpg");
                                        pictureBox8.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s3.jpg");
                                        pictureBox9.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s4.jpg");
                                        pictureBox10.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\s5.jpg");
                                      
                                        break;
                                    case "Social Sciences Research Conference":
                                        pictureBox5.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss1.jpg");
                                        pictureBox6.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss2.jpg");
                                        pictureBox8.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss3.jpg");
                                        pictureBox9.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss4.jpg");
                                        pictureBox10.Image = Image.FromFile("C:\\Users\\DELL\\Desktop\\Temp\\photos\\ss5.jpg");
                                   
                                        break;
                                }

                            }
                        }
                    }
                }
            }
        }
        private int GetAttendeeCount(string conferenceID)
        {
            int count = 0;
            string attendeeCountQuery = $"SELECT COUNT(*) FROM At_Conference WHERE con_ID = '{conferenceID}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(attendeeCountQuery, connection))
                {
                    count = (int)command.ExecuteScalar();
                }
            }

            return count;
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

        private void View_Abstracts_Click(object sender, EventArgs e)
        {
            View_Abstratcs va = new View_Abstratcs();
            va.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Admin_View_Conferences_Load(object sender, EventArgs e)
        {

        }
    }
}
