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
    public partial class ViewStaffForm : Form
    {
        public ViewStaffForm()
        {
            InitializeComponent();

            listView_staff.Columns.Add("ID");
            listView_staff.Columns.Add("Full name");
            listView_staff.Columns.Add("Password");
            listView_staff.Columns.Add("Privelege");

            listView_staff.View = System.Windows.Forms.View.Details;
        }

        #region UI event handlers
        private void button_addNewStaff_Click(object sender, EventArgs e)
        {

        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_deleteSelectedStaff_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
