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
    public partial class Submit_Abstract : Form
    {
        public Submit_Abstract()
        {
            InitializeComponent();
        }

        private void Submit_Abstratc_Click(object sender, EventArgs e)
        {
            All_Conferences ac = new All_Conferences();
            string con_id = UserInfo.ConferenceID;
            string author_id = UserInfo.AuthorID;
            string title = textBox3.Text;
            string link = textBox1.Text;

            // Validate if title and link are not empty
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(link))
            {
                MessageBox.Show("Please enter both title and link.");
                return;
            }

            DateTime sub_Date = DateTime.Now; // Current date
            string abs_Status = "Pending"; // Initial status is pending
            int score = 0; // Initial score

            string connectionString = UserInfo.connectionString;
            // Replace the connection string with your actual SQL Server connection string
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the author has already submitted an abstract for the conference
                if (AbstractExists(connection, author_id, con_id))
                {
                    MessageBox.Show("An abstract with the same title and author already exists.");
                    return;
                }

                // Generate a new abstract ID
                int newId = GetNextAbstractId(connection);

                // SQL INSERT command
                string insertCommand = "INSERT INTO Abstracts (Id, Title, Link, sub_Date, auth_id, abs_Status, con_ID, score) " +
                                       "VALUES (@Id, @Title, @Link, @sub_Date, @auth_id, @abs_Status, @con_ID, @score)";

                using (SqlCommand cmd = new SqlCommand(insertCommand, connection))
                {
                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@Id", newId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Link", link);
                    cmd.Parameters.AddWithValue("@sub_Date", sub_Date);
                    cmd.Parameters.AddWithValue("@auth_id", author_id);
                    cmd.Parameters.AddWithValue("@abs_Status", abs_Status);
                    cmd.Parameters.AddWithValue("@con_ID", con_id);
                    cmd.Parameters.AddWithValue("@score", score);

                    // Execute the command
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Abstract submitted successfully.");
                }
            }
        }


        private bool AbstractExists(SqlConnection connection, string auth_id, string title)
        {
            // SQL SELECT command to check if the abstract already exists
            string selectCommand = "SELECT COUNT(*) FROM Abstracts WHERE auth_id = @Auth_ID AND title = @Title";

            using (SqlCommand cmd = new SqlCommand(selectCommand, connection))
            {
                // Add parameters to the SQL command
                cmd.Parameters.AddWithValue("@Auth_ID", auth_id);
                cmd.Parameters.AddWithValue("@Title", title);

                // Execute the command and check the result
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        private int GetNextAbstractId(SqlConnection connection)
        {
            // SQL SELECT command to get the next maximum abstract ID
            string selectCommand = "SELECT ISNULL(MAX(Id), 99) + 1 FROM Abstracts";

            using (SqlCommand command = new SqlCommand(selectCommand, connection))
            {
                object result = command.ExecuteScalar();

                int lastId = result == DBNull.Value ? 100 : (int)result;

                // Increment the last maximum ID to get the new abstract ID
                return lastId;
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
