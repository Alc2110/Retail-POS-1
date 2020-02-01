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

        public NewProductForm()
        {
            InitializeComponent();

            // controller dependency injection
            controller = ProductController.getInstance();

            // cannot add anything yet
            button_add.Enabled = false;

            // event handlers for data entry controls
            textBox_ID.TextChanged += checkEntries;
            textBox_description.TextChanged += checkEntries;
            textBox_price.TextChanged += checkEntries;
            textBox_quantity.TextChanged += checkEntries;
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (controller != null)
            {
                try
                {
                    int id;
                    Int32.TryParse(textBox_ID.Text, out id);
                    float price;
                    float.TryParse(textBox_price.Text, out price);
                    int quantity;
                    Int32.TryParse(textBox_quantity.Text, out quantity);
                    controller.addProduct(id, textBox_description.Text, quantity, price);
                }
                catch (Exception ex)
                {

                }
            }
        }

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
        }
        #endregion
    }
}
