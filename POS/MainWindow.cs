using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.View;
using Model.ServiceLayer;
using Model.ObjectModel;

namespace POS
{
    public partial class MainWindow : Form
    {
        public State currentState;

        public MainWindow()
        {
            InitializeComponent();

            setUI();  
        }

        public enum State
        {
            READY,
            SALE_MEMBER,
            SALE_NON_MEMBER,
        }

        private void setUI()
        {
            currentState = State.READY;

            // user priveleges
            switch (Configuration.USER_LEVEL)
            {
                case Configuration.Role.ADMIN:
                    databaseToolStripMenuItem.Enabled = true;
                    addNewStaffToolStripMenuItem.Enabled = true;

                    button_Discount.Enabled = true;
                    button_newItem.Enabled = true;

                    break;

                case Configuration.Role.NORMAL:
                    databaseToolStripMenuItem.Enabled = false;
                    addNewStaffToolStripMenuItem.Enabled = false;

                    button_Discount.Enabled = false;
                    button_newItem.Enabled = false;

                    break;

                default:
                    break;
            }

            // regardless of user priveleges, some UI functions are not yet needed
            textBox_customerAddress.Enabled = false;
            textBox_customerCity.Enabled = false;
            textBox_customerEmail.Enabled = false;
            textBox_customerName.Enabled = false;
            textBox_customerPhone.Enabled = false;
            textBox_customerPostCode.Enabled = false;
            comboBox_customerState.Enabled = false;
            textBox_customerAccNo.Enabled = false;

            button_checkout.Enabled = false;
            button_Discount.Enabled = false;
            button_clearSale.Enabled = false;
            button_addItem.Enabled = false;
            button_removeItem.Enabled = false;
            button_priceLookup.Enabled = false;
            button_findCustomer.Enabled = false;

            textBox_itemProductID.Enabled = false;
            textBox_itemQuantity.Enabled = false;
        }

        #region UI Event Handlers
        // logout button clicked
        private void button9_Click(object sender, EventArgs e)
        {
            logout();
        }
       
        private void button_addItem_Click(object sender, EventArgs e)
        {
            // retrieve product record from database, for this ID 
            string productID = textBox_itemProductID.Text;
            Product retrievedProduct = ProductOps.getProduct(productID);

            // check if item already in list
            bool itemInList = false;
            foreach (ListViewItem listItem in listView_sales.Items)
            {
                if (listItem.SubItems[0].Text.Equals(productID))
                {
                    // item already exists
                    itemInList = true;

                    // just increment the total count, and update total item cost
                    string squantity = listItem.SubItems[2].Text;
                    int iquantity = Int32.Parse(squantity);
                    listItem.SubItems[2].Text = (iquantity += iquantity).ToString();

                    string scost = listItem.SubItems[4].Text;
                    float fcost = float.Parse(scost);
                    string sprice = listItem.SubItems[3].Text;
                    float fprice = float.Parse(sprice);
                    listItem.SubItems[4].Text = (fprice + fcost).ToString();

                    // select this item in the list
                    deselectAllItems();
                    listItem.Selected = true;

                    break;
                }
            }

            if (!itemInList)
            {
                // add it to the list
                string[] itemArr = new string[5];
                itemArr[0] = retrievedProduct.getProductIDNumber();
                itemArr[1] = retrievedProduct.getDescription();
                itemArr[2] = "1";// quantity
                itemArr[4] = "1";// total
                itemArr[3] = retrievedProduct.getPrice().ToString();
                ListViewItem item = new ListViewItem(itemArr);
                listView_sales.Items.Add(item);

                // select this item in the list
                deselectAllItems();
                item.Selected = true;
            }

            // clean up UI
            button_addItem.Enabled = false;
            textBox_itemProductID.Text = string.Empty;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        { 
        }

        private void button_newSaleNonMember_Click(object sender, EventArgs e)
        {
            button_newSaleMember.Enabled = false;
            button_newSaleNonMember.Enabled = false;
            button_clearSale.Enabled = true;

            textBox_itemProductID.Enabled = true;

            currentState = State.SALE_NON_MEMBER;
        }

        private void button_clearSale_Click(object sender, EventArgs e)
        {
            if (listView_sales.Items.Count==0)
            {
                // no items
                setUI();
            }
            else
            {
                // items exist, ask user for confirmation
                // TODO: finish implementing this
            }
        }

        // main form is closed
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // clicking the "close" button of the window registers as a DialogResult.Cancel
            if (this.DialogResult==DialogResult.Cancel)
            {
                // ask the user for confirmation first if a sale is taking place
                if ((currentState == State.SALE_MEMBER) || (currentState == State.SALE_NON_MEMBER))
                {
                    DialogResult closeConfirmation = MessageBox.Show("A sale is taking place. Do you really want to close the application?", "Retail POS", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (closeConfirmation == DialogResult.Yes)
                    {
                        this.Close();
                        Application.Exit();
                    }
                    else if (closeConfirmation == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    this.Close();
                    Application.Exit();
                }
            }
        }

        private void button_customerList_Click(object sender, EventArgs e)
        {
            View.ViewCustomersForm customersListForm = new ViewCustomersForm();
            customersListForm.Show();
        }

        private void viewHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View.ViewTransactionsForm transactionsListForm = new ViewTransactionsForm();
            transactionsListForm.Show();
        }

        private void button_addCustomer_Click(object sender, EventArgs e)
        {
            View.NewCustomerForm newCustomerForm = new View.NewCustomerForm();
            newCustomerForm.Show();
        }

        private void addNewCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View.NewCustomerForm newCustomerForm = new View.NewCustomerForm();
            newCustomerForm.Show();
        }

        private void addNewStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View.NewStaffForm newStaffForm = new View.NewStaffForm();
            newStaffForm.Show();
        }

        private void button_staffList_Click(object sender, EventArgs e)
        {
            View.ViewStaffForm viewStaffForm = new View.ViewStaffForm();
            viewStaffForm.Show();
        }

        private void button_findItem_Click(object sender, EventArgs e)
        {
            View.ViewProductsForm viewProductsForm = new ViewProductsForm();
            viewProductsForm.Show();
        }

        private void button_newItem_Click(object sender, EventArgs e)
        {
            View.NewProductForm newProductForm = new View.NewProductForm();
            newProductForm.Show();
        }

        private void button_findCustomer_Click(object sender, EventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logout();
        }
        #endregion

        public void deselectAllItems()
        {
            foreach (ListViewItem selectedItem in listView_sales.SelectedItems)
            {
                selectedItem.Selected = false;
            }
        }

        public void logout()
        {
            if ((currentState == State.SALE_NON_MEMBER) || (currentState == State.SALE_MEMBER))
            {
                DialogResult logoutConfirmation = MessageBox.Show("A sale is taking place. Do you really want to log out?", "Retail POS", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (logoutConfirmation == DialogResult.Yes)
                {
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Close();
                }
                else if (logoutConfirmation == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_newSaleMember_Click(object sender, EventArgs e)
        {

            currentState = State.SALE_MEMBER;
        }

        private void textBox_itemQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_itemProductID_TextChanged(object sender, EventArgs e)
        {
            if (textBox_itemProductID.Text!=string.Empty)
            {
                button_addItem.Enabled = true;
            }
        }

        private void listView_sales_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numberSelectedItems = listView_sales.SelectedItems.Count;

            switch (numberSelectedItems)
            {
                case 0:
                    richTextBox_itemPrice.Text = "0.00";
                    button_removeItem.Enabled = false;

                    break;

                case 1:
                    // display price
                    richTextBox_itemPrice.Text = listView_sales.SelectedItems[0].SubItems[3].Text;
                    button_removeItem.Enabled = true;

                    break;

                default:
                    // cannot select multiple items
                    deselectAllItems();

                    break;
            }
        }
    }
}
