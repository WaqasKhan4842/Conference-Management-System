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
    public partial class View_Reviewer : Form
    {
        private string connectionString = UserInfo.connectionString;
        public View_Reviewer()
        {
            InitializeComponent();
            Hide();
        }
        private void Hide()
        {
            dataGridView1.Hide();
            closebtn.Hide();
        }

        private void View_Reviewers_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Please Enter the Field");
                return;
            }
            else
            {
                // Clear existing rows in the DataGridView
                dataGridView1.Rows.Clear();
                View_Reviewers.Hide();
                panel2.Hide();
                dataGridView1.Show();
                closebtn.Show();
                // Add columns to the DataGridView if not already added
                if (dataGridView1.Columns.Count == 0)
                {
                    dataGridView1.Columns.Add("Rev_ID", "ID");
                    dataGridView1.Columns.Add("RevName", "Name");
                    dataGridView1.Columns.Add("Expert_Field", "Field");
                }
                // Create a SqlConnection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQL query to retrieve Rev_ID, RevName, and Expert_Field from the Reviewer table
                    string query = "SELECT Rev_ID , RevName , Expert_Field FROM Reviewer " +
                        "WHERE Expert_Field = @field";

                    // Create a SqlCommand with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@field", comboBox1.Text);
                        // Open the connection
                        connection.Open();

                        // Create a SqlDataReader to read the data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are rows in the result set
                            if (reader.HasRows)
                            {
                                // Iterate through the rows and add them to the DataGridView
                                while (reader.Read())
                                {
                                    string revId = reader["Rev_ID"].ToString();
                                    string revName = reader["RevName"].ToString();
                                    string expertField = reader["Expert_Field"].ToString();

                                    // Add a new row to the DataGridView
                                    dataGridView1.Rows.Add(revId, revName, expertField);
                                }
                            }
                            else
                            {
                                // No rows in the result set
                                MessageBox.Show("No data found.");
                            }
                        }

                    }
                }
            }
        }


        private void closebtn_Click(object sender, EventArgs e)
        {
            View_Reviewers.Show();
            panel2.Show();
            dataGridView1.Hide();
            closebtn.Hide();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is not the header
            if (e.RowIndex >= 0)
            {
                // Get the data from the clicked row
                label5.Text = dataGridView1.Rows[e.RowIndex].Cells["Rev_ID"].Value.ToString();
                label6.Text = dataGridView1.Rows[e.RowIndex].Cells["RevName"].Value.ToString();
                label7.Text = dataGridView1.Rows[e.RowIndex].Cells["Expert_Field"].Value.ToString();

                // Create a SqlConnection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQL query to find the number of reviews given by a reviewer with a specific ID
                    string reviewsQuery = @"SELECT COUNT(*) AS NumberOfReviewedAbstracts
                                              FROM Suggested_Reviewer sr
                                              INNER JOIN Reviewer r ON sr.Rev_ID = r.Rev_ID
                                              WHERE r.Rev_ID = @reviewerId AND sr.rev_status = 'Done'";

                    // SQL query to find the number of abstracts assigned to the reviewer with a specific ID
                    string assignedAbstractsQuery = @"SELECT COUNT(*) AS NumberOfAssignedAbstracts
                                              FROM Suggested_Reviewer sr
                                              INNER JOIN Reviewer r ON sr.Rev_ID = r.Rev_ID
                                              WHERE r.Rev_ID = @reviewerId AND sr.rev_status = 'Assigned'";

                    // Create a SqlCommand with the query and connection for reviews
                    using (SqlCommand reviewsCommand = new SqlCommand(reviewsQuery, connection))
                    {
                        // Add parameters to the reviews query
                        reviewsCommand.Parameters.AddWithValue("@reviewerId", label5.Text);

                        // Open the connection
                        connection.Open();

                        // Execute the reviews query and get the result
                        object reviewsResult = reviewsCommand.ExecuteScalar();

                        // Check if the result is not null
                        if (reviewsResult != null)
                        {
                            label8.Text = reviewsResult.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No reviews found.");
                        }
                    }

                    // Create a SqlCommand with the query and connection for assigned abstracts
                    using (SqlCommand assignedAbstractsCommand = new SqlCommand(assignedAbstractsQuery, connection))
                    {
                        // Add parameters to the assigned abstracts query
                        assignedAbstractsCommand.Parameters.AddWithValue("@reviewerId", label5.Text);

                        // Execute the assigned abstracts query and get the result
                        object assignedAbstractsResult = assignedAbstractsCommand.ExecuteScalar();

                        // Check if the result is not null
                        if (assignedAbstractsResult != null)
                        {
                            label11.Text = assignedAbstractsResult.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No assigned abstracts found.");
                        }
                    }
                }
            }
        }

        private void Assign_Click(object sender, EventArgs e)
        {
            string reviewerId = label5.Text;
            int abstractId = UserInfo.AbstractId;
            string revStatus = "Assigned";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the assignment already exists
                string checkAssignmentQuery = "SELECT COUNT(*) FROM Suggested_Reviewer WHERE Rev_ID = @reviewerId AND abs_ID = @abstractId";
                using (SqlCommand checkAssignmentCommand = new SqlCommand(checkAssignmentQuery, connection))
                {
                    checkAssignmentCommand.Parameters.AddWithValue("@reviewerId", reviewerId);
                    checkAssignmentCommand.Parameters.AddWithValue("@abstractId", abstractId);

                    int existingAssignments = Convert.ToInt32(checkAssignmentCommand.ExecuteScalar());

                    if (existingAssignments > 0)
                    {
                        MessageBox.Show("This reviewer is already assigned to the selected abstract.");
                        return;
                    }
                }

                // Continue with the assignment process

                string countReviewersQuery = "SELECT COUNT(*) FROM Suggested_Reviewer WHERE abs_ID = @abstractId GROUP BY abs_ID HAVING COUNT(*) >= 3";
                using (SqlCommand countReviewersCommand = new SqlCommand(countReviewersQuery, connection))
                {
                    countReviewersCommand.Parameters.AddWithValue("@abstractId", abstractId);

                    object result = countReviewersCommand.ExecuteScalar();

                    if (result != null)
                    {
                        MessageBox.Show("Cannot assign more than 3 reviewers to this abstract.");
                        return;
                    }

                    // Continue with the assignment process

                    string maxSrIdQuery = "SELECT MAX(sr_ID) FROM Suggested_Reviewer";
                    using (SqlCommand maxSrIdCommand = new SqlCommand(maxSrIdQuery, connection))
                    {
                        object maxSrIdObj = maxSrIdCommand.ExecuteScalar();
                        int newSrId = (maxSrIdObj == DBNull.Value) ? 1 : Convert.ToInt32(maxSrIdObj) + 1;

                        string insertQuery = @"INSERT INTO Suggested_Reviewer (sr_ID, Deadline, Rev_ID, abs_ID, rev_status)
                                        VALUES (@newSrId, DATEADD(day, -3, (SELECT con.con_Date 
                                                                        FROM Abstracts abs 
                                                                        INNER JOIN Conference con ON abs.con_ID = con.ID 
                                                                        WHERE abs.Id = @abstractId)),
                                                @reviewerId, @abstractId, @revStatus)";

                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@newSrId", newSrId);
                            insertCommand.Parameters.AddWithValue("@reviewerId", reviewerId);
                            insertCommand.Parameters.AddWithValue("@abstractId", abstractId);
                            insertCommand.Parameters.AddWithValue("@revStatus", revStatus);

                            int rowsAffected = insertCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Assignment successful! New sr_ID: {newSrId}");
                            }
                            else
                            {
                                MessageBox.Show("Assignment failed.");
                            }
                        }
                    }
                }
            }
        }

    }

 }

