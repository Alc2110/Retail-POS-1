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
using Model.ServiceLayer;
using Model.ObjectModel;

namespace POS.View
{
    public partial class ViewProductsForm : Form
    {
        // controller dependency injection
        private ProductController controller;

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ViewProductsForm()
        {
            InitializeComponent();

            // prepare the listView
            listView_products.Columns.Add("Product ID Number");
            listView_products.Columns.Add("Description");
            listView_products.Columns.Add("Quantity");
            listView_products.Columns.Add("Price");
            listView_products.View = System.Windows.Forms.View.Details;
            listView_products.GridLines = true;

            // can't delete anything until something is selected
            button_deleteSelectedProduct.Enabled = false;

            // controller dependency injection
            controller = ProductController.getInstance();

            // subscribe to Model events
            ProductOps.OnGetAllProducts += new EventHandler<GetAllProductsEventArgs>(productEventHandler);

            // populate the list upon loading
            try
            {
                populateView(ProductOps.getAllProducts());
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                // it failed
                // tell the user and the logger
                string errorMessage = "Error: failed to retrieve data from database";
                MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(errorMessage);

                // nothing more we can do
                // TODO: do we want to close the window at this point?
                this.Close();
            }
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
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    // it failed
                    // tell the user and the logger
                    string errorMessage = "Error: could not delete product: " + sqlEx.Message;
                    MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error(errorMessage);
                    logger.Error("Stack trace: " + sqlEx.StackTrace);
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

        private void productEventHandler(object sender, GetAllProductsEventArgs e)
        {
            populateView(e.getList());
        }

        private void populateView(List<Product> products)
        {
            // clean the list view
            foreach (ListViewItem item in listView_products.Items)
            {
                listView_products.Items.Remove(item);
            }

            // populate it
            foreach (Product product in products)
            {
                string[] itemArr = new string[4];
                itemArr[0] = product.getProductIDNumber();
                itemArr[1] = product.getDescription();
                itemArr[2] = product.getQuantity().ToString();
                itemArr[3] = product.getPrice().ToString();

                ListViewItem item = new ListViewItem(itemArr);
                listView_products.Items.Add(item);
            }
        }
    }
}
