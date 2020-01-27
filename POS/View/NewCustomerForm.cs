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
    public partial class NewCustomerForm : Form
    {
        public NewCustomerForm()
        {
            InitializeComponent();

            comboBox_state.Items.Add("NSW");
            comboBox_state.Items.Add("SA");
            comboBox_state.Items.Add("Qld");
            comboBox_state.Items.Add("Vic");
            comboBox_state.Items.Add("ACT");
            comboBox_state.Items.Add("Tas");
            comboBox_state.Items.Add("WA");
            comboBox_state.Items.Add("NT");
            comboBox_state.Items.Add("Other");
        }

        #region UI event handlers
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_add_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
