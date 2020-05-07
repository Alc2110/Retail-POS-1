using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.ServiceLayer;
using Model.ObjectModel;

namespace POS.View
{
    public partial class ViewTransactionsForm : Form
    {
        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events fired by the model
        private delegate void populateTransactionsViewDelegate(object sender, GetAllTransactionsEventArgs args);

        public ViewTransactionsForm()
        {
            InitializeComponent();

            logger.Info("Initialising view transactions form");

            // prepare the listView
            listView_transactions.Columns.Add("Transaction ID");
            listView_transactions.Columns.Add("Timestamp");
            listView_transactions.Columns.Add("Customer ID");
            listView_transactions.Columns.Add("Customer full name");
            listView_transactions.Columns.Add("Staff ID");
            listView_transactions.Columns.Add("Staff full name");
            listView_transactions.Columns.Add("Product ID");
            listView_transactions.Columns.Add("Product description");
            listView_transactions.Columns.Add("Product price");
            listView_transactions.View = System.Windows.Forms.View.Details;
            listView_transactions.GridLines = true;
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            logger.Info("Closing view transactions form");

            this.Close();
        }
        #endregion

        private void transactionEventHandler(object sender, GetAllTransactionsEventArgs args)
        {
            if (listView_transactions.InvokeRequired)
            {
                listView_transactions.Invoke(new populateTransactionsViewDelegate(populateView), new object[] { sender, args });
            }
            else
            {
                populateView(sender, args);
            }
        }

        private void populateView(object sender, GetAllTransactionsEventArgs args)
        {
            // tell the listView it is being updated
            listView_transactions.BeginUpdate();

            // clear the list
            foreach (ListViewItem item in listView_transactions.Items)
            {
                listView_transactions.Items.Remove(item);
            }

            // populate it
            foreach (var transaction in args.getList())
            {
                string[] itemArr = new string[9];

                itemArr[0] = transaction.TransactionID.ToString();
                itemArr[1] = transaction.Timestamp;

                if (transaction.customer != null)
                {
                    itemArr[2] = transaction.customer.CustomerID.ToString();
                    itemArr[3] = transaction.customer.FullName;
                }

                itemArr[4] = transaction.staff.StaffID.ToString();
                itemArr[5] = transaction.staff.FullName;
                itemArr[6] = transaction.product.ProductIDNumber;
                itemArr[7] = transaction.product.Description;
                itemArr[8] = transaction.product.price.ToString();
                
                ListViewItem transactionItem = new ListViewItem(itemArr);
                listView_transactions.Items.Add(transactionItem);
            }

            // tell the listView it is ready
            listView_transactions.EndUpdate();
        }

        private async void ViewTransactionsForm_Load(object sender, EventArgs e)
        {
            // subscribe to Model events
            POS.Configuration.transactionOps.GetAllTransactions += new EventHandler<GetAllTransactionsEventArgs>(transactionEventHandler);

            // populate the list upon loading
            try
            {
                // run this operation in a separate thread
                await Task.Run(() =>
                {
                    POS.Configuration.transactionOps.getAllTransactions();
                });
            }
            catch (Exception ex)
            {
                // it failed
                // tell the user and the logger
                string errorMessage = "Error: could not retrieve data from database: " + ex.Message;
                logger.Error(ex, errorMessage);
                logger.Error("Stack trace: " + ex.StackTrace);
            }
        }
    }
}
