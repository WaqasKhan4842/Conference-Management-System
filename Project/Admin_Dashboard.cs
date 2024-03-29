using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void View_Conference_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Admin_View_Conferences(), sender);

        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (DesktopPage.Controls.Count > 0)
            {
                // You can either remove the existing child form
                 DesktopPage.Controls.RemoveAt(0);

                // Or hide the existing child form
                //DesktopPage.Controls[0].Hide();
            }
            // Set properties of the ChildForm
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Add the ChildForm to the panel on the MainForm
            DesktopPage.Controls.Add(childForm);
            // Show the ChildForm
            childForm.Show();
        }


        private void Manage_Reviewer_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Manage_Reviewer(), sender);

        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Close();
            HomePage pg = new HomePage();
            pg.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
            Login lg = new Login();
            lg.Show();

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Statistics(), sender);

        }
    }
}
