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
    public partial class ViewCustomersForm : Form
    {
        public ViewCustomersForm()
        {
            InitializeComponent();

            listView_customers.Columns.Add("ID");
            listView_customers.Columns.Add("Full name");
            listView_customers.Columns.Add("Street address");
            listView_customers.Columns.Add("Phone number");
            listView_customers.Columns.Add("Email");
            listView_customers.Columns.Add("City");
            listView_customers.Columns.Add("State");
            listView_customers.Columns.Add("Postcode");
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
