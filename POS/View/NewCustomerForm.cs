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
using Model.ObjectModel;

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

            logger.Info("Initialising new customer form");

            // prepare the comboBox
            foreach (var val in States.GetValues(typeof(States)))
            {
                string state = val.ToString();
                if (!state.Equals("Default"))
                    comboBox_state.Items.Add(state);
            }

            // cannot add anything yet
            button_add.Enabled = false;

            // controller dependency injection
            controller = new CustomerController();

            // subscribe to textbox interaction events
            textBox_city.TextChanged += checkEntries;
            textBox_email.TextChanged += checkEntries;
            textBox_fullName.TextChanged += checkEntries;
            textBox_phoneNumber.TextChanged += checkEntries;
            textBox_postcode.TextChanged += checkEntries;
            textBox_streetAddress.TextChanged += checkEntries;

            // status bar
            labelProgressBar1.Value = 100;
            labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.IDLE_COLOUR, "Ready");
        }

        #region UI event handlers
        private void button_cancel_Click(object sender, EventArgs e)
        {
            logger.Info("Closing new customer form");

            this.Close();
        }

        private async void button_add_Click(object sender, EventArgs e)
        {
            if (controller!=null)
            {
                try
                {
                    // use busy cursor
                    this.UseWaitCursor = true;

                    // update the status bar
                    labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.TASK_IN_PROGRESS_COLOUR, "Adding new customer record");

                    // prepare the data
                    int postCode;
                    Int32.TryParse(textBox_postcode.Text, out postCode);
                    string fullName = textBox_fullName.Text;
                    string streetAddress = textBox_streetAddress.Text;
                    string phoneNumber = textBox_phoneNumber.Text;
                    string email = textBox_email.Text;
                    string city = textBox_city.Text;
                    string sstate = comboBox_state.SelectedItem.ToString();
                    Customer newCustomer = new Customer();
                    newCustomer.Postcode = postCode;
                    newCustomer.Address = streetAddress;
                    newCustomer.FullName = fullName;
                    newCustomer.Email = email;
                    newCustomer.City = city;
                    newCustomer.PhoneNumber = phoneNumber;
                    Model.ObjectModel.States state;
                    if (Enum.TryParse(sstate, out state))
                    {
                        newCustomer.state = state;
                    }
                    else
                    {
                        // should never happen
                        throw new Exception("Invalid customer data");
                    }

                    // log it
                    logger.Info("Adding customer record: ");
                    logger.Info("Full Name: " + fullName);
                    logger.Info("Street address: " + streetAddress);
                    logger.Info("Phone number: " + phoneNumber);
                    logger.Info("Email: " + email);
                    logger.Info("City: " + city);
                    logger.Info("State: " + sstate);

                    // run this operation on a different thread
                    await Task.Run(() =>
                    {
                        controller.addCustomer(newCustomer);
                    });
                }
                catch (Exception ex)
                {
                    // error
                    // inform the user and the logger
                    string errorMessage = "Error adding new customer: " + ex.Message;
                    logger.Error(ex, errorMessage);
                    logger.Error("Stack Trace: " + ex.StackTrace);
                    labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.TASK_FAILED_COLOUR, "Error adding new customer");
                    labelProgressBar1.Value = 100;
                    this.UseWaitCursor = false;
                    MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // nothing more we can do
                    return;
                }

                // success
                // inform the user and the logger
                string successMessage = "Successfully added new customer";
                logger.Info(successMessage);
                labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.TASK_SUCCEEDED_COLOUR, successMessage);
                labelProgressBar1.Value = 100;
                this.UseWaitCursor = false;
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

            labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.IDLE_COLOUR, "Ready");
            labelProgressBar1.Value = 100;
        }
    }
}
