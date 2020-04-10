using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
// https://www.tutorialsteacher.com/csharp/csharp-event

namespace POS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // configure the logger
            Configuration.Logger.ConfigureLogger();

            // read database connection string from configuration file
            Configuration.CONNECTION_STRING = ConfigurationManager.AppSettings["connString"];

            // read store name from configuration file
            Configuration.STORE_NAME = ConfigurationManager.AppSettings["storeName"];

            // show the login form
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);
        }
    }
}
