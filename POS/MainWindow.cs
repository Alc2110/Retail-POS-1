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

namespace POS
{
    public partial class MainWindow : Form
    {
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
            comboBox_customerState.Items.Add("NSW");
            comboBox_customerState.Items.Add("Vic");
            comboBox_customerState.Items.Add("Qld");
            comboBox_customerState.Items.Add("ACT");
            comboBox_customerState.Items.Add("NT");
            comboBox_customerState.Items.Add("SA");
            comboBox_customerState.Items.Add("WA");
            comboBox_customerState.Items.Add("Other");

            // TODO: implement this
            scriptingToolStripMenuItem.Enabled = false;

            listView_sales.GridLines = true;

            setUI();

            // controller dependency injection
            transController = TransactionController.getInstance();
        }

        public enum State
        {
            READY,
            SALE_MEMBER,
            SALE_NON_MEMBER,
        }

        private void setUI()
        {
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

            button_newSaleMember.Enabled = true;
            button_newSaleNonMember.Enabled = true;

            textBox_itemProductID.Enabled = true;
            textBox_itemQuantity.Enabled = false;

            richTextBox_itemPrice.Text = "0.00";

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
       
        private void button_addItem_Click(object sender, EventArgs e)
        {
            // retrieve product record from database, for this ID 
            string productID = textBox_itemProductID.Text;
            Product retrievedProduct = ProductOps.getProduct(productID);

            logger.Info("Retrieving product from database, for product ID: " + productID);

            // could not find product
            if (retrievedProduct==null)
            {
                // tell the user and the logger
                string nullProductMessage = "Could not find specified product";
                MessageBox.Show(nullProductMessage, "Retail POS", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                logger.Warn(nullProductMessage);

                textBox_itemProductID.Text = string.Empty;

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

                    logger.Info("Item of this type is already in list");

                    // just increment the total count, and update total item cost
                    string sQuantity = listItem.SubItems[2].Text;
                    int iQuantity = Int32.Parse(sQuantity);
                    //listItem.SubItems[2].Text = (iQuantity += 1).ToString();
                    listItem.SubItems[2].Text = (iQuantity += Int32.Parse(textBox_itemQuantity.Text)).ToString();
                    
                    string sCost = listItem.SubItems[4].Text;
                    float fCost = float.Parse(sCost);
                    string sPrice = listItem.SubItems[3].Text;
                    float fPrice = float.Parse(sPrice);
                    listItem.SubItems[4].Text = (fPrice + fCost).ToString();

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
                itemArr[0] = retrievedProduct.getProductIDNumber();
                itemArr[1] = retrievedProduct.getDescription();
                //itemArr[2] = "1";// quantity
                itemArr[2] = textBox_itemQuantity.Text;
                itemArr[4] = retrievedProduct.getPrice().ToString();// total
                itemArr[3] = retrievedProduct.getPrice().ToString();
                ListViewItem item = new ListViewItem(itemArr);
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
            button_addItem.Enabled = false;
            textBox_itemProductID.Text = string.Empty;
            textBox_itemQuantity.Text = string.Empty;

            deselectAllItems();

            displayTotal();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        { 
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
            // get customer data
            string customerAccNumber = textBox_customerAccNo.Text;
            logger.Info("Attempting to find customer with account number: " + customerAccNumber);
            Customer retrievedCustomer = CustomerOps.getCustomer(Int32.Parse(customerAccNumber));

            // could not find customer
            if (retrievedCustomer==null)
            {
                string nullCustomerMessage = "Could not find specified customer";
                MessageBox.Show(nullCustomerMessage, "Retail POS",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                logger.Warn(nullCustomerMessage);

                return;
            }

            // found the customer
            logger.Info("Found customer record");
            textBox_customerName.Text = retrievedCustomer.getName();
            textBox_customerPhone.Text = retrievedCustomer.getPhoneNumber();
            textBox_customerEmail.Text = retrievedCustomer.getEmail();
            textBox_customerAddress.Text = retrievedCustomer.getAddress();
            textBox_customerCity.Text = retrievedCustomer.getCity();
            textBox_customerPostCode.Text = retrievedCustomer.getPostcode().ToString();
            
            switch (retrievedCustomer.getState())
            {
                case Customer.States.ACT:
                    comboBox_customerState.SelectedIndex = comboBox_customerState.FindStringExact("ACT");
                    break;
                case Customer.States.NSW:
                    comboBox_customerState.SelectedIndex = comboBox_customerState.FindStringExact("NSW");
                    break;
                case Customer.States.Vic:
                    comboBox_customerState.SelectedIndex = comboBox_customerState.FindStringExact("Vic");
                    break;
                case Customer.States.Qld:
                    comboBox_customerState.SelectedIndex = comboBox_customerState.FindStringExact("Qld");
                    break;
                case Customer.States.NT:
                    comboBox_customerState.SelectedIndex = comboBox_customerState.FindStringExact("NT");
                    break;
                case Customer.States.Tas:
                    comboBox_customerState.SelectedIndex = comboBox_customerState.FindStringExact("Tas");
                    break;
                case Customer.States.SA:
                    comboBox_customerState.SelectedIndex = comboBox_customerState.FindStringExact("SA");
                    break;
                case Customer.States.WA:
                    comboBox_customerState.SelectedIndex = comboBox_customerState.FindStringExact("WA");
                    break;
                case Customer.States.Other:
                    comboBox_customerState.SelectedIndex = comboBox_customerState.FindStringExact("Other");
                    break;
                default:
                    // this shouldn't happen, but handle it anyway
                    throw new Exception("Invalid data");
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

        private void button_checkout_Click(object sender, EventArgs e)
        {
            // create invoice object
            // implement PDF invoices for now
            // TODO: implement spreadsheet invoices, based on a choice
            PDFInvoice invoice = new PDFInvoice();

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
                // collect items for transactions
                Dictionary<string, int> saleItems = new Dictionary<string, int>();
                foreach (ListViewItem item in listView_sales.Items)
                {
                    // get ID and quantity of each
                    saleItems.Add(item.SubItems[0].Text, Int32.Parse(item.SubItems[2].Text));
                }

                // transactions
                ValueTuple<int, int, Dictionary<string, int>> currTransaction;
                switch (currentState)
                {
                    // this sale is for a member
                    case State.SALE_MEMBER:
                        int staffID = Configuration.STAFF_ID;
                        int customerID = Int32.Parse(textBox_customerAccNo.Text);
                        currTransaction = (staffID, customerID, saleItems);
                        try
                        {
                            // create the transaction in the database
                            transController.addTransaction(currTransaction);

                            // retrieve the customer object for the invoice
                            invoice.customer = CustomerOps.getCustomer(customerID);
                            // retrieve the staff object for the invoice
                            invoice.salesperson = StaffOps.getStaff(staffID);
                        }
                        catch (Exception ex)
                        {
                            // it failed
                            // tell the user and the logger
                            string transactionErrorMessage = "Error in transaction: " + ex.Message;
                            MessageBox.Show(transactionErrorMessage, "Retail POS",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            logger.Error(ex, transactionErrorMessage);
                            logger.Error("Stack trace: " + ex.StackTrace);

                            return;
                        }

                        break;
                    
                    // this sale is for a non-member
                    case State.SALE_NON_MEMBER:
                        staffID = Configuration.STAFF_ID;
                        currTransaction = (staffID, 0, saleItems);
                        invoice.customer = null;
                        try
                        {
                            // create the transaction in the database
                            transController.addTransaction(currTransaction);

                            // retrieve the staff object for the invoice
                            invoice.salesperson = StaffOps.getStaff(staffID);
                        }
                        catch (Exception ex)
                        {
                            // it failed
                            // tell the user and the logger
                            string transactionErrorMessage = "Error in transaction: " + ex.Message;
                            MessageBox.Show(transactionErrorMessage, "Retail POS",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            logger.Error(ex, transactionErrorMessage);
                            logger.Error("Stack trace: " + ex.StackTrace);

                            return;
                        }

                        break;

                    default:

                        break;
                }

                // at this point, the checkout succeeded
                // tell the user and the logger
                MessageBox.Show("Checkout successful", "Retail POS", MessageBoxButtons.OK);

                // now deal with the invoice
                // TODO: perhaps display invoice on screen
                invoice.save();

                // reset
                setUI();
            }
            else if (dialogResult==DialogResult.No)
            {
                // clear sale
                richTextBox_itemPrice.Text = "0.00";
                setUI();
            }
            else if (dialogResult==DialogResult.Cancel)
            {
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

                richTextBox_itemPrice.Text = "Total: " + fTotalCost;
                button_removeItem.Enabled = true;
            }
            else
            {
                richTextBox_itemPrice.Text = "0.00";
                button_removeItem.Enabled = false;
            }
        }

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

        private void button_priceLookup_Click(object sender, EventArgs e)
        {
            string productIDnumber = textBox_itemProductID.Text;
            Product retrievedProduct = ProductOps.getProduct(productIDnumber);

            if (retrievedProduct==null)
            {
                // could not retrieve product
                // tell the user and the logger
                string nullProductMessage = "Could not find specified product";
                MessageBox.Show(nullProductMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                logger.Warn(nullProductMessage);

                return;
            }
            else
            {
                // it succeeded
                // tell the user and the logger
                MessageBox.Show("Product ID: " + productIDnumber + "\nDescription: " + retrievedProduct.getDescription() +
                                "\nPrice: " + retrievedProduct.getPrice().ToString(), "Item Lookup", MessageBoxButtons.OK);
                logger.Info("Successfully retrieved product information");

            }

            textBox_itemProductID.Text = string.Empty;
        }

        // "export" menu item click events
        // create a factory
        SpreadsheetExportFactory spreadsheetExportFactory = new SpreadsheetExportFactory();
        private void staffToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // export staff data from database

            // create an instance of the view
            SpreadsheetExport exportView = spreadsheetExportFactory.getSpreadsheetExportView("Staff");

            executeExportSpreadsheetView(exportView);
        }
        private void customersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // export customer data from database

            // create an instance of the view
            SpreadsheetExport exportView = spreadsheetExportFactory.getSpreadsheetExportView("Customer");

            executeExportSpreadsheetView(exportView);
        }
        private void productsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // export product data from database

            // create an instance of the view
            SpreadsheetExport exportView = spreadsheetExportFactory.getSpreadsheetExportView("Product");

            // create the spreadsheet
            executeExportSpreadsheetView(exportView);
        }
        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // export transaction data from database

            // create an instance of the view
            SpreadsheetExport exportView = spreadsheetExportFactory.getSpreadsheetExportView("Transaction");

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

                return;
            }

            // at this point, it succeeded
            // tell the user and the logger
            System.Windows.Forms.MessageBox.Show("Saved spreadsheet file", "Retail POS",
                                                 System.Windows.Forms.MessageBoxButtons.OK,
                                                 System.Windows.Forms.MessageBoxIcon.Information);
            logger.Info("Saved spreadsheet file");
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create an instance of the Controller
            //Controller.ProductsSpreadsheetImport import = new POS.Controller.ProductsSpreadsheetImport();

            // execute it
            try
            {
                /*
                import.openSpreadsheet();
                import.importUpdate();
                */
            }
            catch (Exception ex)
            {
                // it failed
                // tell the user and the logger
                System.Windows.Forms.MessageBox.Show("Error updating product data: " + ex.Message, "Retail POS",
                                                     System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Error);
                logger.Error(ex, "Error updating product data: " + ex.Message);
                logger.Error("Stack trace: " + ex.StackTrace);

                return;
            }

            // at this point, it succeeded
            // tell the user and the logger
            System.Windows.Forms.MessageBox.Show("Successfully updated product data", "Retail POS",
                                                System.Windows.Forms.MessageBoxButtons.OK,
                                                System.Windows.Forms.MessageBoxIcon.Information);
            logger.Info("Updated product data");
        }

        private void customersToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // create an instance of the Controller
            Controller.CustomerSpreadsheetImport import = new Controller.CustomerSpreadsheetImport("Customer");

            // execute it
            import.openSpreadsheet();
            import.importUpdate();
        }
    }
}
