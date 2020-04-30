namespace POS.View
{
    partial class ViewStaffForm
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
            this.listView_staff = new System.Windows.Forms.ListView();
            this.button_deleteSelectedStaff = new System.Windows.Forms.Button();
            this.button_addNewStaff = new System.Windows.Forms.Button();
            this.button_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_staff
            // 
            this.listView_staff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_staff.HideSelection = false;
            this.listView_staff.Location = new System.Drawing.Point(12, 12);
            this.listView_staff.Name = "listView_staff";
            this.listView_staff.Size = new System.Drawing.Size(681, 426);
            this.listView_staff.TabIndex = 0;
            this.listView_staff.UseCompatibleStateImageBehavior = false;
            this.listView_staff.SelectedIndexChanged += new System.EventHandler(this.listView_staff_SelectedIndexChanged);
            // 
            // button_deleteSelectedStaff
            // 
            this.button_deleteSelectedStaff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_deleteSelectedStaff.Location = new System.Drawing.Point(699, 12);
            this.button_deleteSelectedStaff.Name = "button_deleteSelectedStaff";
            this.button_deleteSelectedStaff.Size = new System.Drawing.Size(98, 23);
            this.button_deleteSelectedStaff.TabIndex = 1;
            this.button_deleteSelectedStaff.Text = "Delete selected";
            this.button_deleteSelectedStaff.UseVisualStyleBackColor = true;
            this.button_deleteSelectedStaff.Click += new System.EventHandler(this.button_deleteSelectedStaff_Click);
            // 
            // button_addNewStaff
            // 
            this.button_addNewStaff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addNewStaff.Location = new System.Drawing.Point(699, 41);
            this.button_addNewStaff.Name = "button_addNewStaff";
            this.button_addNewStaff.Size = new System.Drawing.Size(98, 23);
            this.button_addNewStaff.TabIndex = 2;
            this.button_addNewStaff.Text = "Add new";
            this.button_addNewStaff.UseVisualStyleBackColor = true;
            this.button_addNewStaff.Click += new System.EventHandler(this.button_addNewStaff_Click);
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
            // ViewStaffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.button_addNewStaff);
            this.Controls.Add(this.button_deleteSelectedStaff);
            this.Controls.Add(this.listView_staff);
            this.Name = "ViewStaffForm";
            this.Text = "Staff";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_staff;
        private System.Windows.Forms.Button button_deleteSelectedStaff;
        private System.Windows.Forms.Button button_addNewStaff;
        private System.Windows.Forms.Button button_close;
    }
}