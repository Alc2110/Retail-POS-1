using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.View
{
    public partial class NewStaffForm : Form
    {
        public NewStaffForm()
        {
            InitializeComponent();

            comboBox_privelege.Items.Add("Admin");
            comboBox_privelege.Items.Add("Normal");
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_addStaff_Click(object sender, EventArgs e)
        {
            if (!(textBox_password.Text.Equals(textBox_repeatPassword.Text)))
            {
                MessageBox.Show("Passwords not the same. Please try again", "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBox_password.Text = null;
                textBox_repeatPassword.Text = null;
            }
            else
            {

            }
        }
        #endregion
    }
}
