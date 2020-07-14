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
using Controller;
using System.Diagnostics;
using System.Reflection;
using System.Globalization;

namespace POS
{
    public partial class MainWindow : Form
    {
        // TODO: solve culture problem causing dollar sign to show up in the wrong place

        // TODO: implement State pattern (maybe)
        public State currentState;

        // controller dependency injection
        private TransactionController transController;

        // create an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();

            // logging
            logger.Info("Initialising main window");

            // customer state combo box
            // prepare the customer state comboBox
            foreach (var val in States.GetValues(typeof(States)))
            {
                string state = val.ToString();
                if (!state.Equals("Default"))
                    comboBox_customerState.Items.Add(state);
            }

            // TODO: implement this
            scriptingToolStripMenuItem.Enabled = false;

            listView_sales.GridLines = true;

            setUI();

            // controller dependency injection
            transController = new TransactionController();

            // set title
            this.Text = "Retail POS v" + Configuration.VERSION;
        }

        public enum State
        {
            READY,
            SALE_MEMBER,
            SALE_NON_MEMBER,
        }

        private void setUI()
        {
            // test
            //MessageBox.Show("current culture name: " + CultureInfo.CurrentCulture.Name + ". current ui culture name: " + CultureInfo.CurrentUICulture.Name, "Retail POS");");

            logger.Info("Resetting the main window");

            currentState = State.READY;
            logger.Info("Current state: " + currentState.ToString());

            // user priveleges
            logger.Info("Current user level: " + Configuration.USER_LEVEL.ToString());
            switch (Configuration.USER_LEVEL)
            {
                case Configuration.Role.ADMIN:
                    databaseToolStripMenuItem.Enabled = true;
                    addNewStaffToolStripMenuItem.Enabled = true;

                    button_Discount.Enabled = true;
                    //button_newItem.Enabled = true;

                    break;

                case Configuration.Role.NORMAL:
                    databaseToolStripMenuItem.Enabled = false;
                    addNewStaffToolStripMenuItem.Enabled = false;

                    button_Discount.Enabled = false;
                    //button_newItem.Enabled = false;

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

            textBox_customerAccNo.Text = null;
            textBox_customerAddress.Text = null;
            textBox_customerCity.Text = null;
            textBox_customerEmail.Text = null;
            textBox_customerName.Text = null;
            textBox_customerPhone.Text = null;
            textBox_customerPostCode.Text = null;
            textBox_itemProductID.Text = null;
            textBox_itemQuantity.Text = null;

            button_checkout.Enabled = false;
            button_Discount.Enabled = false;
            button_clearSale.Enabled = false;
            button_addItem.Enabled = false;
            button_removeItem.Enabled = false;
            button_priceLookup.Enabled = false;
            button_findCustomer.Enabled = false;

            if (Configuration.USER_LEVEL == Configuration.Role.ADMIN)
            {
                //transactionToolStripMenuItem.Enabled = true;
            }
            else
            {
                //transactionToolStripMenuItem.Enabled = false;
            }

            button_newSaleMember.Enabled = true;
            button_newSaleNonMember.Enabled = true;

            textBox_itemProductID.Enabled = true;
            textBox_itemQuantity.Enabled = false;

            double priceDisplayed = 0.00;
            string sPriceDisplayed = priceDisplayed.ToString("C2", CultureInfo.GetCultureInfo("en-AU"));
            richTextBox_itemPrice.Text = sPriceDisplayed;
            //richTextBox_itemPrice.Text = "0.00";

            scriptingToolStripMenuItem.Enabled = false;

            foreach (ListViewItem listItem in listView_sales.Items)
            {
                listView_sales.Items.Remove(listItem);
            }

            switch (Configuration.USER_LEVEL)
            {
                case Configuration.Role.ADMIN:
                    toolStripStatusLabel_accType.Text = "Admin";
                    break;
                case Configuration.Role.NORMAL:
                    toolStripStatusLabel_accType.Text = "Normal";
                    break;
                default:
                    // shouldn't happen
                    throw new Exception("Unknown user access level");
            }
            
            toolStripStatusLabel_state.Text = "Ready";
        }

        #region UI Event Handlers
        // logout button clicked
        private void button9_Click(object sender, EventArgs e)
        {
            logout();
        }
       
        private async void button_addItem_Click(object sender, EventArgs e)
        {
            // retrieve product record from database, for this ID 
            string productID = textBox_itemProductID.Text;
            Product retrievedProduct = new Product();
            try
            {
                // run this operation on a separate thread
               await Task.Run(() =>
               {
                   // ask the model for the product information
                   retrievedProduct = (Product)(POS.Configuration.productOps.getProduct(productID));
               });
            }
            catch (Exception ex)
            {
                // something went wrong
                // tell the user and the logger
                string retrieveProductErrorMessage = "Failed to retrieve product from database: " + ex.Message;
                System.Windows.Forms.MessageBox.Show(retrieveProductErrorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, retrieveProductErrorMessage);
                logger.Error("Stack trace: " + ex.StackTrace);

                // nothing more we can do
                return;
            }

            logger.Info("Retrieving product from database, for product ID: " + productID);

            // could not find product
            if (retrievedProduct==null)
            {
                // tell the user and the logger
                string nullProductMessage = "Could not find specified product";
                MessageBox.Show(nullProductMessage, "Retail POS", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                logger.Warn(nullProductMessage);

                // reset the product id text box
                textBox_itemProductID.Text = string.Empty;

                // nothing more to do
                return;
            }

            // check if item already in list
            bool itemInList = false;
            foreach (ListViewItem listItem in listView_sales.Items)
            {
                if (listItem.SubItems[0].Text.Equals(productID))
                {
                    // item already exists
                    itemInList = true;
                    logger.Info("Item of this type is already in list (cart)");

                    // calculate cost of new items being added
                    float productPrice = retrievedProduct.price;
                    string sNumber = textBox_itemQuantity.Text;
                    int iNumber = Int32.Parse(sNumber);

                    // add this cost to the total cost
                    string sOldCost = listItem.SubItems[4].Text;
                    float iOldCost = float.Parse(sOldCost);
                    float iNewCost = iOldCost + (productPrice * iNumber);
                    listItem.SubItems[4].Text = iNewCost.ToString();

                    // update number of items
                    string sQuantity = listItem.SubItems[2].Text;
                    int iQuantity = Int32.Parse(sQuantity);
                    int iNewQuantity = iQuantity + iNumber;
                    listItem.SubItems[2].Text = iNewQuantity.ToString();

                    deselectAllItems();
                    //listItem.Selected = true;

                    // display total price

                    break;
                }
            }

            if (!itemInList)
            {
                // item not yet in list
                logger.Info("Item of this type not yet in list. Adding item to list");

                // add it to the list
                string[] itemArr = new string[5];
                itemArr[0] = retrievedProduct.ProductIDNumber;
                itemArr[1] = retrievedProduct.Description;
                //itemArr[2] = "1";// quantity
                string sQuantity = textBox_itemQuantity.Text;
                int iQuantity = Int32.Parse(sQuantity);
                //itemArr[2] = textBox_itemQuantity.Text;
                itemArr[2] = sQuantity;
                //itemArr[4] = retrievedProduct.getPrice().ToString();// total
                itemArr[4] = (iQuantity * retrievedProduct.price).ToString();
                itemArr[3] = retrievedProduct.price.ToString();
                ListViewItem item = new ListViewItem(itemArr);
                //item.Font = new Font(item.Font, FontStyle.Regular);
                item.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                listView_sales.Items.Add(item);

                // select this item in the list
                deselectAllItems();
                //item.Selected = true;

                // display total price

                // item not yet in list
                // item should now be 1
                textBox_itemQuantity.Text = "1";
            }
            else
            {
                // increment item count
                string sItemCount = textBox_itemQuantity.Text;
                int iItemCount = Int32.Parse(sItemCount);
                textBox_itemQuantity.Text = (iItemCount + 1).ToString();
            }

            // checkout button
            if (listView_sales.Items.Count>0)
            {
                button_checkout.Enabled = true;
            }
            else
            {
                button_checkout.Enabled = false;
            }

            // clean up UI
            textBox_itemProductID.Select();
            button_addItem.Enabled = false;
            button_removeItem.Enabled = false;
            textBox_itemProductID.Text = string.Empty;
            textBox_itemQuantity.Text = string.Empty;

            deselectAllItems();

            displayTotal();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // prepare the listView
            // make the headers bold
            listView_sales.Font = new Font("Microsoft Sans Serif", 12);
            for (int i = 0; i < listView_sales.Columns.Count; i++)
            {
                listView_sales.Columns[i].ListView.Font = new Font(listView_sales.Columns[i].ListView.Font, FontStyle.Bold);
            }
        }

        private void button_newSaleNonMember_Click(object sender, EventArgs e)
        {
            logger.Info("Initiating nom-member sale");

            button_newSaleMember.Enabled = false;
            button_newSaleNonMember.Enabled = false;

            button_clearSale.Enabled = true;

            textBox_itemProductID.Enabled = true;

            currentState = State.SALE_NON_MEMBER;
            toolStripStatusLabel_state.Text = "Non-member sale";
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
                DialogResult dialogResult = MessageBox.Show("A sale is taking place. Do you really want to clear it?",
                                                            "Retail POS", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dialogResult==DialogResult.Yes)
                {
                    logger.Info("Clearing sale");
                    setUI();
                }
                else if (dialogResult==DialogResult.No)
                {
                    return;
                }
            }
        }

        // main form is closed
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((currentState == State.SALE_MEMBER) || (currentState == State.SALE_NON_MEMBER))
            {
                var result = MessageBox.Show("A sale is taking place. Do you really want to close the application?",
                                                "Retail POS", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (result != System.Windows.Forms.DialogResult.Yes)
                    e.Cancel = true;
            }

            if (!(Configuration.currentProgramState == Model.ProgramState.LOGGED_OUT))
            {
                Application.Exit();
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

        private async void button_findCustomer_Click(object sender, EventArgs e)
        {
            // get customer data
            // prepare the data
            //string customerAccNumber = textBox_customerAccNo.Text;
            int customerAccNumber = (Int32.Parse(textBox_customerAccNo.Text));
            logger.Info("Attempting to find customer with account number: " + customerAccNumber);

            Customer retrievedCustomer = null;
            try
            {
                // run this operation on a separate thread
                await Task.Run(() =>
                {
                    retrievedCustomer = (Customer)POS.Configuration.customerOps.getCustomer(customerAccNumber);
                });
            }
            catch (Exception ex)
            {
                // something went wrong 
                // tell the user and the logger
                string errorRetrievingCustomerMessage = "Failed to retrieve customer: " + ex.Message;
                System.Windows.Forms.MessageBox.Show(errorRetrievingCustomerMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, errorRetrievingCustomerMessage);
                logger.Error("Stack trace: " + ex.StackTrace);

                // nothing more we can do
                return;
            }

            // could not find customer
            if (retrievedCustomer==null)
            {
                // tell the user and the logger
                string nullCustomerMessage = "Could not find specified customer";
                MessageBox.Show(nullCustomerMessage, "Retail POS",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                logger.Warn(nullCustomerMessage);

                // nothing more we can do
                return;
            }

            // found the customer
            // load the data into the window
            logger.Info("Found customer record");
            button_findCustomer.Enabled = false;
            textBox_customerName.Text = retrievedCustomer.FullName;
            textBox_customerPhone.Text = retrievedCustomer.PhoneNumber;
            textBox_customerEmail.Text = retrievedCustomer.Email;
            textBox_customerAddress.Text = retrievedCustomer.Address;
            textBox_customerCity.Text = retrievedCustomer.City;
            textBox_customerPostCode.Text = retrievedCustomer.Postcode.ToString();
            Model.ObjectModel.States retrievedCustomerState = retrievedCustomer.state;
            string sRetrievedCustomerState = retrievedCustomerState.ToString();
            int comboBoxIndex = comboBox_customerState.FindStringExact(sRetrievedCustomerState);
            if (comboBoxIndex!=-1)
            {
                comboBox_customerState.SelectedIndex = comboBoxIndex;
            }
            else
            {
                // shouldn't happen
                setUI();
            }

            textBox_itemProductID.Enabled = true;

            textBox_customerAccNo.Enabled = false;
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
                    Configuration.currentProgramState = Model.ProgramState.LOGGED_OUT;

                    logger.Info("Logging out");
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
                Configuration.currentProgramState = Model.ProgramState.LOGGED_OUT;

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
            toolStripStatusLabel_state.Text = "Member Sale";
            logger.Info("Initiating member sale");

            button_clearSale.Enabled = true;

            // get customer data
            textBox_itemProductID.Enabled = false;
            textBox_customerAccNo.Enabled = true;
            button_findCustomer.Enabled = true;
        }

        private void textBox_itemQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_itemProductID_TextChanged(object sender, EventArgs e)
        {
            if (textBox_itemProductID.Text!=string.Empty)
            {
                if (currentState == State.SALE_MEMBER || currentState == State.SALE_NON_MEMBER)
                {
                    // enable "Add Item" button and item quantity textbox
                    button_addItem.Enabled = true;
                    textBox_itemQuantity.Enabled = true;

                    textBox_itemQuantity.Text = "1";
                }

                button_priceLookup.Enabled = true;
            }
            else
            {
                button_addItem.Enabled = false;
                button_priceLookup.Enabled = false;

                textBox_itemQuantity.Text = string.Empty;
            }
        }

        private void listView_sales_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numberSelectedItems = listView_sales.SelectedItems.Count;

            switch (numberSelectedItems)
            {
                case 0:
                    // display total cost of items in cart
                    displayTotal();

                    button_removeItem.Enabled = false;

                    button_Discount.Enabled = false;

                    textBox_itemQuantity.ReadOnly = false;
                    textBox_itemQuantity.Text = string.Empty;

                    break;

                case 1:
                    // display price
                    richTextBox_itemPrice.Text = listView_sales.SelectedItems[0].SubItems[4].Text;

                    button_removeItem.Enabled = true;

                    // only Admins can approve discounts
                    if (Configuration.USER_LEVEL == Configuration.Role.ADMIN)
                    {
                        button_Discount.Enabled = true;
                    }
                    else
                    {
                        button_Discount.Enabled = false;
                    }

                    // cannot add items
                    button_addItem.Enabled = false;
                    textBox_itemProductID.Text = string.Empty;
                    // display quantity in quantity textbox, which is readonly
                    textBox_itemQuantity.ReadOnly = true;
                    textBox_itemQuantity.Text = listView_sales.SelectedItems[0].SubItems[2].Text;

                    //string priceDisplayed = string.Format("{0:C}", (listView_sales.SelectedItems[0].SubItems[4].Text));
                    //richTextBox_itemPrice.Text = priceDisplayed;
                    double selectedItemTotal = Convert.ToDouble(listView_sales.SelectedItems[0].SubItems[4].Text);
                    richTextBox_itemPrice.Text = selectedItemTotal.ToString("C", new CultureInfo("en-AU"));

                    break;

                default:
                    // cannot select multiple items
                    deselectAllItems();

                    button_removeItem.Enabled = false;

                    button_Discount.Enabled = false;

                    break;
            }
        }

        private void button_removeItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView_sales.SelectedItems)
            {
                listView_sales.Items.Remove(selectedItem);
            }

            // checkout button
            if (listView_sales.Items.Count > 0)
            {
                button_checkout.Enabled = true;
            }
            else
            {
                button_checkout.Enabled = false;
            }

            displayTotal();
        }

        // "Checkout" button click event handler
        private async void button_checkout_Click(object sender, EventArgs e)
        {
            // create invoice object
            // implement Excel invoices for now
            // TODO: implement spreadsheet invoices, based on a choice
            // PDFInvoice invoice = new PDFInvoice();
            View.ExcelInvoice invoice = new View.ExcelInvoice();
            invoice.transactions = new List<Transaction>();

            // calculate total and ask user for confirmation
            float fTotal = 0;
            foreach (ListViewItem item in listView_sales.Items)
            {
                float fItemTotal = float.Parse(item.SubItems[4].Text);
                fTotal += fItemTotal;
            }
            string sTotal = fTotal.ToString();
            DialogResult dialogResult = MessageBox.Show("Total: " + sTotal + " Checkout now? Clicking NO will clear sale", "Retail POS",
                                                        MessageBoxButtons.YesNoCancel);
            if (dialogResult==DialogResult.Yes)
            {
                try
                {
                    // collect items for transactions
                    foreach (ListViewItem itemInCart in listView_sales.Items)
                    {
                        // get product ID number and quantity for each
                        string id = itemInCart.SubItems[0].Text;
                        int quantity = Int32.Parse(itemInCart.SubItems[2].Text);

                        // get the staff ID of the salesperson
                        int staffID = Configuration.STAFF_ID;

                        // create the transaction
                        Transaction transaction = new Transaction();

                        await Task.Run(() =>
                        {
                            // retrieve product information
                            // run this task in a separate thread
                            transaction.product = POS.Configuration.productOps.getProduct(id);
                        });

                        // set the timestamp
                        transaction.Timestamp = System.DateTime.Now.ToString("F");

                        await Task.Run(() =>
                        {
                            // retrieve salesperson information
                            // run this task in a separate thread
                            transaction.staff = POS.Configuration.staffOps.getStaff(staffID);
                        });

                        switch (currentState)
                        {
                            case State.SALE_MEMBER:
                                // the customer is a member
                                // get their details and add them to the transaction information 
                                int customerID = Int32.Parse(textBox_customerAccNo.Text);
                                await Task.Run(() =>
                                {
                                    // run this task in a separate thread
                                    transaction.customer = POS.Configuration.customerOps.getCustomer(customerID);
                                });

                                break;

                            case State.SALE_NON_MEMBER:
                                transaction.customer = null;
                                break;
                            default:
                                // not valid
                                break;
                        }

                        for (int i = 1; i <= quantity; i++)
                        {
                            await Task.Run(() =>
                            {
                                // run this task in a separate thread
                                transController.addTransaction(transaction);
                            });

                            // add information to invoice
                            invoice.addEntry(transaction);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // something bad happened
                    // tell the user and the logger
                    string checkoutFailureMessage = "Checkout failed: " + ex.Message;
                    MessageBox.Show(checkoutFailureMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error(ex, checkoutFailureMessage);
                    logger.Error("Stack Trace: " + ex.StackTrace);

                    // nothing more we can do
                    return;
                }

                // at this point, the checkout succeeded
                // tell the user and the logger
                string checkoutSuccessMessage = "Checkout successful";
                MessageBox.Show(checkoutSuccessMessage, "Retail POS", MessageBoxButtons.OK);
                logger.Info(checkoutSuccessMessage);

                // now save the invoice
                // TODO: perhaps display invoice on screen
                try
                {
                    invoice.generateSpreadsheet();
                    invoice.save();
                }
                catch (Exception ex)
                {
                    // it failed
                    // tell the user and the logger
                    string saveInvoiceFailureMessage = "Error saving invoice: " + ex.Message;
                    MessageBox.Show(saveInvoiceFailureMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error(ex, saveInvoiceFailureMessage);
                    logger.Error("Stack trace: " + ex.StackTrace);
                }

                // reset
                setUI();
            }
            else if (dialogResult==DialogResult.No)
            {
                // clear sale
                // tell the logger
                logger.Info("Clear sale requested");

                // clean up and reset
                //richTextBox_itemPrice.Text = "0.00";
                setUI();
            }
            else if (dialogResult==DialogResult.Cancel)
            {
                // cancelling
                // tell the logger
                logger.Info("Cancel checkout requested");

                return;
            }
        }

        private void displayTotal()
        {
            if (listView_sales.Items.Count > 0)
            {
                float fTotalCost = 0;
                foreach (ListViewItem item in listView_sales.Items)
                {
                    // get total price of current item
                    string sCurrentItemCost = item.SubItems[4].Text;
                    float fCurrentItemCost = float.Parse(sCurrentItemCost);
                    fTotalCost += fCurrentItemCost;
                }

                //richTextBox_itemPrice.Text = "Total: " + fTotalCost;
                string priceDisplayed = "Total: " + fTotalCost.ToString("C2", new CultureInfo("en-AU"));
                richTextBox_itemPrice.Text = priceDisplayed;

                button_removeItem.Enabled = true;
            }
            else
            {
                //richTextBox_itemPrice.Text = "0.00";
                double priceDisplayed = 0.00;
                richTextBox_itemPrice.Text = priceDisplayed.ToString("C2", new CultureInfo("en-AU"));

                button_removeItem.Enabled = false;
            }
        }

        // "Discount" button click event handler
        private void button_Discount_Click(object sender, EventArgs e)
        {
            // create discount form and show it
            string selectedItemID = listView_sales.SelectedItems[0].SubItems[0].Text;
            float selectedItemPrice = float.Parse(listView_sales.SelectedItems[0].SubItems[4].Text);
            DiscountForm discountForm = new DiscountForm(selectedItemID, selectedItemPrice);
            discountForm.Show();

            // subscribe to events from the discount form
            discountForm.OnApplyDiscount += new EventHandler<DiscountForm.DiscountEventArgs>(applyDiscount);
        }

        // discount event handler
        private void applyDiscount(object sender, DiscountForm.DiscountEventArgs e)
        {
            // extract data from event arguments
            string itemNumber = e.getItemIDNumber();
            float totalItemPrice = e.getPrice();

            // find the correct item in the list
            foreach (ListViewItem item in listView_sales.Items)
            {
                if (item.SubItems[0].Text.Equals(itemNumber))
                {
                    // apply discount
                    item.SubItems[4].Text = totalItemPrice.ToString();

                    // reselect item
                    ListViewItem currSelectedItem = listView_sales.SelectedItems[0];
                    currSelectedItem.Selected = false;
                    currSelectedItem.Selected = true;

                    break;
                }
            }
        }

        // "Lookup item" button click event handler
        private async void button_priceLookup_Click(object sender, EventArgs e)
        {
            string productIDnumber = textBox_itemProductID.Text;
            Product retrievedProduct = new Product();
            try
            {
                await Task.Run(() =>
                {
                    // ask the model for the product information
                    retrievedProduct = (Product)(POS.Configuration.productOps.getProduct(productIDnumber));
                });
            }
            catch (Exception ex)
            {
                // database error
                // tell the user and the logger
                string dbErrorMessage = "Error retrieving data from database: " + ex.Message;
                MessageBox.Show(dbErrorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, dbErrorMessage + ": " + ex.Message);
                logger.Error("Stack trace: " + ex.StackTrace);

                // nothing more we can do
                return;
            }
            
            if (retrievedProduct==null)
            {
                // could not retrieve product
                // tell the user and the logger
                string nullProductMessage = "Error: Could not find specified product";
                MessageBox.Show(nullProductMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                logger.Info(nullProductMessage);

                // nothing more we can do
                return;
            }
            else
            {
                // at this point, it succeeded
                // tell the user and the logger
                MessageBox.Show("Product ID: " + productIDnumber + "\nDescription: " + retrievedProduct.Description +
                                "\nPrice: " + retrievedProduct.price.ToString(), "Item Lookup", MessageBoxButtons.OK);
                logger.Info("Successfully retrieved product information");

            }

            textBox_itemProductID.Text = string.Empty;
        }

        // TODO: format number cells as numbers in the spreadsheet
        // "export" menu item click events
        // create an "export factory"
        SpreadsheetExportFactory spreadsheetExportFactory = new SpreadsheetExportFactory();
        private void staffToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // export staff data from database

            // create an instance of the view
            SpreadsheetExport exportView = spreadsheetExportFactory.getSpreadsheetExportView("Staff");

            // execute it
            executeExportSpreadsheetView(exportView);
        }
        private void customersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // export customer data from database

            // create an instance of the view
            SpreadsheetExport exportView = spreadsheetExportFactory.getSpreadsheetExportView("Customer");

            // execute it
            executeExportSpreadsheetView(exportView);
        }
        private void productsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // export product data from database

            // create an instance of the view
            SpreadsheetExport exportView = spreadsheetExportFactory.getSpreadsheetExportView("Product");

            // execute it
            executeExportSpreadsheetView(exportView);
        }
        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // export transaction data from database

            // create an instance of the view
            SpreadsheetExport exportView = spreadsheetExportFactory.getSpreadsheetExportView("Transaction");

            // execute it
            executeExportSpreadsheetView(exportView);
        }

        /// <summary>
        /// Create the export spreadsheet, then show a dialog to save it.
        /// </summary>
        /// <param name="exportView">SpreadsheetExport object. Can pass in objects of derived classes (export subtypes)</param>
        private void executeExportSpreadsheetView(SpreadsheetExport exportView)
        {
            // create the spreadsheet
            exportView.retrieveData();
            exportView.prepareSpreadsheet();

            try
            {
                // save the spreadsheet
                exportView.saveSpreadsheet();
                
            }
            catch (Exception ex)
            {
                // it failed
                // tell the user and the logger
                System.Windows.Forms.MessageBox.Show("Error saving spreadsheet file", "Retail POS",
                                                     System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Error);
                logger.Error(ex, "Error saving spreadsheet file: " + ex.Message);
                logger.Error("Stack trace: ", ex.StackTrace);

                // nothing more we can do
                return;
            }
            
            /*
            // at this point, it succeeded
            // tell the user and the logger
            System.Windows.Forms.MessageBox.Show("Saved spreadsheet file", "Retail POS",
                                                 System.Windows.Forms.MessageBoxButtons.OK,
                                                 System.Windows.Forms.MessageBoxIcon.Information);
            logger.Info("Saved spreadsheet file");
            */
        }

        // "import" menu item click events
        Controller.SpreadsheetImportFactory spreadsheetImportFactory = new Controller.SpreadsheetImportFactory();
        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create an instance of the Controller
            Controller.SpreadsheetImport spreadsheetImportController = spreadsheetImportFactory.getImportSpreadsheetController("Product");

            // execute it
            executeImportSpreadsheetController(spreadsheetImportController);
        }

        private void customersToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // create an instance of the Controller
            Controller.SpreadsheetImport spreadsheetImportController = spreadsheetImportFactory.getImportSpreadsheetController("Customer");

            // execute it
            executeImportSpreadsheetController(spreadsheetImportController);
        }

        private void staffToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // create an instance of the Controller
            Controller.SpreadsheetImport spreadsheetImportController = spreadsheetImportFactory.getImportSpreadsheetController("Staff");

            // execute it
            executeImportSpreadsheetController(spreadsheetImportController);
        }

        private void productsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // create an instance of the Controller
            Controller.SpreadsheetImport spreadsheetImportController = spreadsheetImportFactory.getImportSpreadsheetController("Product");

            // execute it
            executeImportSpreadsheetController(spreadsheetImportController);
        }

        /// <summary>
        /// Show a dialog to import the spreadsheet, then import/update the data into the database.
        /// </summary>
        /// <param name="importController">Controller.SpreadsheetImport</param>
        private void executeImportSpreadsheetController(Controller.SpreadsheetImport importController)
        {
            // TODO: continue to split this to catch different exception types (database error, file error)
            try
            {
                // execute it
                importController.openSpreadsheet();
                importController.importUpdate();
            }
            catch (System.IO.InvalidDataException invDatEx)
            {
                // found invalid data in spreadsheet
                string importDataErrorMessage = "Error importing " + importController.importType + " data: " + invDatEx.Message;
                System.Windows.Forms.MessageBox.Show(importDataErrorMessage, "Retail POS",
                                                     System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Error);
                logger.Error(invDatEx, importDataErrorMessage);
                logger.Error("Stack trace: " + invDatEx.StackTrace);

                return;
            }
            catch (Exception ex)
            {
                // some other error
                // tell the user and the logger
                string importDataErrorMessage = "Error importing " + importController.importType + " data: " + ex.Message;
                System.Windows.Forms.MessageBox.Show(importDataErrorMessage, "Retail POS",
                                                     System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Error);
                logger.Error(ex, importDataErrorMessage);
                logger.Error("Stack trace: " + ex.StackTrace);

                return;
            }

            // at this point, it succeeded
            // tell the user and the logger
            string importDataSuccessMessage = "Successfully imported data";
            System.Windows.Forms.MessageBox.Show(importDataSuccessMessage, "Retail POS",
                                                 System.Windows.Forms.MessageBoxButtons.OK,
                                                 System.Windows.Forms.MessageBoxIcon.Information);
            logger.Debug(importDataSuccessMessage);
        }

        private void textBox_itemProductID_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData==Keys.Tab)
            {
                // make sure the text box is not empty, and in a sale state
                if (((textBox_itemProductID.Text!=null) && !(textBox_itemProductID.Text.Equals(string.Empty))) && ((this.currentState==State.SALE_MEMBER) || (this.currentState==State.SALE_NON_MEMBER)))
                {
                    // trigger the add item button click event
                    button_addItem.PerformClick();
                    textBox_itemProductID.Select();
                }
            }
        }

        private void textBox_itemQuantity_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                // make sure the text box is not empty, and in a sale state
                if (((textBox_itemProductID.Text != null) && !(textBox_itemProductID.Text.Equals(string.Empty))) && ((this.currentState == State.SALE_MEMBER) || (this.currentState == State.SALE_NON_MEMBER)))
                {
                    // trigger the add item button click event
                    button_addItem.PerformClick();
                    textBox_itemProductID.Select();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private void viewTransactionHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View.ViewTransactionsForm transactionsListForm = new ViewTransactionsForm();
            transactionsListForm.Show();
        }
    }
}
