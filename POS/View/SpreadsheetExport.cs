using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Model.ObjectModel;
using Model.ServiceLayer;
using System.IO;

namespace POS.View
{
    public abstract class SpreadsheetExport
    {
        // row constants
        protected int SPREADSHEET_HEADER_ROW = 6;
        protected int SPREADSHEET_ROW_OFFSET = 7;

        // spreadsheet objects
        protected ExcelPackage spreadsheet;
        protected ExcelWorksheet worksheet;

        // data, metadata, headers
        protected string title = Configuration.STORE_NAME;
        protected string exportHeader = "Database Export";
        protected string exportTypeHeader = "Type:";
        protected string exportDateHeader = "Date:";

        // cell colours
        protected System.Drawing.Color dataColour1 = System.Drawing.Color.LightSkyBlue;
        protected System.Drawing.Color dataColour2 = System.Drawing.Color.PaleTurquoise;
        protected System.Drawing.Color headerColour = System.Drawing.Color.MediumSlateBlue;
        protected System.Drawing.Color metaColour = System.Drawing.Color.MediumSeaGreen;
 
        // spreadsheet processing methods
        public abstract void prepareSpreadsheet();
        public abstract void retrieveData();
        /// <summary>
        /// Save the spreadsheet as an Excel file.
        /// </summary>
        /// <param name="exportType">string. Describes the type of list being exported.</param>
        public void saveSpreadsheet(string exportType)
        {
            try
            {
                // save the spreadsheet

                // create a save file dialog
                System.Windows.Forms.SaveFileDialog saveSpreadsheetDialog = new System.Windows.Forms.SaveFileDialog();
                saveSpreadsheetDialog.Filter = "Excel spreadsheet|*.xlsx";
                saveSpreadsheetDialog.Title = "Export " + exportType + " list as spreadsheet";

                // check if user clicked save button
                if (saveSpreadsheetDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // get the file info
                    FileInfo fi = new FileInfo(saveSpreadsheetDialog.FileName);
                    // write the file to disk
                    this.spreadsheet.SaveAs(fi);
                }
            }
            catch (Exception ex)
            {
                // it failed
                // pass it up
                throw;
            }
        }

        // spreadsheet styling methods
        protected void applyThickAllBorders(ExcelRange cells)
        {
            cells.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            cells.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            cells.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            cells.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        }
        protected void applyRightThickBorders(ExcelRange cells)
        {
            cells.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        }
        protected void applyBottomThickBorders(ExcelRange cells)
        {
            cells.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
        }
        protected void applyThinBorders(ExcelRange cells)
        {
            cells.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            cells.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            cells.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            cells.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        }
        protected void colourEvenRow(ExcelRange cells)
        {
            cells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(dataColour1);
        }
        protected void colourOddRow(ExcelRange cells)
        {
            cells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(dataColour2);
        }
        protected void colourMeta(ExcelRange cells)
        {
            cells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(metaColour);
        }
        protected void colourHeader(ExcelRange cells)
        {
            cells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(headerColour);
        }
    }

    public class CustomerSpreadsheetExport : SpreadsheetExport
    {
        private List<Customer> customerList;

        public override void retrieveData()
        {
            // ask the model for a list of all customers
            customerList = CustomerOps.getAllCustomers();
        }

        public override void prepareSpreadsheet()
        {
            // create spreadsheet and worksheet
            this.spreadsheet = new ExcelPackage();
            this.worksheet = this.spreadsheet.Workbook.Worksheets.Add("Customers");

            // write metadata
            this.worksheet.Cells["A1:B1"].Merge = true;
            this.worksheet.Cells["A1"].Value = this.title;
            applyThickAllBorders(this.worksheet.Cells["A1"]);
            colourMeta(this.worksheet.Cells["A1"]);

            this.worksheet.Cells["A2:B2"].Merge = true;
            this.worksheet.Cells["A2"].Value = this.exportHeader;
            applyThickAllBorders(this.worksheet.Cells["A2"]);
            colourMeta(this.worksheet.Cells["A2"]);

            this.worksheet.Cells["A3"].Value = this.exportTypeHeader;
            this.worksheet.Cells["B3"].Value = "Customers";
            this.worksheet.Cells["A4"].Value = this.exportDateHeader;
            this.worksheet.Cells["B4"].Value = System.DateTime.Now.ToString("F");
            colourMeta(this.worksheet.Cells["A3:B3"]);
            colourMeta(this.worksheet.Cells["A4:B4"]);
            this.worksheet.Column(2).Width = 50;

            // write headers
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1].Value = "Customer ID";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 2].Value = "Full name";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 3].Value = "Address";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 4].Value = "Phone Number";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 5].Value = "Email";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 6].Value = "City";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 7].Value = "State";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 8].Value = "Postcode";
            applyThickAllBorders(this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1, SPREADSHEET_HEADER_ROW, 8]);
            colourHeader(this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1, SPREADSHEET_HEADER_ROW, 8]);

            // write data
            int row = SPREADSHEET_ROW_OFFSET;
            for (int i = 0; i < this.customerList.Count; i++)
            {
                Customer customer = this.customerList[i];
                this.worksheet.Cells[row, 1].Value = customer.getID().ToString();
                this.worksheet.Cells[row, 2].Value = customer.getName();
                this.worksheet.Cells[row, 3].Value = customer.getAddress();
                this.worksheet.Cells[row, 4].Value = customer.getPhoneNumber();
                this.worksheet.Cells[row, 5].Value = customer.getEmail();
                this.worksheet.Cells[row, 6].Value = customer.getCity();
                this.worksheet.Cells[row, 7].Value = customer.getState().ToString();
                this.worksheet.Cells[row, 8].Value = customer.getPostcode().ToString();
                applyRightThickBorders(this.worksheet.Cells[row, 8]);
                // even and odd rows have alternating colours
                if (row%2==0)
                {
                    for (int col = 1; col <= 8; col++)
                    {
                        colourEvenRow(this.worksheet.Cells[row, col]);
                    }
                }
                else
                {
                    for (int col = 1; col <= 8; col++)
                    {
                        colourOddRow(this.worksheet.Cells[row, col]);
                    }
                }

                // if this is the last element in the list, apply the outside thick border
                if (i == this.customerList.Count - 1)
                {
                    for (int col = 1; col <= 8; col++)
                    {
                        applyBottomThickBorders(this.worksheet.Cells[row, col]);
                    }
                }

                row++;
            }
        }
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
            applyThickAllBorders(this.worksheet.Cells["A1"]);
            colourMeta(this.worksheet.Cells["A1"]);

            this.worksheet.Cells["A2:B2"].Merge = true;
            this.worksheet.Cells["A2"].Value = this.exportHeader;
            applyThickAllBorders(this.worksheet.Cells["A2"]);
            colourMeta(this.worksheet.Cells["A2"]);

            this.worksheet.Cells["A3"].Value = this.exportTypeHeader;
            this.worksheet.Cells["B3"].Value = "Staff";
            this.worksheet.Cells["A4"].Value = this.exportDateHeader;
            this.worksheet.Cells["B4"].Value = System.DateTime.Now.ToString("F");
            colourMeta(this.worksheet.Cells["A3:B3"]);
            colourMeta(this.worksheet.Cells["A4:B4"]);
            this.worksheet.Column(2).Width = 50;

            // write headers
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1].Value = "Staff ID";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 2].Value = "Full Name";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 3].Value = "Password Hash";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 4].Value = "Privelege";
            applyThickAllBorders(this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1, SPREADSHEET_HEADER_ROW, 4]);
            colourHeader(this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1, SPREADSHEET_HEADER_ROW, 4]);

            // write data
            int row = SPREADSHEET_ROW_OFFSET;
            for (int i = 0; i < this.staffList.Count; i++)
            {
                Staff staff = this.staffList[i];
                this.worksheet.Cells[row, 1].Value = staff.getID().ToString();
                this.worksheet.Cells[row, 2].Value = staff.getName();
                this.worksheet.Cells[row, 3].Value = staff.getPasswordHash();
                this.worksheet.Cells[row, 4].Value = staff.getPrivelege().ToString();
                applyRightThickBorders(this.worksheet.Cells[row, 4]);
                // even and odd rows have alternating colours
                if (row%2==0)
                {
                    for (int col = 1; col <= 4; col++)
                    {
                        colourEvenRow(this.worksheet.Cells[row, col]);
                    }
                }
                else
                {
                    for (int col = 1; col <= 4; col++)
                    {
                        colourOddRow(this.worksheet.Cells[row, col]);
                    }
                }

                // if this is the last element in the list, apply the outside thick border
                if (i==this.staffList.Count-1)
                {
                    for (int col = 1; col <= 4; col++)
                    {
                        applyBottomThickBorders(this.worksheet.Cells[row, col]);
                    }
                }

                row++;
            }
        }
    }

    public class ProductSpreadsheetExport : SpreadsheetExport
    {
        private List<Product> productList;

        public override void retrieveData()
        {
            // ask the Model for a list of all products
            productList = ProductOps.getAllProducts();
        }

        public override void prepareSpreadsheet()
        {
            // create spreadsheet and worksheet
            this.spreadsheet = new ExcelPackage();
            this.worksheet = this.spreadsheet.Workbook.Worksheets.Add("Products");

            // write metadata
            this.worksheet.Cells["A1:B1"].Merge = true;
            this.worksheet.Cells["A1"].Value = this.title;
            applyThickAllBorders(this.worksheet.Cells["A1"]);
            colourMeta(this.worksheet.Cells["A1"]);

            this.worksheet.Cells["A2:B2"].Merge = true;
            this.worksheet.Cells["A2"].Value = this.exportHeader;
            applyThickAllBorders(this.worksheet.Cells["A2"]);
            colourMeta(this.worksheet.Cells["A2"]);

            this.worksheet.Cells["A3"].Value = this.exportTypeHeader;
            this.worksheet.Cells["B3"].Value = "Products";
            this.worksheet.Cells["A4"].Value = this.exportDateHeader;
            this.worksheet.Cells["B4"].Value = System.DateTime.Now.ToString("F");
            colourMeta(this.worksheet.Cells["A3:B3"]);
            colourMeta(this.worksheet.Cells["A4:B4"]);
            this.worksheet.Column(2).Width = 50;

            // write headers
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1].Value = "Product ID";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 2].Value = "Product Number";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 3].Value = "Description";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 4].Value = "Quantity";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 5].Value = "Price";
            applyThickAllBorders(this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1, SPREADSHEET_HEADER_ROW, 5]);
            colourHeader(this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1, SPREADSHEET_HEADER_ROW, 5]);

            // write data
            int row = SPREADSHEET_ROW_OFFSET;
            for (int i = 0; i < this.productList.Count; i++)
            {
                Product product = this.productList[i];
                this.worksheet.Cells[row, 1].Value = product.getProductID().ToString();
                this.worksheet.Cells[row, 2].Value = product.getProductIDNumber();
                this.worksheet.Cells[row, 3].Value = product.getDescription();
                this.worksheet.Cells[row, 4].Value = product.getQuantity();
                this.worksheet.Cells[row, 5].Value = product.getPrice().ToString();
                applyRightThickBorders(this.worksheet.Cells[row, 5]);
                // even and odd rows have alternating colours
                if (row%2==0)
                {
                    for (int col = 1; col <= 5; col++)
                    {
                        colourEvenRow(this.worksheet.Cells[row, col]);
                    }
                }
                else
                {
                    for (int col = 1; col <= 5; col++)
                    {
                        colourOddRow(this.worksheet.Cells[row, col]);
                    }
                }

                // if this is the last element in the list, apply the outside thick border
                if (i == this.productList.Count - 1)
                {
                    for (int col = 1; col <= 5; col++)
                    {
                        applyBottomThickBorders(this.worksheet.Cells[row, col]);
                    }
                }

                row++;
            }
        }
    }

    public class TransactionSpreadsheetExport : SpreadsheetExport
    {
        public List<Transaction> transactionList;

        public override void retrieveData()
        {
            // ask the Model for a list of all transactions
            transactionList = TransactionOps.getAllTransactions();
        }

        public override void prepareSpreadsheet()
        {
            // create spreadsheet and worksheet
            this.spreadsheet = new ExcelPackage();
            this.worksheet = this.spreadsheet.Workbook.Worksheets.Add("Products");

            // write metadata
            this.worksheet.Cells["A1:B1"].Merge = true;
            this.worksheet.Cells["A1"].Value = this.title;
            applyThickAllBorders(this.worksheet.Cells["A1"]);
            colourMeta(this.worksheet.Cells["A1"]);

            this.worksheet.Cells["A2:B2"].Merge = true;
            this.worksheet.Cells["A2"].Value = this.exportHeader;
            applyThickAllBorders(this.worksheet.Cells["A2"]);
            colourMeta(this.worksheet.Cells["A2"]);

            this.worksheet.Cells["A3"].Value = this.exportTypeHeader;
            this.worksheet.Cells["B3"].Value = "Transactions";
            this.worksheet.Cells["A4"].Value = this.exportDateHeader;
            this.worksheet.Cells["B4"].Value = System.DateTime.Now.ToString("F");
            colourMeta(this.worksheet.Cells["A3:B3"]);
            colourMeta(this.worksheet.Cells["A4:B4"]);
            this.worksheet.Column(2).Width = 50;

            // write headers
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1].Value = "Transaction ID";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 2].Value = "Timestamp";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 3].Value = "Customer ID";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 4].Value = "Customer name";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 5].Value = "Staff ID";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 6].Value = "Staff name";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 7].Value = "Product ID";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 8].Value = "Product Number";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 9].Value = "Product description";
            this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 10].Value = "Product Price";
            applyThickAllBorders(this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1, SPREADSHEET_HEADER_ROW, 10]);
            colourHeader(this.worksheet.Cells[SPREADSHEET_HEADER_ROW, 1, SPREADSHEET_HEADER_ROW, 10]);

            // write data
            int row = SPREADSHEET_ROW_OFFSET;
            for (int i = 0; i < this.transactionList.Count; i++)
            {
                Transaction transaction = this.transactionList[i];
                // transaction data
                this.worksheet.Cells[row, 1].Value = transaction.getTransactionID();
                this.worksheet.Cells[row, 2].Value = transaction.getTimestamp();
                // customer data
                if (transaction.getCustomer() != null)
                {
                    this.worksheet.Cells[row, 3].Value = transaction.getCustomer().getID().ToString();
                    this.worksheet.Cells[row, 4].Value = transaction.getCustomer().getName();
                }
                // TODO: add remaining fields
                // staff data
                this.worksheet.Cells[row, 5].Value = transaction.getStaff().getID().ToString();
                this.worksheet.Cells[row, 6].Value = transaction.getStaff().getName();
                // TODO: add remaining fields
                // product data
                this.worksheet.Cells[row, 7].Value = transaction.getProduct().getProductID();
                this.worksheet.Cells[row, 8].Value = transaction.getProduct().getProductIDNumber();
                this.worksheet.Cells[row, 9].Value = transaction.getProduct().getDescription();
                this.worksheet.Cells[row, 10].Value = transaction.getProduct().getPrice().ToString();
                applyRightThickBorders(this.worksheet.Cells[row, 10]);
                // even and odd rows have alternating colours
                if (row%2==0)
                {
                    for (int col = 1; col <= 10; col++)
                    {
                        colourEvenRow(this.worksheet.Cells[row, col]);
                    }
                }
                else
                {
                    for (int col = 1; col <= 10; col++)
                    {
                        colourOddRow(this.worksheet.Cells[row, col]);
                    }
                }

                // if this is the last element in the list, apply the outside thick border
                if (i == this.transactionList.Count - 1)
                {
                    for (int col = 1; col <= 10; col++)
                    {
                        applyBottomThickBorders(this.worksheet.Cells[row, col]);
                    }
                }

                row++;
            }
        }
    }
}
