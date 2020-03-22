namespace POS.View
{
    partial class ViewTransactionsForm
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
            this.listView_transactions = new System.Windows.Forms.ListView();
            this.button_close = new System.Windows.Forms.Button();
            this.button_viewInvoices = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_transactions
            // 
            this.listView_transactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_transactions.HideSelection = false;
            this.listView_transactions.Location = new System.Drawing.Point(12, 12);
            this.listView_transactions.Name = "listView_transactions";
            this.listView_transactions.Size = new System.Drawing.Size(776, 397);
            this.listView_transactions.TabIndex = 0;
            this.listView_transactions.UseCompatibleStateImageBehavior = false;
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_close.Location = new System.Drawing.Point(12, 415);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(98, 23);
            this.button_close.TabIndex = 1;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_viewInvoices
            // 
            this.button_viewInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_viewInvoices.Location = new System.Drawing.Point(665, 415);
            this.button_viewInvoices.Name = "button_viewInvoices";
            this.button_viewInvoices.Size = new System.Drawing.Size(123, 23);
            this.button_viewInvoices.TabIndex = 2;
            this.button_viewInvoices.Text = "View Invoices";
            this.button_viewInvoices.UseVisualStyleBackColor = true;
            // 
            // ViewTransactionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_viewInvoices);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.listView_transactions);
            this.Name = "ViewTransactionsForm";
            this.Text = "Transactions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_transactions;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_viewInvoices;
    }
}