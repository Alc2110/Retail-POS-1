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

        public ViewTransactionsForm()
        {
            InitializeComponent();

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

            // populate the list upon loading
            try
            {
                populateView(TransactionOps.getAllTransactions());
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                // it failed
                // tell the user and the logger
                string errorMessage = "Error: could not retrieve data from database: " + sqlEx.Message;
                logger.Error(sqlEx, errorMessage);
                logger.Error("Stack trace: " + sqlEx.StackTrace);

                // nothing more we can do at this point
                // TODO: do we want to close the window at this point?
                this.Close();
            }

            // subscribe to Model events
            TransactionOps.OnGetAllTransactions += new EventHandler<GetAllTransactionsEventArgs>(transactionEventHandler);
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void transactionEventHandler(object sender, GetAllTransactionsEventArgs e)
        {
            populateView(e.getList());
        }

        private void populateView(List<Transaction> transactions)
        {
            // clear the list
            foreach (ListViewItem item in listView_transactions.Items)
            {
                listView_transactions.Items.Remove(item);
            }

            // populate it
            foreach (Transaction transaction in transactions)
            {
                string[] itemArr = new string[9];

                itemArr[0] = transaction.getTransactionID().ToString();
                itemArr[1] = transaction.getTimestamp();

                if (transaction.getCustomer() != null)
                {
                    itemArr[2] = transaction.getCustomer().getID().ToString();
                    itemArr[3] = transaction.getCustomer().getName();
                }

                itemArr[4] = transaction.getStaff().getID().ToString();
                itemArr[5] = transaction.getStaff().getName();

                itemArr[6] = transaction.getProduct().getProductIDNumber();
                itemArr[7] = transaction.getProduct().getDescription();
                itemArr[8] = transaction.getProduct().getPrice().ToString();
                
                ListViewItem transactionItem = new ListViewItem(itemArr);
                listView_transactions.Items.Add(transactionItem);
            }
        }
    }
}
