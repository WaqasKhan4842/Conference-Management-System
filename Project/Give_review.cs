using System;
using System.Collections;
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
    public partial class Give_review : Form
    {
        private readonly string connectionString = UserInfo.connectionString;
        public Give_review()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            // Assuming you have five CheckedListBox controls named checkedListBox1 to checkedListBox5
            List<CheckedListBox> checkedListBoxes = new List<CheckedListBox>
            {
                checkedListBox1,
                checkedListBox2,
                checkedListBox3,
                checkedListBox4,
                checkedListBox5
            };

            // Assuming you have an abstract ID, replace 123 with the actual abstract ID
            int abstractId = UserInfo.AbstractId;

            // Insert review into the Reviews table
            InsertReview(abstractId, checkedListBoxes);

            MessageBox.Show("Review submitted successfully.");
        }

        private void InsertReview(int absId, List<CheckedListBox> checkedListBoxes)
        {
            int abstractId = UserInfo.AbstractId;

            int nextReviewId = GetNextReviewId();
            string reviewerId = UserInfo.userid;
            int suggestedReviewerId = GetSuggestedReviewerId(reviewerId);
            int score = GetTotalScore(checkedListBoxes);
            int currentAbstractScore;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Query to get the current score of the abstract
                string query = "SELECT score FROM Abstracts WHERE Id = @AbsId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AbsId", absId);

                    // Execute the query
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        currentAbstractScore = Convert.ToInt32(result);
                    }
                    else
                    {
                        currentAbstractScore = 0; // Default score if no result is found
                    }
                }

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertReviewQuery = "INSERT INTO Reviews (review_id, score, suggested_reviewer_id, abs_ID) " +
                                                   "VALUES (@ReviewId, @Score, @SuggestedReviewerId, @AbsId)";

                        using (SqlCommand command = new SqlCommand(insertReviewQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ReviewId", nextReviewId);
                            command.Parameters.AddWithValue("@Score", score);
                            command.Parameters.AddWithValue("@SuggestedReviewerId", suggestedReviewerId);
                            command.Parameters.AddWithValue("@AbsId", absId);

                            command.ExecuteNonQuery();
                        }

                        string updateRevStatusQuery = "UPDATE Suggested_Reviewer SET rev_status = 'Done' WHERE sr_ID = @SuggestedReviewerId";

                        using (SqlCommand updateCommand = new SqlCommand(updateRevStatusQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@SuggestedReviewerId", suggestedReviewerId);
                            updateCommand.ExecuteNonQuery();
                        }

                        string updateAbstractScoreQuery = "UPDATE Abstracts SET score = score + @Score WHERE Id = @AbsId";

                        using (SqlCommand updateScoreCommand = new SqlCommand(updateAbstractScoreQuery, connection, transaction))
                        {
                            updateScoreCommand.Parameters.AddWithValue("@Score", score);
                            updateScoreCommand.Parameters.AddWithValue("@AbsId", absId);
                            updateScoreCommand.ExecuteNonQuery();
                        }
                        score += currentAbstractScore;

                        if (score == 75)
                        {
                            Console.WriteLine("Score is 75. Inserting into Accepted_Abstracts.");

                            string insertAcceptedAbstractQuery = "INSERT INTO Accepted_Abstracts (abs_ID) VALUES (@AbsId)";

                            using (SqlCommand insertAcceptedCommand = new SqlCommand(insertAcceptedAbstractQuery, connection, transaction))
                            {
                                insertAcceptedCommand.Parameters.AddWithValue("@AbsId", absId);
                                int rowsAffected = insertAcceptedCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine("Inserted into Accepted_Abstracts successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to insert into Accepted_Abstracts.");
                                }
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Review submitted successfully.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }


        private int GetTotalScore(List<CheckedListBox> checkedListBoxes)
        {
            int totalScore = 0;

            // Calculate the total score based on checked items in all CheckedListBox controls
            for (int i = 0; i < checkedListBoxes.Count; i++)
            {
                totalScore += GetCheckedListBoxScore(checkedListBoxes[i]);
            }

            return totalScore;
        }

        private int GetCheckedListBoxScore(CheckedListBox checkedListBox)
        {
            int score = 0;

            // Calculate the score based on checked items and their values
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (checkedListBox.GetItemChecked(i))
                {
                    // Assuming each checkbox value is equal to its index + 1
                    score += i + 1;
                }
            }

            return score;
        }

        private int GetNextReviewId()
        {
            int nextReviewId = 1;

          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to get the max review_id
                string query = "SELECT ISNULL(MAX(review_id), 0) + 1 FROM Reviews";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();

                    nextReviewId = Convert.ToInt32(result);
                }
            }

            return nextReviewId;
        }

        private int GetSuggestedReviewerId(string reviewerId)
        {
            int suggestedReviewerId = -1;

          

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to get the suggested_reviewer_id from UserInfo.userid
                string query = "SELECT sr_ID FROM Suggested_Reviewer WHERE Rev_ID = @ReviewerId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter
                    command.Parameters.AddWithValue("@ReviewerId", reviewerId);

                    // Execute the query
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        suggestedReviewerId = Convert.ToInt32(result);
                    }
                }
            }

            return suggestedReviewerId;
        }
    }
}
