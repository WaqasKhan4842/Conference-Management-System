using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string SQLconnection = "Data Source=DESKTOP-CRB2O4O\\SQLEXPRESS01;Initial Catalog=DB;Integrated Security=True";
            UserInfo.connectionString = SQLconnection;
            Application.Run(new HomePage());
        }
        
    }
}
