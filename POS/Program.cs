using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Model;

namespace POS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // set the program state
            Configuration.currentProgramState = ProgramState.LOGGED_OUT;

            // configure the logger
            Configuration.Logger.ConfigureLogger();

            // read database connection information from configuration file
            Configuration.CONNECTION_STRING = "Server=" + ConfigurationManager.AppSettings["serverAddress"] + ";Database=Retail_POS;User Id=" + ConfigurationManager.AppSettings["serverUser"] +
                                              ";Password=" + ConfigurationManager.AppSettings["serverPassword"];

            // read store name from configuration file
            Configuration.STORE_NAME = ConfigurationManager.AppSettings["storeName"];

            // initialise the models
            Configuration.customerOps = new Model.ServiceLayer.CustomerOps();
            Configuration.productOps = new Model.ServiceLayer.ProductOps();
            Configuration.staffOps = new Model.ServiceLayer.StaffOps();
            Configuration.transactionOps = new Model.ServiceLayer.TransactionOps();

            try
            {
                // show the login form
                LoginForm loginForm = new LoginForm();
                Application.Run(loginForm);

                Application.ExitThread();
                Environment.Exit(Environment.ExitCode);
            }
            catch (Exception ex)
            {
                // something bad happened
                // this is a last resort to catch the error
                // TODO: log it
                System.Windows.Forms.MessageBox.Show("A fatal error occurred: " + ex.Message);
            }
        }
    }
}
