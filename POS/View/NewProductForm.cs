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
    public partial class NewProductForm : Form
    {
        // controller dependency injection
        private ProductController controller;

        // get logger instance for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public NewProductForm()
        {
            InitializeComponent();

            logger.Info("Initialising new product form");

            // controller dependency injection
            controller = new ProductController();

            // cannot add anything yet
            button_add.Enabled = false;

            // event handlers for data entry controls
            textBox_ID.TextChanged += checkEntries;
            textBox_description.TextChanged += checkEntries;
            textBox_price.TextChanged += checkEntries;
            textBox_quantity.TextChanged += checkEntries;

            // status bar
            labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.IDLE_COLOUR, "Ready");
            labelProgressBar1.Value = 100;
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            logger.Info("Closing new product form");

            this.Close();
        }

        private async void button_add_Click(object sender, EventArgs e)
        {
            if (controller != null)
            {
                try
                {
                    // use busy cursor
                    this.UseWaitCursor = true;

                    // update the status bar
                    labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.TASK_IN_PROGRESS_COLOUR, "Adding new product");
                    labelProgressBar1.Value = 100;

                    // prepare the data
                    float price;
                    float.TryParse(textBox_price.Text, out price);
                    int quantity;
                    Int32.TryParse(textBox_quantity.Text, out quantity);
                    string id = textBox_ID.Text;
                    string description = textBox_description.Text;
                    Model.ObjectModel.Product newProduct = new Model.ObjectModel.Product();
                    newProduct.price = price;
                    newProduct.Quantity = quantity;
                    newProduct.ProductID = Convert.ToInt64(id);
                    newProduct.Description = description;

                    // log it
                    logger.Info("Adding new product record: ");
                    logger.Info("ID: " + id);
                    logger.Info("Description: " + description);
                    logger.Info("Quantity: " + quantity);
                    logger.Info("Price: " + price);

                    // run this task in a separate thread
                    await Task.Run(() =>
                    {
                        controller.addProduct(newProduct);
                    });
                }
                catch (Exception ex)
                {
                    // error adding new product
                    // tell the user and the logger
                    string errorMessage = "Error adding new product: " + ex.Message;
                    logger.Error(ex, errorMessage);
                    labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.TASK_FAILED_COLOUR, "Error adding new product");
                    labelProgressBar1.Value = 100;
                    this.UseWaitCursor = false;
                    MessageBox.Show(errorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // nothing more we can do
                    return;
                }

                // success
                // inform the user and the logger
                string successMessage = "Successfully added new product";
                labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.IDLE_COLOUR, "Ready");
                labelProgressBar1.Value = 100;
                this.UseWaitCursor = false;
                MessageBox.Show(successMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logger.Info(successMessage);

                // clean up UI
                textBox_ID.Text = string.Empty;
                textBox_description.Text = string.Empty;
                textBox_price.Text = string.Empty;
                textBox_quantity.Text = string.Empty;
            }
        }
        #endregion

        private void checkEntries(object sender, EventArgs e)
        {
            if ((textBox_ID.Text!=string.Empty) && (textBox_description.Text!=string.Empty) && (textBox_price.Text!=string.Empty) && (textBox_quantity.Text!=string.Empty))
            {
                button_add.Enabled = true;
            }
            else
            {
                button_add.Enabled = false;
            }

            labelProgressBar1.setColourAndText(Configuration.ProgressBarColours.IDLE_COLOUR, "Ready");
            labelProgressBar1.Value = 100;
        }
    }
}
