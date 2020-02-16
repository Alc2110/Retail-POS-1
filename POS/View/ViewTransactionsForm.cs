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
            populateView(TransactionOps.getAllTransactions());

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
                string[] itemArr = new string[2];
                itemArr[0] = transaction.getTransactionID().ToString();
                itemArr[1] = transaction.getTimestamp();
                // TODO: complete this
                ListViewItem transactionItem = new ListViewItem(itemArr);
                listView_transactions.Items.Add(transactionItem);
            }
        }
    }
}
