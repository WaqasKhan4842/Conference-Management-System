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

namespace Project
{
    public partial class Reviewer_Window : Form
    {
        string connectionString = UserInfo.connectionString;
        public Reviewer_Window()
        {
            InitializeComponent();
            Find_ID();
            Display_Info();

        }
        private void Find_ID()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Write your SQL query
                string query = "SELECT Rev_ID FROM Reviewer WHERE email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query to avoid SQL injection
                    command.Parameters.AddWithValue("@Email", UserInfo.Email);

                    // Execute the query
                    object result = command.ExecuteScalar();

                    
                        string reviewerId = result.ToString();
                        UserInfo.userid = reviewerId;
                    }
                    
            }
        }

        private void Reviewer_Window_Load(object sender, EventArgs e)
        {

        }
        private void Display_Info()
        {
            string email = UserInfo.Email;

            // Create a SqlConnection using the connection string
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to find the number of reviews given by a reviewer and the number of assigned abstracts
                string infoQuery = @"SELECT 
                                COUNT(DISTINCT CASE WHEN sr.rev_status = 'Done' THEN sr.sr_ID END) AS NumberOfReviews,
                                COUNT(DISTINCT CASE WHEN sr.rev_status = 'Assigned' THEN sr.sr_ID END) AS NumberOfAssignedAbstracts
                            FROM Suggested_Reviewer sr
                            INNER JOIN Reviewer r ON sr.Rev_ID = r.Rev_ID
                            WHERE r.email = @email";

                // Create a SqlCommand with the query and connection
                using (SqlCommand infoCommand = new SqlCommand(infoQuery, connection))
                {
                    // Add parameters to the query
                    infoCommand.Parameters.AddWithValue("@email", email);

                    // Open the connection
                    connection.Open();

                    // Execute the query and get the result
                    using (SqlDataReader reader = infoCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Get the values from the result
                            int numberOfReviews = Convert.ToInt32(reader["NumberOfReviews"]);
                            int numberOfAssignedAbstracts = Convert.ToInt32(reader["NumberOfAssignedAbstracts"]);

                            // Update the labels with the obtained values
                            label4.Text = numberOfReviews.ToString();
                            label3.Text = numberOfAssignedAbstracts.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Reviewer not found.");
                        }
                    }
                }
            }
        }


        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (panel3.Controls.Count > 0)
            {
                // You can either remove the existing child form
                panel3.Controls.RemoveAt(0);

                // Or hide the existing child form
                //DesktopPage.Controls[0].Hide();
            }
            // Set properties of the ChildForm
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Add the ChildForm to the panel on the MainForm
            panel3.Controls.Add(childForm);
            panel3.Controls.Add(childForm);
            panel3.Controls.Add(childForm);
            panel3.Controls.Add(childForm);
            // Show the ChildForm
            childForm.Show();
        }

        private void Open_Click_1(object sender, EventArgs e)
        {
            
           
          
          

        }

        private void Open_Click(object sender, EventArgs e)
        {
            string which_abs = comboBox2.Text;
        

            // Check if the choice is not null or empty before proceeding
            if (!string.IsNullOrEmpty(which_abs))
            {
                
                UserInfo.choice = which_abs;
                MessageBox.Show(which_abs);
                Assigned_Abstracts form = new Assigned_Abstracts();
                OpenChildForm(form, e);
            }
            else
            {
                MessageBox.Show("Please select a valid choice from the combo box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage pg = new HomePage();
            pg.Show();
        }
    }
}
