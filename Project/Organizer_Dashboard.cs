using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project
{
    public partial class Organizer_Dashboard : Form
    {
        public Organizer_Dashboard()
        {
            InitializeComponent();
        }
           
        private void Form3_Load(object sender, EventArgs e)
        {

        }

       

     
        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.LightSeaGreen; button3.ForeColor = Color.White;
        }

       


       

       

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Event.BackColor = Color.Turquoise; Event.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Event.BackColor = Color.FromArgb(41, 128, 185); Event.ForeColor = Color.White;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            Event_Progress.BackColor = Color.Aquamarine; Event_Progress.ForeColor = Color.White;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            Event_Progress.BackColor = Color.FromArgb(41, 128, 185); Event_Progress.ForeColor = Color.White;

        }

     


        private void Event_Click(object sender, EventArgs e)
        {
            // Instantiate the ChildForm
            Add_Event_Form childForm = new Add_Event_Form();

            // Set properties of the ChildForm
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Add the ChildForm to the panel on the MainForm
            panel3.Controls.Add(childForm);

            // Show the ChildForm
            childForm.Show();

        }

        private void Event_Progress_Click(object sender, EventArgs e)
        {
            // Instantiate the ChildForm
            Conferences_Details childForm = new Conferences_Details();

            // Set properties of the ChildForm
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Add the ChildForm to the panel on the MainForm
            Add_Event.Controls.Add(childForm);

            // Show the ChildForm
            childForm.Show();


        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }
    }
}
