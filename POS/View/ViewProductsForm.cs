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

        // delegate for handling events fired by the model
        private delegate void populateProductsViewDelegate(object sender, GetAllProductsEventArgs args);

        public ViewProductsForm()
        {
            InitializeComponent();

            logger.Info("Initialising view products form");

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
            controller = new ProductController();
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            logger.Info("Closing new products form");

            this.Close();
        }

        private void button_addNewProduct_Click(object sender, EventArgs e)
        {
            NewProductForm newProductForm = new NewProductForm();
            newProductForm.Show();
        }

        private async void button_deleteSelectedProduct_Click(object sender, EventArgs e)
        {
            if (controller!=null)
            {
                try
                {
                    // prepare the data
                    string idNumber = listView_products.SelectedItems[0].SubItems[0].Text;

                    // log it
                    logger.Info("Deleting product record: ");
                    logger.Info("ID: " + idNumber);

                    // run this operation in a separate thread
                    await Task.Run(() =>
                    {
                        controller.deleteProduct(idNumber);
                    });
                }
                catch (Exception ex)
                {
                    // it failed
                    // tell the user and the logger
                    string errorMessage = "Error: could not delete product: " + ex.Message;
                    MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error(errorMessage);
                    logger.Error("Stack trace: " + ex.StackTrace);

                    return;
                }

                // at this point, it succeeded
                // tell the user and the logger
                string successMessage = "Successfully deleted product record";
                MessageBox.Show(successMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logger.Info(successMessage);
            }
        }

        private void listView_products_SelectedIndexChanged(object sender, EventArgs e)
        {
            // only one item allowed to be selected at a time
            if (listView_products.SelectedItems.Count>1)
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

        private void productEventHandler(object sender, GetAllProductsEventArgs args)
        {
            if (listView_products.InvokeRequired)
            {
                listView_products.Invoke(new populateProductsViewDelegate(populateView), new object[] { sender, args });
            }
            else
            {
                populateView(sender, args);
            }
        }

        private void populateView(object sender, GetAllProductsEventArgs args)
        {
            List<Product> products = args.getList();

            // tell the listView it is being updated
            listView_products.BeginUpdate();

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

            // tell the listView it is ready
            listView_products.EndUpdate();
        }

        private async void ViewProductsForm_Load(object sender, EventArgs e)
        {
            // subscribe to Model events
            POS.Configuration.productOps.GetAllProducts += new EventHandler<GetAllProductsEventArgs>(productEventHandler);

            // populate the list upon loading
            try
            {
                // run this task in a separate thread
                await Task.Run(() =>
                {
                    Configuration.productOps.getAllProducts();
                });
            }
            catch (Exception ex)
            {
                // it failed
                // tell the user and the logger
                string errorMessage = "Error retrieving data from database: " + ex.Message;
                MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, errorMessage);
                logger.Error("Stack Trace: " + ex.StackTrace);
            }
        }
    }
}
