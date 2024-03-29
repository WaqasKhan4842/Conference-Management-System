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
    
    public partial class View_Abstratcs : Form
    {
        private int currentAbstractIndex = 0;
        public View_Abstratcs()
        {
            InitializeComponent();
            display_Abstract_Details();

        }
        private void display_Abstract_Details()
        {
            string connectionString = UserInfo.connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM AbstractsWithAuthorsForConferenceView " +
                                   "WHERE con_ID = @conference_ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@conference_ID", UserInfo.ConferenceID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Move to the record corresponding to the current index
                            for (int i = 0; i <= currentAbstractIndex && reader.Read(); i++)
                            {
                                if (i == currentAbstractIndex)
                                {
                                    int id = Convert.ToInt32(reader["abstractID"]);
                                    UserInfo.AbstractId = id;
                                    // Load abstract details
                                    label1.Text = reader["title"].ToString();
                                    linkLabel1.Text = reader["link"].ToString();
                                    label18.Text = reader["sub_Date"].ToString();
                                    label17.Text = reader["abs_Status"].ToString();
                                  //  label16.Text = reader["score"].ToString();

                                    // Load author details
                                    label7.Text = reader["AutName"].ToString();
                                    label19.Text = reader["AuthorEmail"].ToString();
                                    label8.Text = reader["profession"].ToString();
                                    label9.Text = reader["works_at"].ToString();
                                    label10.Text = reader["Degree"].ToString();
                                    label11.Text = reader["Field"].ToString();
                                    DisplayReviewersForAbstract(id);
                                }
                            }
                        }
                    }
                }

            }
        

        private void Assign_Reviewer_Click(object sender, EventArgs e)
        {
            View_Reviewer vr = new View_Reviewer();
            vr.Show();
        }
        private void DisplayReviewersForAbstract(int abstractId)
        {
            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                string query = "SELECT R.RevName " +
                               "FROM Reviewer R " +
                               "JOIN Suggested_Reviewer SR ON R.Rev_ID = SR.Rev_ID " +
                               "JOIN Abstracts A ON SR.abs_ID = A.Id " +
                               "WHERE A.Id = @AbstractID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AbstractID", abstractId);

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable resultDataTable = new DataTable();
                        dataAdapter.Fill(resultDataTable);

                        // Bind the DataTable to the DataGridView
                        r.DataSource = resultDataTable;
                    }
                }
            }
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open the link in the default web browser
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            currentAbstractIndex++;

            // Load details for the next abstract
            display_Abstract_Details();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentAbstractIndex > 0)
            {
                currentAbstractIndex--;

                // Load details for the previous abstract
                display_Abstract_Details();
            }
        }
    }
}
