using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project
{
    public partial class New_Authors : Form
    {
        private readonly string connectionString = UserInfo.connectionString;
        public New_Authors()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Generate a new ID
                string newId = GenerateNewId(connection);
                // Validate input fields
                if (string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(password.Text))
                {
                    MessageBox.Show("Please fill the required fields.");
                    return;
                }

                // Check if the email already exists
                if (IsEmailExists(connection, email.Text))
                {
                    MessageBox.Show("Email Already Exists");
                    return;
                }

                // SQL INSERT command
                string insertCommand = "INSERT INTO Authors (ID, AutName, email, pass, profession, works_at, Degree, Field) " +
                                       "VALUES (@ID, @AutName, @Email, @Pass, @Profession, @WorksAt, @Degree, @Field)";

                using (SqlCommand cmd = new SqlCommand(insertCommand, connection))
                {
                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@ID", newId);
                    cmd.Parameters.AddWithValue("@AutName", Name.Text);
                    cmd.Parameters.AddWithValue("@Email", email.Text);
                    cmd.Parameters.AddWithValue("@Pass", password.Text);
                    cmd.Parameters.AddWithValue("@Profession", profession.Text);
                    cmd.Parameters.AddWithValue("@WorksAt", works_at.Text);
                    cmd.Parameters.AddWithValue("@Degree", Degree.Text);
                    cmd.Parameters.AddWithValue("@Field", Field.Text);

                    // Execute the command
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Author inserted successfully.");

                    // Optionally, you can open another form or perform additional actions here
                    Registered_Al_Authors ral = new Registered_Al_Authors();
                    ral.Show();
                    this.Hide();
                }
            }
        }

        private string GenerateNewId(SqlConnection connection)
        {
                // Query to get the last maximum ID from the Authors table
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(ID, 4, LEN(ID) - 3) AS INT)), 99) + 1 FROM Authors";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();

                    int lastId = result == DBNull.Value ? 100 : (int)result;

                    // Generate a new ID by incrementing the last maximum ID
                    return $"Aut{lastId:D3}";
                }

        }

        private bool IsEmailExists(SqlConnection connection, string email)
        {
            // SQL SELECT command to check if the email already exists
            string selectCommand = "SELECT COUNT(*) FROM Authors WHERE email = @Email";

            using (SqlCommand cmd = new SqlCommand(selectCommand, connection))
            {
                // Add parameter to the SQL command
                cmd.Parameters.AddWithValue("@Email", email);

                // Execute the command and check the result
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
