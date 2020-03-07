namespace POS
{
    partial class MainWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewStaffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staffToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.customersToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.productsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_customerEmail = new System.Windows.Forms.TextBox();
            this.textBox_customerPhone = new System.Windows.Forms.TextBox();
            this.textBox_customerPostCode = new System.Windows.Forms.TextBox();
            this.comboBox_customerState = new System.Windows.Forms.ComboBox();
            this.textBox_customerCity = new System.Windows.Forms.TextBox();
            this.textBox_customerAddress = new System.Windows.Forms.TextBox();
            this.textBox_customerName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_addCustomer = new System.Windows.Forms.Button();
            this.button_findCustomer = new System.Windows.Forms.Button();
            this.textBox_customerAccNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_priceLookup = new System.Windows.Forms.Button();
            this.button_removeItem = new System.Windows.Forms.Button();
            this.button_addItem = new System.Windows.Forms.Button();
            this.textBox_itemQuantity = new System.Windows.Forms.TextBox();
            this.textBox_itemProductID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.richTextBox_itemPrice = new System.Windows.Forms.RichTextBox();
            this.listView_sales = new System.Windows.Forms.ListView();
            this.columnHeader_itemNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_qty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_findItem = new System.Windows.Forms.Button();
            this.button_newItem = new System.Windows.Forms.Button();
            this.button_customerList = new System.Windows.Forms.Button();
            this.button_Discount = new System.Windows.Forms.Button();
            this.button_clearSale = new System.Windows.Forms.Button();
            this.button_logOut = new System.Windows.Forms.Button();
            this.button_checkout = new System.Windows.Forms.Button();
            this.button_newSaleMember = new System.Windows.Forms.Button();
            this.button_newSaleNonMember = new System.Windows.Forms.Button();
            this.button_staffList = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_accType = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_state = new System.Windows.Forms.ToolStripStatusLabel();
            this.iToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staffToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.customersToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.productsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.databaseToolStripMenuItem,
            this.transactionToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1099, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.addNewStaffToolStripMenuItem,
            this.addNewCustomerToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // addNewStaffToolStripMenuItem
            // 
            this.addNewStaffToolStripMenuItem.Name = "addNewStaffToolStripMenuItem";
            this.addNewStaffToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.addNewStaffToolStripMenuItem.Text = "Add new staff";
            this.addNewStaffToolStripMenuItem.Click += new System.EventHandler(this.addNewStaffToolStripMenuItem_Click);
            // 
            // addNewCustomerToolStripMenuItem
            // 
            this.addNewCustomerToolStripMenuItem.Name = "addNewCustomerToolStripMenuItem";
            this.addNewCustomerToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.addNewCustomerToolStripMenuItem.Text = "Add new customer";
            this.addNewCustomerToolStripMenuItem.Click += new System.EventHandler(this.addNewCustomerToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptingToolStripMenuItem,
            this.iToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // scriptingToolStripMenuItem
            // 
            this.scriptingToolStripMenuItem.Name = "scriptingToolStripMenuItem";
            this.scriptingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.scriptingToolStripMenuItem.Text = "Scripting";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staffToolStripMenuItem,
            this.customersToolStripMenuItem,
            this.productsToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importToolStripMenuItem.Text = "Import Update";
            // 
            // staffToolStripMenuItem
            // 
            this.staffToolStripMenuItem.Name = "staffToolStripMenuItem";
            this.staffToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.staffToolStripMenuItem.Text = "Staff";
            // 
            // customersToolStripMenuItem
            // 
            this.customersToolStripMenuItem.Name = "customersToolStripMenuItem";
            this.customersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.customersToolStripMenuItem.Text = "Customers";
            // 
            // productsToolStripMenuItem
            // 
            this.productsToolStripMenuItem.Name = "productsToolStripMenuItem";
            this.productsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.productsToolStripMenuItem.Text = "Products";
            this.productsToolStripMenuItem.Click += new System.EventHandler(this.productsToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staffToolStripMenuItem1,
            this.customersToolStripMenuItem1,
            this.productsToolStripMenuItem1,
            this.transactionsToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // staffToolStripMenuItem1
            // 
            this.staffToolStripMenuItem1.Name = "staffToolStripMenuItem1";
            this.staffToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.staffToolStripMenuItem1.Text = "Staff";
            this.staffToolStripMenuItem1.Click += new System.EventHandler(this.staffToolStripMenuItem1_Click);
            // 
            // customersToolStripMenuItem1
            // 
            this.customersToolStripMenuItem1.Name = "customersToolStripMenuItem1";
            this.customersToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.customersToolStripMenuItem1.Text = "Customers";
            this.customersToolStripMenuItem1.Click += new System.EventHandler(this.customersToolStripMenuItem1_Click);
            // 
            // productsToolStripMenuItem1
            // 
            this.productsToolStripMenuItem1.Name = "productsToolStripMenuItem1";
            this.productsToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.productsToolStripMenuItem1.Text = "Products";
            this.productsToolStripMenuItem1.Click += new System.EventHandler(this.productsToolStripMenuItem1_Click);
            // 
            // transactionsToolStripMenuItem
            // 
            this.transactionsToolStripMenuItem.Name = "transactionsToolStripMenuItem";
            this.transactionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.transactionsToolStripMenuItem.Text = "Transactions";
            this.transactionsToolStripMenuItem.Click += new System.EventHandler(this.transactionsToolStripMenuItem_Click);
            // 
            // transactionToolStripMenuItem
            // 
            this.transactionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHistoryToolStripMenuItem});
            this.transactionToolStripMenuItem.Name = "transactionToolStripMenuItem";
            this.transactionToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.transactionToolStripMenuItem.Text = "Transaction";
            // 
            // viewHistoryToolStripMenuItem
            // 
            this.viewHistoryToolStripMenuItem.Name = "viewHistoryToolStripMenuItem";
            this.viewHistoryToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.viewHistoryToolStripMenuItem.Text = "View history";
            this.viewHistoryToolStripMenuItem.Click += new System.EventHandler(this.viewHistoryToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBox_customerEmail);
            this.groupBox1.Controls.Add(this.textBox_customerPhone);
            this.groupBox1.Controls.Add(this.textBox_customerPostCode);
            this.groupBox1.Controls.Add(this.comboBox_customerState);
            this.groupBox1.Controls.Add(this.textBox_customerCity);
            this.groupBox1.Controls.Add(this.textBox_customerAddress);
            this.groupBox1.Controls.Add(this.textBox_customerName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button_addCustomer);
            this.groupBox1.Controls.Add(this.button_findCustomer);
            this.groupBox1.Controls.Add(this.textBox_customerAccNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(612, 169);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer details";
            // 
            // textBox_customerEmail
            // 
            this.textBox_customerEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_customerEmail.Location = new System.Drawing.Point(388, 82);
            this.textBox_customerEmail.Name = "textBox_customerEmail";
            this.textBox_customerEmail.Size = new System.Drawing.Size(218, 20);
            this.textBox_customerEmail.TabIndex = 17;
            // 
            // textBox_customerPhone
            // 
            this.textBox_customerPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_customerPhone.Location = new System.Drawing.Point(388, 56);
            this.textBox_customerPhone.Name = "textBox_customerPhone";
            this.textBox_customerPhone.Size = new System.Drawing.Size(218, 20);
            this.textBox_customerPhone.TabIndex = 16;
            // 
            // textBox_customerPostCode
            // 
            this.textBox_customerPostCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_customerPostCode.Location = new System.Drawing.Point(206, 134);
            this.textBox_customerPostCode.Name = "textBox_customerPostCode";
            this.textBox_customerPostCode.Size = new System.Drawing.Size(112, 20);
            this.textBox_customerPostCode.TabIndex = 15;
            // 
            // comboBox_customerState
            // 
            this.comboBox_customerState.FormattingEnabled = true;
            this.comboBox_customerState.Location = new System.Drawing.Point(65, 134);
            this.comboBox_customerState.Name = "comboBox_customerState";
            this.comboBox_customerState.Size = new System.Drawing.Size(77, 21);
            this.comboBox_customerState.TabIndex = 14;
            // 
            // textBox_customerCity
            // 
            this.textBox_customerCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_customerCity.Location = new System.Drawing.Point(65, 108);
            this.textBox_customerCity.Name = "textBox_customerCity";
            this.textBox_customerCity.Size = new System.Drawing.Size(253, 20);
            this.textBox_customerCity.TabIndex = 13;
            // 
            // textBox_customerAddress
            // 
            this.textBox_customerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_customerAddress.Location = new System.Drawing.Point(65, 82);
            this.textBox_customerAddress.Name = "textBox_customerAddress";
            this.textBox_customerAddress.Size = new System.Drawing.Size(253, 20);
            this.textBox_customerAddress.TabIndex = 12;
            // 
            // textBox_customerName
            // 
            this.textBox_customerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_customerName.Location = new System.Drawing.Point(65, 56);
            this.textBox_customerName.Name = "textBox_customerName";
            this.textBox_customerName.Size = new System.Drawing.Size(253, 20);
            this.textBox_customerName.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(148, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Postcode";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(344, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Email";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(344, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Phone";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "State";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "City";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name";
            // 
            // button_addCustomer
            // 
            this.button_addCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addCustomer.Location = new System.Drawing.Point(531, 22);
            this.button_addCustomer.Name = "button_addCustomer";
            this.button_addCustomer.Size = new System.Drawing.Size(75, 23);
            this.button_addCustomer.TabIndex = 3;
            this.button_addCustomer.Text = "Add";
            this.button_addCustomer.UseVisualStyleBackColor = true;
            this.button_addCustomer.Click += new System.EventHandler(this.button_addCustomer_Click);
            // 
            // button_findCustomer
            // 
            this.button_findCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_findCustomer.Location = new System.Drawing.Point(450, 22);
            this.button_findCustomer.Name = "button_findCustomer";
            this.button_findCustomer.Size = new System.Drawing.Size(75, 23);
            this.button_findCustomer.TabIndex = 2;
            this.button_findCustomer.Text = "Find";
            this.button_findCustomer.UseVisualStyleBackColor = true;
            this.button_findCustomer.Click += new System.EventHandler(this.button_findCustomer_Click);
            // 
            // textBox_customerAccNo
            // 
            this.textBox_customerAccNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_customerAccNo.Location = new System.Drawing.Point(118, 22);
            this.textBox_customerAccNo.Name = "textBox_customerAccNo";
            this.textBox_customerAccNo.Size = new System.Drawing.Size(200, 20);
            this.textBox_customerAccNo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer account #:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button_priceLookup);
            this.groupBox2.Controls.Add(this.button_removeItem);
            this.groupBox2.Controls.Add(this.button_addItem);
            this.groupBox2.Controls.Add(this.textBox_itemQuantity);
            this.groupBox2.Controls.Add(this.textBox_itemProductID);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.richTextBox_itemPrice);
            this.groupBox2.Location = new System.Drawing.Point(630, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(457, 169);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Items";
            // 
            // button_priceLookup
            // 
            this.button_priceLookup.Location = new System.Drawing.Point(376, 106);
            this.button_priceLookup.Name = "button_priceLookup";
            this.button_priceLookup.Size = new System.Drawing.Size(75, 49);
            this.button_priceLookup.TabIndex = 7;
            this.button_priceLookup.Text = "Price lookup";
            this.button_priceLookup.UseVisualStyleBackColor = true;
            this.button_priceLookup.Click += new System.EventHandler(this.button_priceLookup_Click);
            // 
            // button_removeItem
            // 
            this.button_removeItem.Location = new System.Drawing.Point(295, 106);
            this.button_removeItem.Name = "button_removeItem";
            this.button_removeItem.Size = new System.Drawing.Size(75, 49);
            this.button_removeItem.TabIndex = 6;
            this.button_removeItem.Text = "Remove item";
            this.button_removeItem.UseVisualStyleBackColor = true;
            this.button_removeItem.Click += new System.EventHandler(this.button_removeItem_Click);
            // 
            // button_addItem
            // 
            this.button_addItem.Location = new System.Drawing.Point(214, 106);
            this.button_addItem.Name = "button_addItem";
            this.button_addItem.Size = new System.Drawing.Size(75, 49);
            this.button_addItem.TabIndex = 5;
            this.button_addItem.Text = "Add item";
            this.button_addItem.UseVisualStyleBackColor = true;
            this.button_addItem.Click += new System.EventHandler(this.button_addItem_Click);
            // 
            // textBox_itemQuantity
            // 
            this.textBox_itemQuantity.Location = new System.Drawing.Point(73, 135);
            this.textBox_itemQuantity.Name = "textBox_itemQuantity";
            this.textBox_itemQuantity.Size = new System.Drawing.Size(135, 20);
            this.textBox_itemQuantity.TabIndex = 4;
            this.textBox_itemQuantity.TextChanged += new System.EventHandler(this.textBox_itemQuantity_TextChanged);
            // 
            // textBox_itemProductID
            // 
            this.textBox_itemProductID.Location = new System.Drawing.Point(73, 106);
            this.textBox_itemProductID.Name = "textBox_itemProductID";
            this.textBox_itemProductID.Size = new System.Drawing.Size(135, 20);
            this.textBox_itemProductID.TabIndex = 3;
            this.textBox_itemProductID.TextChanged += new System.EventHandler(this.textBox_itemProductID_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Quantity";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Product ID";
            // 
            // richTextBox_itemPrice
            // 
            this.richTextBox_itemPrice.AutoWordSelection = true;
            this.richTextBox_itemPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F);
            this.richTextBox_itemPrice.Location = new System.Drawing.Point(6, 13);
            this.richTextBox_itemPrice.Name = "richTextBox_itemPrice";
            this.richTextBox_itemPrice.ReadOnly = true;
            this.richTextBox_itemPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.richTextBox_itemPrice.Size = new System.Drawing.Size(445, 87);
            this.richTextBox_itemPrice.TabIndex = 0;
            this.richTextBox_itemPrice.Text = "0.00";
            // 
            // listView_sales
            // 
            this.listView_sales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_sales.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_itemNumber,
            this.columnHeader_desc,
            this.columnHeader_qty,
            this.columnHeader_price,
            this.columnHeader_total});
            this.listView_sales.HideSelection = false;
            this.listView_sales.Location = new System.Drawing.Point(12, 202);
            this.listView_sales.Name = "listView_sales";
            this.listView_sales.Size = new System.Drawing.Size(1075, 265);
            this.listView_sales.TabIndex = 3;
            this.listView_sales.UseCompatibleStateImageBehavior = false;
            this.listView_sales.View = System.Windows.Forms.View.Details;
            this.listView_sales.SelectedIndexChanged += new System.EventHandler(this.listView_sales_SelectedIndexChanged);
            // 
            // columnHeader_itemNumber
            // 
            this.columnHeader_itemNumber.Text = "Item #";
            this.columnHeader_itemNumber.Width = 182;
            // 
            // columnHeader_desc
            // 
            this.columnHeader_desc.Text = "Description";
            this.columnHeader_desc.Width = 308;
            // 
            // columnHeader_qty
            // 
            this.columnHeader_qty.Text = "Qty";
            // 
            // columnHeader_price
            // 
            this.columnHeader_price.Text = "Price";
            this.columnHeader_price.Width = 114;
            // 
            // columnHeader_total
            // 
            this.columnHeader_total.Text = "Total";
            this.columnHeader_total.Width = 118;
            // 
            // button_findItem
            // 
            this.button_findItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_findItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_findItem.Location = new System.Drawing.Point(12, 474);
            this.button_findItem.Name = "button_findItem";
            this.button_findItem.Size = new System.Drawing.Size(75, 49);
            this.button_findItem.TabIndex = 8;
            this.button_findItem.Text = "Find item";
            this.button_findItem.UseVisualStyleBackColor = true;
            this.button_findItem.Click += new System.EventHandler(this.button_findItem_Click);
            // 
            // button_newItem
            // 
            this.button_newItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_newItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_newItem.Location = new System.Drawing.Point(93, 474);
            this.button_newItem.Name = "button_newItem";
            this.button_newItem.Size = new System.Drawing.Size(75, 49);
            this.button_newItem.TabIndex = 9;
            this.button_newItem.Text = "New item";
            this.button_newItem.UseVisualStyleBackColor = true;
            this.button_newItem.Click += new System.EventHandler(this.button_newItem_Click);
            // 
            // button_customerList
            // 
            this.button_customerList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_customerList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button_customerList.Location = new System.Drawing.Point(174, 474);
            this.button_customerList.Name = "button_customerList";
            this.button_customerList.Size = new System.Drawing.Size(75, 49);
            this.button_customerList.TabIndex = 10;
            this.button_customerList.Text = "Customer List";
            this.button_customerList.UseVisualStyleBackColor = true;
            this.button_customerList.Click += new System.EventHandler(this.button_customerList_Click);
            // 
            // button_Discount
            // 
            this.button_Discount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Discount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button_Discount.Location = new System.Drawing.Point(336, 474);
            this.button_Discount.Name = "button_Discount";
            this.button_Discount.Size = new System.Drawing.Size(75, 49);
            this.button_Discount.TabIndex = 11;
            this.button_Discount.Text = "Discount";
            this.button_Discount.UseVisualStyleBackColor = true;
            this.button_Discount.Click += new System.EventHandler(this.button_Discount_Click);
            // 
            // button_clearSale
            // 
            this.button_clearSale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_clearSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_clearSale.Location = new System.Drawing.Point(417, 474);
            this.button_clearSale.Name = "button_clearSale";
            this.button_clearSale.Size = new System.Drawing.Size(75, 49);
            this.button_clearSale.TabIndex = 12;
            this.button_clearSale.Text = "Clear Sale";
            this.button_clearSale.UseVisualStyleBackColor = true;
            this.button_clearSale.Click += new System.EventHandler(this.button_clearSale_Click);
            // 
            // button_logOut
            // 
            this.button_logOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_logOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_logOut.Location = new System.Drawing.Point(498, 474);
            this.button_logOut.Name = "button_logOut";
            this.button_logOut.Size = new System.Drawing.Size(75, 49);
            this.button_logOut.TabIndex = 13;
            this.button_logOut.Text = "Log Out";
            this.button_logOut.UseVisualStyleBackColor = true;
            this.button_logOut.Click += new System.EventHandler(this.button9_Click);
            // 
            // button_checkout
            // 
            this.button_checkout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_checkout.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.button_checkout.Location = new System.Drawing.Point(925, 474);
            this.button_checkout.Name = "button_checkout";
            this.button_checkout.Size = new System.Drawing.Size(162, 49);
            this.button_checkout.TabIndex = 14;
            this.button_checkout.Text = "Checkout";
            this.button_checkout.UseVisualStyleBackColor = true;
            this.button_checkout.Click += new System.EventHandler(this.button_checkout_Click);
            // 
            // button_newSaleMember
            // 
            this.button_newSaleMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_newSaleMember.Location = new System.Drawing.Point(844, 474);
            this.button_newSaleMember.Name = "button_newSaleMember";
            this.button_newSaleMember.Size = new System.Drawing.Size(75, 49);
            this.button_newSaleMember.TabIndex = 15;
            this.button_newSaleMember.Text = "New Sale (member)";
            this.button_newSaleMember.UseVisualStyleBackColor = true;
            this.button_newSaleMember.Click += new System.EventHandler(this.button_newSaleMember_Click);
            // 
            // button_newSaleNonMember
            // 
            this.button_newSaleNonMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_newSaleNonMember.Location = new System.Drawing.Point(753, 474);
            this.button_newSaleNonMember.Name = "button_newSaleNonMember";
            this.button_newSaleNonMember.Size = new System.Drawing.Size(85, 49);
            this.button_newSaleNonMember.TabIndex = 16;
            this.button_newSaleNonMember.Text = "New Sale (non-member)";
            this.button_newSaleNonMember.UseVisualStyleBackColor = true;
            this.button_newSaleNonMember.Click += new System.EventHandler(this.button_newSaleNonMember_Click);
            // 
            // button_staffList
            // 
            this.button_staffList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_staffList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button_staffList.Location = new System.Drawing.Point(255, 474);
            this.button_staffList.Name = "button_staffList";
            this.button_staffList.Size = new System.Drawing.Size(75, 49);
            this.button_staffList.TabIndex = 17;
            this.button_staffList.Text = "Staff List";
            this.button_staffList.UseVisualStyleBackColor = true;
            this.button_staffList.Click += new System.EventHandler(this.button_staffList_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_accType,
            this.toolStripStatusLabel_state});
            this.statusStrip1.Location = new System.Drawing.Point(0, 527);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1099, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_accType
            // 
            this.toolStripStatusLabel_accType.Name = "toolStripStatusLabel_accType";
            this.toolStripStatusLabel_accType.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLabel_accType.Text = "Account Type";
            // 
            // toolStripStatusLabel_state
            // 
            this.toolStripStatusLabel_state.Name = "toolStripStatusLabel_state";
            this.toolStripStatusLabel_state.Size = new System.Drawing.Size(33, 17);
            this.toolStripStatusLabel_state.Text = "State";
            // 
            // iToolStripMenuItem
            // 
            this.iToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staffToolStripMenuItem2,
            this.customersToolStripMenuItem2,
            this.productsToolStripMenuItem2});
            this.iToolStripMenuItem.Name = "iToolStripMenuItem";
            this.iToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.iToolStripMenuItem.Text = "Import";
            // 
            // staffToolStripMenuItem2
            // 
            this.staffToolStripMenuItem2.Name = "staffToolStripMenuItem2";
            this.staffToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.staffToolStripMenuItem2.Text = "Staff";
            // 
            // customersToolStripMenuItem2
            // 
            this.customersToolStripMenuItem2.Name = "customersToolStripMenuItem2";
            this.customersToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.customersToolStripMenuItem2.Text = "Customers";
            // 
            // productsToolStripMenuItem2
            // 
            this.productsToolStripMenuItem2.Name = "productsToolStripMenuItem2";
            this.productsToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.productsToolStripMenuItem2.Text = "Products";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 549);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_staffList);
            this.Controls.Add(this.button_newSaleNonMember);
            this.Controls.Add(this.button_newSaleMember);
            this.Controls.Add(this.button_checkout);
            this.Controls.Add(this.button_logOut);
            this.Controls.Add(this.button_clearSale);
            this.Controls.Add(this.button_Discount);
            this.Controls.Add(this.button_customerList);
            this.Controls.Add(this.button_newItem);
            this.Controls.Add(this.button_findItem);
            this.Controls.Add(this.listView_sales);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Retail POS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_addCustomer;
        private System.Windows.Forms.Button button_findCustomer;
        private System.Windows.Forms.TextBox textBox_customerAccNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_customerAddress;
        private System.Windows.Forms.TextBox textBox_customerName;
        private System.Windows.Forms.TextBox textBox_customerEmail;
        private System.Windows.Forms.TextBox textBox_customerPhone;
        private System.Windows.Forms.TextBox textBox_customerPostCode;
        private System.Windows.Forms.ComboBox comboBox_customerState;
        private System.Windows.Forms.TextBox textBox_customerCity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_itemQuantity;
        private System.Windows.Forms.TextBox textBox_itemProductID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox richTextBox_itemPrice;
        private System.Windows.Forms.Button button_priceLookup;
        private System.Windows.Forms.Button button_removeItem;
        private System.Windows.Forms.Button button_addItem;
        private System.Windows.Forms.ListView listView_sales;
        private System.Windows.Forms.Button button_findItem;
        private System.Windows.Forms.Button button_newItem;
        private System.Windows.Forms.Button button_customerList;
        private System.Windows.Forms.Button button_Discount;
        private System.Windows.Forms.Button button_clearSale;
        private System.Windows.Forms.Button button_logOut;
        private System.Windows.Forms.Button button_checkout;
        private System.Windows.Forms.ColumnHeader columnHeader_itemNumber;
        private System.Windows.Forms.ColumnHeader columnHeader_desc;
        private System.Windows.Forms.ColumnHeader columnHeader_qty;
        private System.Windows.Forms.ColumnHeader columnHeader_price;
        private System.Windows.Forms.ColumnHeader columnHeader_total;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewStaffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewCustomerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptingToolStripMenuItem;
        private System.Windows.Forms.Button button_newSaleMember;
        private System.Windows.Forms.Button button_newSaleNonMember;
        private System.Windows.Forms.Button button_staffList;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staffToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem customersToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem productsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem transactionsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_accType;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_state;
        private System.Windows.Forms.ToolStripMenuItem iToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staffToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem customersToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem productsToolStripMenuItem2;
    }
}

