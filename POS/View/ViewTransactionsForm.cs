using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            listView_transactions.Columns.Add("Staff ID");
            listView_transactions.Columns.Add("Product ID");

            listView_transactions.View = System.Windows.Forms.View.Details;
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void populateView()
        { }
    }
}
