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
    public partial class Signup: Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Event_Organizer_SignUp childForm = new Event_Organizer_SignUp();
            // Set properties of the ChildForm
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Add the ChildForm to the panel on the MainForm
            desktoppanel.Controls.Add(childForm);

            // Show the ChildForm
            childForm.Show();


        }
    }
}
