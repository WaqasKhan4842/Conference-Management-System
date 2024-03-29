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
    public partial class Manage_Reviewer : Form
    {
        public Manage_Reviewer()
        {
            InitializeComponent();
            dataGridView1.Hide();
            closebtn.Hide();
            Add.Hide();
            Delete.Hide();
            Hide_all();

        }
        private void Hide_all()
        {
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            comboBox1.Hide();
            label5.Hide();
            label4.Hide();
            label1.Hide();
            label9.Hide();
            label2.Hide();
            label3.Hide();
            label6.Hide();
            label7.Hide();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Error: Please enter the reviewer's information.");
                return;
            }
            // Accessing UserInfo class to get AuthorEmail and ConferenceName
            string reviewer_email = textBox1.Text; // Assuming you have a property for Reviewer's email in UserInfo
                                                   // Other data you want to insert
            string reviewer_name = textBox3.Text;
            string reviewer_password = textBox2.Text;
            string expert_field = comboBox1.Text;
            // Retrieve the maximum existing reviewer ID from the database
            int maxId = GetMaxReviewerId();

            // Increment the ID to generate a new one
            int newId = maxId + 1;

            // Construct the new reviewer ID in the format "Rev100"
            string reviewer_id = "Rev" + newId;


            // Check if the reviewer with the same email already exists
            if (IsReviewerAlreadyExists(reviewer_email))
            {
                MessageBox.Show("Error: A reviewer with the same email already exists.");
                return; // You may choose to handle this error differently.
            }

            // SQL INSERT command
            string insertCommand = "INSERT INTO Reviewer (Rev_ID,RevName, email, pass, Expert_Field) " +
                                   "VALUES (@ID, @RevName, @Email, @Pass, @ExpertField)";

            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(insertCommand, connection))
                {
                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@ID", reviewer_id);
                    cmd.Parameters.AddWithValue("@RevName", reviewer_name);
                    cmd.Parameters.AddWithValue("@Email", reviewer_email);
                    cmd.Parameters.AddWithValue("@Pass", reviewer_password);
                    cmd.Parameters.AddWithValue("@ExpertField", expert_field);

                    // Execute the command
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Reviewer data submitted successfully");
                    // You can perform further actions or navigate to other forms as needed
                }
            }
            Dothis();
            Hide_all();
            Add.Hide();
        }

        private bool IsReviewerAlreadyExists(string reviewerEmail)
        {
            // SQL SELECT command to check if the reviewer with the same email already exists
            string selectCommand = "SELECT COUNT(*) FROM Reviewer WHERE email = @Email";

            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(selectCommand, connection))
                {
                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@Email", reviewerEmail);

                    // Execute the command and check the result
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private int GetMaxReviewerId()
        {
            // SQL SELECT command to get the maximum existing reviewer ID
            string selectMaxIdCommand = "SELECT MAX(CAST(SUBSTRING(Rev_ID, 4, LEN(Rev_ID) - 3) AS INT)) FROM Reviewer";

            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(selectMaxIdCommand, connection))
                {
                    // Execute the command and return the result
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        return Convert.ToInt32(result);
                    }

                    return 0; // If there are no existing IDs, start from 0
                }
            }
        }



        private void Delete_Click(object sender, EventArgs e)
        {
            // Check if the email field is empty
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Error: Please enter the reviewer's information.");
                return;
            }
            // Assuming you have a confirmation step before deletion
            DialogResult result = MessageBox.Show("Are you sure you want to delete the reviewer?", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Assuming you have a property for Reviewer's email in UserInfo
                string reviewer_email = textBox1.Text;

                // Check if the reviewer with the specified email exists
                if (!IsReviewerAlreadyExists(reviewer_email))
                {
                    MessageBox.Show("Error: Reviewer not found.");
                    return;
                }

                // SQL DELETE command
                string deleteCommand = "DELETE FROM Reviewer WHERE email = @Email";

                using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(deleteCommand, connection))
                    {
                        // Add parameters to the SQL command
                        cmd.Parameters.AddWithValue("@Email", reviewer_email);

                        // Execute the command
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Reviewer deleted successfully");
                        // You can perform further actions or refresh the DataGridView as needed
                    }
                }
            }
            Dothis();
            show_Delete();
            Hide_all();
            Delete.Hide();

        }

        private void View_Reviewers_Click(object sender, EventArgs e)
        {
            View_Reviewers.Hide();
            panel2.Hide();
            dataGridView1.Show();
            closebtn.Show();
            string selectAllCommand = "SELECT RevName as Name, email as Email, Expert_Field as [Field of Expertise] FROM Reviewer";

            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(selectAllCommand, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            closebtn.Hide();
            panel2.Show();
            View_Reviewers.Show();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            Dothis();
        }
        private void Dothis()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            comboBox1.Text = string.Empty;
            dataGridView1.Hide();
            closebtn.Hide();
            panel2.Show();
            View_Reviewers.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hide_all(); // Hide all controls first
            Add.Hide();
            Delete.Hide();

            string choice = comboBox2.Text;
            switch (choice)
            {
                case "Add":
                    show_Add();
                    break;
                case "Delete":
                    show_Delete();
                    break;
         
            }
          
        }
        private void show_Delete()
        {
            Delete.Show();
            textBox1.Show();
            textBox3.Show();
            label5.Show();
            label4.Show();
            label1.Show();
            label9.Show();

        }
        private void show_Add()
        {
            Add.Show();
            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            comboBox1.Show();
            label5.Show();
            label4.Show();
            label1.Show();
            label9.Show();
            label2.Show();
            label3.Show();
            label6.Show();
            label7.Show();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Hide_all();
        }
    }
}
