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
    public partial class ViewCustomersForm : Form
    {
        // controller dependency injection
        CustomerController controller;

        public ViewCustomersForm()
        {
            InitializeComponent();

            // prepare the listView
            listView_customers.Columns.Add("ID");
            listView_customers.Columns.Add("Full name");
            listView_customers.Columns.Add("Street address");
            listView_customers.Columns.Add("Phone number");
            listView_customers.Columns.Add("Email");
            listView_customers.Columns.Add("City");
            listView_customers.Columns.Add("State");
            listView_customers.Columns.Add("Postcode");

            listView_customers.View = System.Windows.Forms.View.Details;

            // can't delete anything until something is selected
            button_deleteSelectedCustomer.Enabled = false;

            // controller dependency injection
            controller = CustomerController.getInstance();
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_deleteSelectedCustomer_Click(object sender, EventArgs e)
        {
            if (controller!=null)
            {
                try
                {
                    int idNum;
                    if (Int32.TryParse(listView_customers.SelectedItems[0].SubItems[0].Text, out idNum))
                    {
                        controller.deleteCustomer(idNum);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void button_addNewCustomer_Click(object sender, EventArgs e)
        {
            NewCustomerForm newCustomerForm = new NewCustomerForm();
            newCustomerForm.Show();
        }

        private void ViewCustomersForm_Load(object sender, EventArgs e)
        {

        }

        private void listView_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_customers.SelectedItems.Count>0)
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
        #endregion

        private void populateView()
        { }
    }
}
