﻿using System;
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
    public partial class ViewProductsForm : Form
    {
        public ViewProductsForm()
        {
            InitializeComponent();
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_addNewProduct_Click(object sender, EventArgs e)
        {

        }

        private void button_deleteSelectedProduct_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
