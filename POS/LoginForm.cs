using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
/*https://www.mssqltips.com/sqlservertip/5771/querying-sql-server-tables-from-net/*/
namespace POS
{
    public partial class LoginForm : Form
    {
        string retrievedHash = null;

        // create instance of logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        int staffID;

        public LoginForm()
        {
            
            logger.Info("Initialising login form");

            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            tryLogin();
        }

        private void tryLogin()
        {
            // attempt connection
            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                try
                {
                    logger.Info("Attempting connection with database");
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    conn.Close();
                    string errorMessage = "Error connecting to database: " + ex.Message;
                    MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error(ex, errorMessage);

                    return;
                }

                if (authenticated(conn))
                {
                    // authentication successful
                    // feedback for user
                    string authSuccessMessage = "Login successful";
                    MessageBox.Show(authSuccessMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info(authSuccessMessage);

                    // config
                    Configuration.STAFF_ID = staffID;

                    // show main form and close this one
                    MainWindow mainForm = new MainWindow();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    // authentication failed
                    // feedback for user
                    string authFailMessage = "Incorrect credentials";
                    MessageBox.Show(authFailMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private bool authenticated(SqlConnection conn)
        {
            // salting and hashing
            Security.Hasher loginHasher = new Security.Hasher(textBox_username.Text, textBox_password.Text);
            string hash = loginHasher.computeHash();

            // check if username exists
            string userNameQuery = "DECLARE @varFullName varchar(max);" +
            " SET @varFullName = '" + textBox_username.Text + "';" +
            " SELECT * FROM Staff" +
            " WHERE FullName = @varFullName;";
            SqlCommand userNameCommand = new SqlCommand(userNameQuery, conn);
            SqlDataReader userNameDataReader = userNameCommand.ExecuteReader();
            if (!userNameDataReader.HasRows)
            {
                // empty, no such username exists
                logger.Info("Username does not exist");
                conn.Close();

                return false;
            }

            // retrieve salted hash from database and compare
            // while there, might as well grab the user privelege level
            while (userNameDataReader.Read())
            {
                retrievedHash = userNameDataReader.GetString(2);
                staffID = userNameDataReader.GetInt32(0);

                string retrievedPrivelege = userNameDataReader.GetString(3);
                switch (retrievedPrivelege)
                {
                    case "Admin":
                        Configuration.USER_LEVEL = Configuration.Role.ADMIN;
                        logger.Info("Admin user");

                        break;

                    case "Normal":
                        Configuration.USER_LEVEL = Configuration.Role.NORMAL;
                        logger.Info("Normal user");

                        break;

                    default:
                        // this shouldn't happen
                        // TODO: handle it appropriately
                        break;
                }
            }

            if (retrievedHash.Equals(hash))
            {
                // success
                Configuration.STAFF_ID = staffID;
                
                return true;
            }

            // access denied
            return false;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            logger.Info("Exiting application");
            Application.Exit();
        }
    }
}
