using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project
{
    public partial class Assigned_Abstracts : Form
    {
        string connectionString = UserInfo.connectionString;
        DataTable dataTable; // Declare the DataTable at the class level

        // Variables to store clicked row values
        private string selectedRevStatus;
        private DateTime selectedDeadline;
        private string selectedTitle;
        // Add more variables for other values you want to capture

        public Assigned_Abstracts()
        {
            InitializeComponent();
            dataTable = new DataTable(); // Initialize the DataTable
            DisplayAll();

 
        }

        private void DisplayAll()
        {
            give_review.Show();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Write your SQL query
                string query = "SELECT SR.rev_Status, SR.Deadline, A.Id AS abs_ID, A.title, A.link, A.abs_Status, A.score, A.sub_Date, C.con_Name, C.about, O.org_name, AU.AutName, AU.email AS AuthorEmail, AU.profession, AU.works_at, AU.Degree, AU.Field " +
                    "FROM Suggested_Reviewer SR " +
                    "JOIN Abstracts A ON SR.abs_ID = A.Id " +
                    "JOIN Conference C ON A.con_ID = C.ID " +
                    "JOIN Organizers O ON C.org_ID = O.org_ID " +
                    "JOIN Authors AU ON A.auth_id = AU.ID " +
                    "WHERE SR.Rev_ID = @RevID AND SR.rev_Status = @RevStatus"; 


                MessageBox.Show(UserInfo.choice);

                // Open the connection
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query to avoid SQL injection
                    command.Parameters.AddWithValue("@RevID", UserInfo.userid);
                    command.Parameters.AddWithValue("@RevStatus", UserInfo.choice);
                    if (UserInfo.choice == "Done")
                        give_review.Hide();


                    // Create a DataAdapter to execute the query and fill the DataTable
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }
            }

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;
            
           
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is not the header row
            if (e.RowIndex >= 0)
            {
                // Access the values in the clicked row and assign them to variables
                selectedRevStatus = dataGridView1.Rows[e.RowIndex].Cells["rev_Status"].Value.ToString();
                selectedDeadline = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Deadline"].Value);
                selectedTitle = dataGridView1.Rows[e.RowIndex].Cells["title"].Value.ToString();              

                // Update labels with the selected values
                label1.Text = $"{selectedDeadline.ToShortDateString()}";
                label2.Text = $"{selectedRevStatus}";
                label3.Text = $"{dataGridView1.Rows[e.RowIndex].Cells["abs_ID"].Value.ToString()}";
                if (int.TryParse(label3.Text, out int abstractId))
                {
                    UserInfo.AbstractId = abstractId;
                }
                label4.Text = selectedRevStatus.ToString();
                label5.Text = $"{selectedTitle}";
                linkLabel1.Text = $"{dataGridView1.Rows[e.RowIndex].Cells["link"].Value.ToString()}";
                label6.Text = $"{dataGridView1.Rows[e.RowIndex].Cells["score"].Value.ToString()}";
                label7.Text = $"{dataGridView1.Rows[e.RowIndex].Cells["AutName"].Value.ToString()}";
                label8.Text = $"{dataGridView1.Rows[e.RowIndex].Cells["AuthorEmail"].Value.ToString()}";
                label9.Text = $"{dataGridView1.Rows[e.RowIndex].Cells["con_Name"].Value.ToString()}";
                label10.Text = $"{dataGridView1.Rows[e.RowIndex].Cells["org_name"].Value.ToString()}";
            }
        }


        private void Assigned_Abstracts_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {

        }

        private void give_review_Click(object sender, EventArgs e)
        {
            Give_review give_Review = new Give_review();
            give_Review.Show();
        }
    }
}
