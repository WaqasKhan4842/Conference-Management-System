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
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string val = comboBox1.Text;
            switch (val)
            {
                case "Conference Having More than 2 Accepted Abstracts":
                    case1();
                    break;
                case "Average Score of Reviews of Each Author":
                    case2();
                    break; ;
                case "Average Abstract Score of All Conference":
                    case3();
                    break;
                case "Authors Having Submitted Abstracts For 1 Conferernces":
                    case4();
                    break;

            }

        }
        private void case1()
        {
            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM ConferenceAverageScores"; // Assuming you named your view as ConferenceAverageScores

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable resultDataTable = new DataTable();
                        dataAdapter.Fill(resultDataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = resultDataTable;
                    }
                }
            }
        }
        private void case2()
        {
            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM AuthorReviewSummaryView"; // Assuming you named your view as ConferenceAverageScores

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable resultDataTable = new DataTable();
                        dataAdapter.Fill(resultDataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = resultDataTable;
                    }
                }
            }
        }
        private void case3()
        {
            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                string query = "SELECT C.ID AS ConferenceID, C.con_Name AS ConferenceName,  COUNT(AA.abs_ID) AS AcceptedAbstractCount,AVG(R.score) AS AverageAbstractScore " +
                    "FROM Conference C " +
                    "JOIN Abstracts A ON C.ID = A.con_ID " +
                    "JOIN Accepted_Abstracts AA ON A.ID = AA.abs_ID " +
                    "LEFT JOIN Reviews R ON AA.abs_ID = R.abs_ID " +
                    "GROUP BY C.ID, C.con_Name;"; 

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable resultDataTable = new DataTable();
                        dataAdapter.Fill(resultDataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = resultDataTable;
                    }
                }
            }
        }
        private void case4()
        {
            using (SqlConnection connection = new SqlConnection(UserInfo.connectionString))
            {
                connection.Open();

                string query = " SELECT * FROM CountAuthorsSubmmission";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable resultDataTable = new DataTable();
                        dataAdapter.Fill(resultDataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = resultDataTable;
                    }
                }
            }
        }
    }
}
