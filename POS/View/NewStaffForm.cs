﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;
using Security;

namespace POS.View
{
    public partial class NewStaffForm : Form
    {
        // controller dependency injection
        private StaffController controller;

        public NewStaffForm()
        {
            InitializeComponent();

            // prepare the comboBox
            comboBox_privelege.Items.Add("Admin");
            comboBox_privelege.Items.Add("Normal");

            // controller dependency injection
            controller = StaffController.getInstance();

            // cannot add anything yet
            button_addStaff.Enabled = false;

            // event handlers for data entry controls
            textBox_fullName.TextChanged += checkEntries;
            textBox_password.TextChanged += checkEntries;
            textBox_repeatPassword.TextChanged += checkEntries;
        }

        #region UI event handlers
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_addStaff_Click(object sender, EventArgs e)
        {
            if (controller != null)
            {
                try
                {
                    if (!(textBox_password.Text.Equals(textBox_repeatPassword.Text)))
                    {
                        MessageBox.Show("Passwords not the same. Please try again", "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        textBox_password.Text = null;
                        textBox_repeatPassword.Text = null;

                        return;
                    }
                    else
                    {
                        // can add
                        // hash the password
                        Hasher hasher = new Hasher(textBox_fullName.Text,textBox_password.Text);
                        controller.addStaff(textBox_fullName.Text,hasher.computeHash(),comboBox_privelege.Text);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void checkEntries(object sender, EventArgs e)
        {
            if ((textBox_fullName.Text!=string.Empty) && (textBox_password.Text!=string.Empty) && (textBox_repeatPassword.Text!=string.Empty))
            {
                button_addStaff.Enabled = true;
            }
            else
            {
                button_addStaff.Enabled = false;
            }
        }
        #endregion
    }
}
