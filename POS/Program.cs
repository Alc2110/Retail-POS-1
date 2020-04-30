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
        // TODO: fix process still running when the application is closed
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

            // read database connection information from configuration file
            Configuration.CONNECTION_STRING = "Server=" + ConfigurationManager.AppSettings["serverAddress"] + ";Database=Retail_POS;User Id=" + ConfigurationManager.AppSettings["serverUser"] +
                                              ";Password=" + ConfigurationManager.AppSettings["serverPassword"];

            // read store name from configuration file
            Configuration.STORE_NAME = ConfigurationManager.AppSettings["storeName"];

            // show the login form
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);

            Application.ExitThread();
            Environment.Exit(Environment.ExitCode);
        }
    }
}
