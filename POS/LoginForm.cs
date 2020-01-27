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

        public LoginForm()
        {

            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (authenticated())
            {
                // show main form and close this one
                // TODO: do this properly
                
                MainWindow mainForm = new MainWindow();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect credentials", "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }    
        }

        private bool authenticated()
        {
            // salting and hashing
            Security.Hasher loginHasher = new Security.Hasher(textBox_username.Text, textBox_password.Text);
            string hash = loginHasher.computeHash();

            using (SqlConnection conn = new SqlConnection(Configuration.connectionString)) // this seems to work
            {
                // attempt connection
                try
                {
                    conn.Open();
                }
                catch (System.Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Connection error: " + ex, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

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
                    conn.Close();

                    return false;
                }

                // retrieve salted hash from database and compare
                // while there, might as well grab the user privelege level
                while (userNameDataReader.Read())
                {
                    retrievedHash = userNameDataReader.GetString(2);

                    string retrievedPrivelege = userNameDataReader.GetString(3);
                    switch (retrievedPrivelege)
                    {
                        case "Admin":
                            Configuration.userLevel = Configuration.Role.ADMIN;
                            break;
                        case "Normal":
                            Configuration.userLevel = Configuration.Role.NORMAL;
                            break;
                        default:
                            // this shouldn't happen
                            // TODO: handle it appropriately
                            break;
                    }
                }
            }

            if (retrievedHash.Equals(hash))
            {
                // success
                return true;
            }

            return false;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
