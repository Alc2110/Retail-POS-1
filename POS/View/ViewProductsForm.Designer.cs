namespace POS.View
{
    partial class ViewProductsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView_products = new System.Windows.Forms.ListView();
            this.button_deleteSelectedProduct = new System.Windows.Forms.Button();
            this.button_addNewProduct = new System.Windows.Forms.Button();
            this.button_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_products
            // 
            this.listView_products.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_products.GridLines = true;
            this.listView_products.HideSelection = false;
            this.listView_products.Location = new System.Drawing.Point(12, 12);
            this.listView_products.Name = "listView_products";
            this.listView_products.Size = new System.Drawing.Size(681, 426);
            this.listView_products.TabIndex = 0;
            this.listView_products.UseCompatibleStateImageBehavior = false;
            this.listView_products.SelectedIndexChanged += new System.EventHandler(this.listView_products_SelectedIndexChanged);
            // 
            // button_deleteSelectedProduct
            // 
            this.button_deleteSelectedProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_deleteSelectedProduct.Location = new System.Drawing.Point(699, 12);
            this.button_deleteSelectedProduct.Name = "button_deleteSelectedProduct";
            this.button_deleteSelectedProduct.Size = new System.Drawing.Size(98, 23);
            this.button_deleteSelectedProduct.TabIndex = 1;
            this.button_deleteSelectedProduct.Text = "Delete selected";
            this.button_deleteSelectedProduct.UseVisualStyleBackColor = true;
            this.button_deleteSelectedProduct.Click += new System.EventHandler(this.button_deleteSelectedProduct_Click);
            // 
            // button_addNewProduct
            // 
            this.button_addNewProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addNewProduct.Location = new System.Drawing.Point(699, 41);
            this.button_addNewProduct.Name = "button_addNewProduct";
            this.button_addNewProduct.Size = new System.Drawing.Size(98, 23);
            this.button_addNewProduct.TabIndex = 2;
            this.button_addNewProduct.Text = "Add new";
            this.button_addNewProduct.UseVisualStyleBackColor = true;
            this.button_addNewProduct.Click += new System.EventHandler(this.button_addNewProduct_Click);
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_close.Location = new System.Drawing.Point(699, 415);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(98, 23);
            this.button_close.TabIndex = 3;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // ViewProductsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.button_addNewProduct);
            this.Controls.Add(this.button_deleteSelectedProduct);
            this.Controls.Add(this.listView_products);
            this.Name = "ViewProductsForm";
            this.Text = "Products";
            this.Load += new System.EventHandler(this.ViewProductsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_products;
        private System.Windows.Forms.Button button_deleteSelectedProduct;
        private System.Windows.Forms.Button button_addNewProduct;
        private System.Windows.Forms.Button button_close;
    }
}