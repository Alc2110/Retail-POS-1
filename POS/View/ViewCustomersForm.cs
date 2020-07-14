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
using Model.ServiceLayer;
using Model.ObjectModel;

namespace POS.View
{
    public partial class ViewCustomersForm : Form
    {
        // controller dependency injection
        CustomerController controller;

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events fired from the model
        private delegate void populateCustomersViewDelegate(object sender, GetAllCustomersEventArgs args);

        public ViewCustomersForm()
        {
            InitializeComponent();

            logger.Info("Initialising view customers form");

            // prepare the listView
            listView_customers.Columns.Add("ID",50);
            listView_customers.Columns.Add("Full name",120);
            listView_customers.Columns.Add("Street address",150);
            listView_customers.Columns.Add("Phone number",100);
            listView_customers.Columns.Add("Email",150);
            listView_customers.Columns.Add("City",130);
            listView_customers.Columns.Add("State",100);
            listView_customers.Columns.Add("Postcode",75);
            listView_customers.View = System.Windows.Forms.View.Details;
            listView_customers.GridLines = true;
            // make the headers bold
            for (int i = 0; i < listView_customers.Columns.Count; i++)
            {
                listView_customers.Columns[i].ListView.Font = new Font(listView_customers.Columns[i].ListView.Font, FontStyle.Bold);
            }

            // colour and position the busy indicator properly
            circularProgressBar1.Location = calculateBusyIndicatorPos();
            circularProgressBar1.ProgressColor = Configuration.ProgressBarColours.TASK_IN_PROGRESS_COLOUR;
          
            // can't delete anything until something is selected
            button_deleteSelectedCustomer.Enabled = false;

            // controller dependency injection
            controller = new CustomerController();
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            logger.Info("Closing new customers form");

            this.Close();
        }

        private async void button_deleteSelectedCustomer_Click(object sender, EventArgs e)
        {
            if (controller!=null)
            {
                try
                {
                    showBusyIndicator();

                    // prepare the data
                    int idNum;
                    Int32.TryParse(listView_customers.SelectedItems[0].SubItems[0].Text, out idNum);

                    // log it
                    logger.Info("Deleting customer record: ");
                    logger.Info("ID: " + idNum);

                    await Task.Run(() =>
                    {
                        // run this task in a separate thread
                        controller.deleteCustomer(idNum);
                    });
                }
                catch (Exception ex)
                {
                    // it failed
                    // tell the user and the logger
                    string errorMessage = "Error deleting customer: " + ex.Message;
                    MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error(ex, errorMessage);
                    logger.Error("Stack Trace: " + ex.StackTrace);

                    removeBusyIndicator();

                    // nothing more we can do
                    return;
                }

                // at this point, it succeeded
                // tell the user and the logger
                string successMessage = "Successfully deleted customer record";
                MessageBox.Show(successMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logger.Info(successMessage);
            }
        }

        private void button_addNewCustomer_Click(object sender, EventArgs e)
        {
            NewCustomerForm newCustomerForm = new NewCustomerForm();
            newCustomerForm.Show();
        }

        private async void ViewCustomersForm_Load(object sender, EventArgs e)
        {
            // subscribe to the required Model events
            POS.Configuration.customerOps.GetAllCustomers += new EventHandler<GetAllCustomersEventArgs>(customerEventHandler);

            // populate the list upon loading
            try
            {
                showBusyIndicator();

                // run this operation in a separate thread
                await Task.Run(() =>
                {
                    Configuration.customerOps.getAllCustomers();
                });
            }
            catch (System.IO.InvalidDataException invDatEx)
            {
                // found invalid data in database
                // tell the user and the logger
                string errorMessage = "Error: found invalid data in database: " + invDatEx.Message;
                MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(errorMessage);

                removeBusyIndicator();

                return;
            }
            catch (Exception ex)
            {
                // it failed with some other error
                // tell the user and the logger
                string errorMessage = "Error: failed to retrieve data from database: " + ex.Message;
                MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(errorMessage);

                removeBusyIndicator();

                return;
            }
        }

        private void listView_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_customers.SelectedItems.Count>1)
            {
                // only one selection allowed at a time
                foreach (ListViewItem item in listView_customers.SelectedItems)
                {
                    item.Selected = false;
                }
            }

            if (listView_customers.SelectedItems.Count==1)
            {
                button_deleteSelectedCustomer.Enabled = true;
            }
            else
            {
                button_deleteSelectedCustomer.Enabled = false;
            }
        }

        private void ViewCustomersForm_Resize(object sender, EventArgs e)
        {
            // reposition the circular progress bar
            circularProgressBar1.Location = calculateBusyIndicatorPos();
        }
        #endregion

        /// <summary>
        /// Event handler for getting list of customer records.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void customerEventHandler(object sender, GetAllCustomersEventArgs args)
        {
            if (listView_customers.InvokeRequired)
            {
                listView_customers.Invoke(new populateCustomersViewDelegate(populateView), new object[] { sender, args });
            }
            else
            {
                showBusyIndicator();

                populateView(sender, args);
            }
        }

        private void populateView(object sender, GetAllCustomersEventArgs args)
        {
            showBusyIndicator();

            List<ICustomer> customers = args.getList().ToList();

            // tell the listView it is being updated
            listView_customers.BeginUpdate();

            // clean the listView
            listView_customers.Items.Clear();

            // now fill it
            foreach (Customer customer in customers)
            {
                string[] itemArr = new string[8];
                itemArr[0] = customer.CustomerID.ToString();
                itemArr[1] = customer.FullName;
                itemArr[2] = customer.Address;
                itemArr[3] = customer.PhoneNumber;
                itemArr[4] = customer.Email;
                itemArr[5] = customer.City;
                itemArr[6] = customer.state.ToString();
                itemArr[7] = customer.Postcode.ToString();
                ListViewItem item = new ListViewItem(itemArr);
                item.Font = new Font(item.Font, FontStyle.Regular);
                listView_customers.Items.Add(item);
            }

            // tell the listView it is ready
            listView_customers.EndUpdate();

            removeBusyIndicator();
        }
        
        private System.Drawing.Point calculateBusyIndicatorPos()
        {
            int xPos = ((groupBox1.Width)/2)-((circularProgressBar1.Width)/2);
            int yPos = ((groupBox1.Height) / 2) - ((circularProgressBar1.Height) / 2);

            return new System.Drawing.Point(xPos, yPos);
        } 

        private void removeBusyIndicator()
        {
            circularProgressBar1.Visible = false;
            listView_customers.Visible = true;
        }

        private void showBusyIndicator()
        {
            circularProgressBar1.Visible = true;
            listView_customers.Visible = false;
        }
    }
}
