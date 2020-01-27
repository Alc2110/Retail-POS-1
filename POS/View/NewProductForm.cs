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
    public partial class NewProductForm : Form
    {
        public NewProductForm()
        {
            InitializeComponent();
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_add_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
