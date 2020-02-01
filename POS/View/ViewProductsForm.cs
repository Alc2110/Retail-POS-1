using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;

namespace POS.View
{
    public partial class ViewProductsForm : Form
    {
        // controller dependency injection
        private ProductController controller;

        public ViewProductsForm()
        {
            InitializeComponent();

            // prepare the listView
            listView_products.Columns.Add("Product ID");
            listView_products.Columns.Add("Description");
            listView_products.Columns.Add("Quantity");
            listView_products.Columns.Add("Price");

            listView_products.View = System.Windows.Forms.View.Details;

            // can't delete anything until something is selected
            button_deleteSelectedProduct.Enabled = false;

            // controller dependency injection
            controller = ProductController.getInstance();
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_addNewProduct_Click(object sender, EventArgs e)
        {
            NewProductForm newProductForm = new NewProductForm();
            newProductForm.Show();
        }

        private void button_deleteSelectedProduct_Click(object sender, EventArgs e)
        {
            if (controller!=null)
            {
                try
                {
                    int id;
                    if (Int32.TryParse(listView_products.SelectedItems[0].SubItems[0].Text, out id))
                    {
                        controller.deleteProduct(id);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void listView_products_SelectedIndexChanged(object sender, EventArgs e)
        {
            // only one item allowed to be selected at a time
            if (listView_products.SelectedItems.Count>0)
            {
                foreach (ListViewItem item in listView_products.SelectedItems)
                {
                    item.Selected = false;
                }
            }

            if (listView_products.SelectedItems.Count==1)
            {
                button_deleteSelectedProduct.Enabled = true;
            }
            else
            {
                button_deleteSelectedProduct.Enabled = false;
            }
        }
        #endregion

        private void populateView()
        { }
    }
}
