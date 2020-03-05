using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Model.ObjectModel;
using Model.ServiceLayer;

namespace POS.View
{
    public abstract class SpreadsheetExport
    {
        // constants
        protected int SPREADSHEET_ROW_OFFSET;

        // spreadsheet objects
        protected ExcelPackage spreadsheet;
        protected ExcelWorksheet worksheet;

        // data, metadata, headers
        protected string title = Configuration.STORE_NAME;
        protected string exportHeader = "Database Export";
        protected string exportTypeHeader = "Type:";
        protected string exportDateHeader = "Date:";

        // styles
        protected System.Drawing.Color dataColour1 = System.Drawing.Color.LightSkyBlue;
        protected System.Drawing.Color dataColour2 = System.Drawing.Color.PaleTurquoise;
        protected System.Drawing.Color headerColour = System.Drawing.Color.MediumSlateBlue;
        protected System.Drawing.Color metaColour = System.Drawing.Color.MediumSeaGreen;
 
        public abstract void prepareSpreadsheet();

        public abstract void retrieveData();

        public abstract void save();
    }

    public class StaffSpreadsheetExport : SpreadsheetExport
    {
        private List<Staff> staffList;

        public override void retrieveData()
        {
            // ask the model for a list of all staff
            staffList = StaffOps.getAllStaff();
        }

        public override void prepareSpreadsheet()
        {
            // create spreadsheet and worksheet
            this.spreadsheet = new ExcelPackage();
            this.worksheet = this.spreadsheet.Workbook.Worksheets.Add("Staff");

            // write metadata
            this.worksheet.Cells["A1:B1"].Merge = true;
            this.worksheet.Cells["A1"].Value = this.title;
            this.worksheet.Cells["A2:B2"].Merge = true;
            this.worksheet.Cells["B1"].Value = this.exportHeader;

            this.worksheet.Cells["A3"].Value = this.exportTypeHeader;
            this.worksheet.Cells["B3"].Value = "Staff";
            this.worksheet.Cells["A4"].Value = this.exportDateHeader;
            this.worksheet.Cells["B4"].Value = System.DateTime.Now.ToString("F");

            // write headers
            this.worksheet.Cells[SPREADSHEET_ROW_OFFSET - 1, 1].Value = "Staff ID";
            this.worksheet.Cells[SPREADSHEET_ROW_OFFSET - 1, 2].Value = "Full Name";
            this.worksheet.Cells[SPREADSHEET_ROW_OFFSET - 1, 3].Value = "Password Hash";
            this.worksheet.Cells[SPREADSHEET_ROW_OFFSET - 1, 4].Value = "Privelege";

            // write data
            int row = SPREADSHEET_ROW_OFFSET;
            for (int i = 0; i < this.staffList.Count; i++)
            {
                Staff staff = this.staffList[i];
                this.worksheet.Cells[row, 1].Value = staff.getID().ToString();
                this.worksheet.Cells[row, 2].Value = staff.getName();
                this.worksheet.Cells[row, 3].Value = staff.getPasswordHash();
                this.worksheet.Cells[row, 4].Value = staff.getPrivelege().ToString();

                // if this is the last element in the list, apply the outside thick border
                if (i==this.staffList.Count-1)
                {

                }

                row++;
            }
        }

        public override void save()
        {
            try
            {
                // save the spreadsheet
            }
            catch (Exception ex)
            {
                // it failed
                // tell the user and the logger
                System.Windows.Forms.MessageBox.Show("Error saving spreadsheet file", "Retail POS",
                                                     System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
