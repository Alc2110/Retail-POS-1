using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;

namespace POS.View
{
    public partial class NewCustomerForm : Form
    {
        // controller dependency injection
        private CustomerController controller;

        // get logger instance for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public NewCustomerForm()
        {
            InitializeComponent();

            // prepare the comboBox
            comboBox_state.Items.Add("NSW");
            comboBox_state.Items.Add("SA");
            comboBox_state.Items.Add("Qld");
            comboBox_state.Items.Add("Vic");
            comboBox_state.Items.Add("ACT");
            comboBox_state.Items.Add("Tas");
            comboBox_state.Items.Add("WA");
            comboBox_state.Items.Add("NT");
            comboBox_state.Items.Add("Other");

            // cannot add anything yet
            button_add.Enabled = false;

            // controller dependency injection
            controller = CustomerController.getInstance();

            // event handlers
            textBox_city.TextChanged += checkEntries;
            textBox_email.TextChanged += checkEntries;
            textBox_fullName.TextChanged += checkEntries;
            textBox_phoneNumber.TextChanged += checkEntries;
            textBox_postcode.TextChanged += checkEntries;
            textBox_streetAddress.TextChanged += checkEntries;
        }

        #region UI event handlers
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (controller!=null)
            {
                try
                {
                    int postCode;
                    Int32.TryParse(textBox_postcode.Text, out postCode);
                    controller.addCustomer(textBox_fullName.Text, textBox_streetAddress.Text, textBox_phoneNumber.Text, textBox_email.Text, textBox_city.Text, comboBox_state.SelectedItem.ToString(),
                        postCode);
                }
                catch (Exception ex)
                {
                    // error
                    // inform the user
                    string errorMessage = "Error adding new customer: " + ex.Message;
                    logger.Error(ex, errorMessage);

                    MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                // success
                // inform the user
                string successMessage = "Successfully added new customer";
                logger.Info(successMessage);
                MessageBox.Show(successMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // clean up the UI
                textBox_fullName.Text = string.Empty;
                textBox_phoneNumber.Text = string.Empty;
                textBox_streetAddress.Text = string.Empty;
                textBox_postcode.Text = string.Empty;
                textBox_email.Text = string.Empty;
                textBox_city.Text = string.Empty;
            }
        }
        #endregion

        private void checkEntries(object sender, EventArgs e)
        {
            if ((textBox_city.Text!=string.Empty) && (textBox_email.Text!=string.Empty) && (textBox_fullName.Text!=string.Empty) && (textBox_phoneNumber.Text!=string.Empty) && 
                (textBox_postcode.Text!=string.Empty) && (textBox_streetAddress.Text!=string.Empty))
            {
                button_add.Enabled = true;
            }
            else
            {
                button_add.Enabled = false;
            }
        }
    }
}
