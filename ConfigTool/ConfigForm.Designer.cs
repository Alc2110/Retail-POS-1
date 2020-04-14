namespace ConfigTool
{
    partial class ConfigForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_serverAddress = new System.Windows.Forms.TextBox();
            this.textBox_serverUserID = new System.Windows.Forms.TextBox();
            this.textBox_userPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_apply = new System.Windows.Forms.Button();
            this.textBox_storeName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton_overwriteLogFile = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_userPassword);
            this.groupBox1.Controls.Add(this.textBox_serverUserID);
            this.groupBox1.Controls.Add(this.textBox_serverAddress);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // textBox_serverAddress
            // 
            this.textBox_serverAddress.Location = new System.Drawing.Point(68, 16);
            this.textBox_serverAddress.Name = "textBox_serverAddress";
            this.textBox_serverAddress.Size = new System.Drawing.Size(245, 20);
            this.textBox_serverAddress.TabIndex = 0;
            // 
            // textBox_serverUserID
            // 
            this.textBox_serverUserID.Location = new System.Drawing.Point(68, 49);
            this.textBox_serverUserID.Name = "textBox_serverUserID";
            this.textBox_serverUserID.Size = new System.Drawing.Size(245, 20);
            this.textBox_serverUserID.TabIndex = 1;
            // 
            // textBox_userPassword
            // 
            this.textBox_userPassword.Location = new System.Drawing.Point(68, 81);
            this.textBox_userPassword.Name = "textBox_userPassword";
            this.textBox_userPassword.Size = new System.Drawing.Size(245, 20);
            this.textBox_userPassword.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "User ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password:";
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(94, 238);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(175, 238);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_overwriteLogFile);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox_storeName);
            this.groupBox2.Location = new System.Drawing.Point(12, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(319, 100);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Miscellaneous";
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(256, 238);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(75, 23);
            this.button_apply.TabIndex = 8;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // textBox_storeName
            // 
            this.textBox_storeName.Location = new System.Drawing.Point(70, 19);
            this.textBox_storeName.Name = "textBox_storeName";
            this.textBox_storeName.Size = new System.Drawing.Size(243, 20);
            this.textBox_storeName.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Store name:";
            // 
            // radioButton_overwriteLogFile
            // 
            this.radioButton_overwriteLogFile.AutoSize = true;
            this.radioButton_overwriteLogFile.Location = new System.Drawing.Point(3, 77);
            this.radioButton_overwriteLogFile.Name = "radioButton_overwriteLogFile";
            this.radioButton_overwriteLogFile.Size = new System.Drawing.Size(106, 17);
            this.radioButton_overwriteLogFile.TabIndex = 2;
            this.radioButton_overwriteLogFile.TabStop = true;
            this.radioButton_overwriteLogFile.Text = "Overwrite log file ";
            this.radioButton_overwriteLogFile.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 271);
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_ok);
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "Retail POS Configuration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_userPassword;
        private System.Windows.Forms.TextBox textBox_serverUserID;
        private System.Windows.Forms.TextBox textBox_serverAddress;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.TextBox textBox_storeName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton_overwriteLogFile;
    }
}

