using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Model.ObjectModel;
using Model.ServiceLayer;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace POS.View
{
    // TODO: fix open/closed principle violation in this class
    /// <summary>
    /// Factory class for creating Views for exporting data to spreadsheets.
    /// </summary>
    public class SpreadsheetExportFactory
    {
        public SpreadsheetExport getSpreadsheetExportView(string exportType)
        {
            switch (exportType)
            {
                case "Staff":
                    return new StaffSpreadsheetExport(exportType);
                case "Customer":
                    return new CustomerSpreadsheetExport(exportType);
                case "Product":
                    return new ProductSpreadsheetExport(exportType);
                case "Transaction":
                    return new TransactionSpreadsheetExport(exportType);
                default:
                    // this shouldn't be allowed to happen!
                    // TODO: handle it properly
                    throw new Exception("Invalid spreadsheet export type requested");
            }
        }
    }

    // TODO: continue factoring out common code
    /// <summary>
    /// Base class for the spreadsheet export Views.
    /// </summary>
    public abstract class SpreadsheetExport
    {
        // create an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string exportType;
        protected string[] headers;

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

        // ctor
        public SpreadsheetExport(string exportType)
        {
            this.exportType = exportType;
        }

        // spreadsheet processing methods
        protected abstract void writeData();

        public void prepareSpreadsheet()
        {
            writeData();
            // create spreadsheet and worksheet
            this.spreadsheet = new ExcelPackage();
            this.worksheet = this.spreadsheet.Workbook.Worksheets.Add(exportType);

            // write metadata
            this.worksheet.Cells["A1:B1"].Merge = true;
            this.worksheet.Cells["A1"].Value = this.title;
            
            colourMeta(this.worksheet.Cells["A1:B1"]);

            this.worksheet.Cells["A2:B2"].Merge = true;
            this.worksheet.Cells["A2"].Value = this.exportHeader;
            
            colourMeta(this.worksheet.Cells["A2:B2"]);

            this.worksheet.Cells["A3"].Value = this.exportTypeHeader;
            this.worksheet.Cells["B3"].Value = exportType;
            this.worksheet.Cells["A4"].Value = this.exportDateHeader;
            this.worksheet.Cells["B4"].Value = System.DateTime.Now.ToString("F"); // get the current timestamp
            colourMeta(this.worksheet.Cells["A3:B3"]);
            applyThinBorders(this.worksheet.Cells["A3:B3"]);
            applyThinBorders(this.worksheet.Cells["A4:B4"]);
            colourMeta(this.worksheet.Cells["A4:B4"]);
            this.worksheet.Column(2).Width = 50;

            applyThickAllBorders(this.worksheet.Cells["A1:B1"]);
            applyThickAllBorders(this.worksheet.Cells["A2:B2"]);
            applyBottomThickBorders(this.worksheet.Cells["A4:B4"]);
            applyRightThickBorders(this.worksheet.Cells["B3"]);
            applyRightThickBorders(this.worksheet.Cells["B4"]);

            // write headers
            for (int i = 0; i < headers.Length; i++)
            {
                this.worksheet.Cells[Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, i + 1].Value = headers[i];
            }
            applyThickAllBorders(this.worksheet.Cells[Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, 1, 
                                 Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, headers.Length]);
            colourHeader(this.worksheet.Cells[Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, 1, 
                        Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, headers.Length]);

            // write data
            //writeData();
        }

        public abstract void retrieveData();

        /// <summary>
        /// Save the spreadsheet as an Excel file.
        /// </summary>
        /// <param name="exportType">string. Describes the type of list being exported. Will be shown in file dialog title</param>
        public void saveSpreadsheet()
        {
 
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

                // at this point, it succeeded
                // tell the user and the logger
                System.Windows.Forms.MessageBox.Show("Saved spreadsheet file", "Retail POS",
                                                     System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Information);
                logger.Info("Saved spreadsheet file");
            }
        }

        // spreadsheet styling methods
        // borders
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
        // colours
        protected void applyColourToCell(ExcelRange cells, System.Drawing.Color colour)
        {
            cells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(colour);
        }
        protected void colourEvenRow(ExcelRange cells)
        {
            applyColourToCell(cells, dataColour1);
        }
        protected void colourOddRow(ExcelRange cells)
        {
            applyColourToCell(cells, dataColour2);
        }
        protected void colourMeta(ExcelRange cells)
        {
            applyColourToCell(cells, metaColour);
        }
        protected void colourHeader(ExcelRange cells)
        {
            applyColourToCell(cells, headerColour);
        }
    }

    public class CustomerSpreadsheetExport : SpreadsheetExport
    {
        private List<ICustomer> customerList;

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events fired from the model
        private delegate void getCustomersListDelegate(object sender, GetAllCustomersEventArgs args);

        // ctor
        public CustomerSpreadsheetExport(string exportType) : base(exportType)
        {
            this.headers = new string[] { "Customer ID", "Full name", "Address", "Phone number", "Email", "City", "State", "Postcode" };
            customerList = new List<ICustomer>();
        }

        // event handler for model events
        private void loadDataEventHandler(object sender, GetAllCustomersEventArgs args)
        {
            this.customerList = args.getList().ToList();
        }

        // fetch the data from the database and import it to this class
        public async override void retrieveData()
        {
            // subscribe to model events
            POS.Configuration.customerOps.GetAllCustomers += loadDataEventHandler;

            try
            {
                // ask the model for a list of all customers
                // run this operation in a separate thread
                await Task.Run(() =>
                {
                    customerList =  POS.Configuration.customerOps.getAllCustomers().ToList();
                });
            }
            catch (Exception ex)
            {
                string retrieveDataErrorMessage = "Error retrieving data from database: " + ex.Message;
                MessageBox.Show(retrieveDataErrorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, retrieveDataErrorMessage);
                logger.Error("Stack Trace: " + ex.StackTrace);
            }
        }

        protected override void writeData()
        {
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            for (int i = 0; i < customerList.Count; i++)
            {
                Customer currCustomer = (Customer)customerList[i];
                worksheet.Cells[row, 1].Value = currCustomer.CustomerID.ToString();
                worksheet.Cells[row, 2].Value = currCustomer.FullName;
                worksheet.Cells[row, 3].Value = currCustomer.Address;
                worksheet.Cells[row, 4].Value = currCustomer.PhoneNumber;
                worksheet.Cells[row, 5].Value = currCustomer.Email;
                worksheet.Cells[row, 6].Value = currCustomer.City;
                worksheet.Cells[row, 7].Value = currCustomer.state.ToString();
                worksheet.Cells[row, 8].Value = currCustomer.Postcode.ToString();
                applyRightThickBorders(worksheet.Cells[row, 8]);
                // even and odd rows have alternating colours
                if (row % 2 == 0)
                {
                    for (int col = 1; col <= 8; col++)
                    {
                        colourEvenRow(worksheet.Cells[row, col]);
                    }
                }
                else
                {
                    for (int col = 1; col <= 8; col++)
                    {
                        colourOddRow(worksheet.Cells[row, col]);
                    }
                }

                // if this is the last element in the list, apply the outside thick border
                if (i == customerList.Count - 1)
                {
                    for (int col = 1; col <= 8; col++)
                    {
                        applyBottomThickBorders(worksheet.Cells[row, col]);
                    }
                }

                row++;
            }
        }
    }

    public class StaffSpreadsheetExport : SpreadsheetExport
    {
        private List<IStaff> staffList;

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events fired from the model
        private delegate void getStafflistDelegate(object sender, GetAllStaffEventArgs args);

        // ctor
        public StaffSpreadsheetExport(string exportType) : base(exportType)
        {
            headers = new string[] { "Staff ID", "Full name", "Password hash", "Priveleges" };
            staffList = new List<IStaff>();
        }

        // event handler for model events
        private void loadDataEventHandler(object sender, GetAllStaffEventArgs args)
        {
            staffList = args.getList().ToList();
        }

        // fetch the data from the database and import it to this class
        public async override void retrieveData()
        {
            // subscribe to model events
            POS.Configuration.staffOps.GetAllStaff += loadDataEventHandler;

            try
            {
                // ask the model for a list of all staff
                // run this operation in a separate thread
                await Task.Run(() =>
                {
                    staffList = POS.Configuration.staffOps.getAllStaff().ToList();
                });
            }
            catch (Exception ex)
            {
                string retrieveDataErrorMessage = "Error retrieving data from database: " + ex.Message;
                MessageBox.Show(retrieveDataErrorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, retrieveDataErrorMessage);
                logger.Error("Stack Trace: " + ex.StackTrace);
            }
        }

        protected override void writeData()
        {
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            for (int i = 0; i < this.staffList.Count; i++)
            {
                Staff staff = (Staff)staffList[i];
                worksheet.Cells[row, 1].Value = staff.StaffID.ToString();
                //this.worksheet.Cells[row, 1].Style.Numberformat.Format = "0"; 
                worksheet.Cells[row, 2].Value = staff.FullName;
                worksheet.Cells[row, 3].Value = staff.PasswordHash;
                worksheet.Cells[row, 4].Value = staff.privelege.ToString();
                applyRightThickBorders(worksheet.Cells[row, 4]);
                // even and odd rows have alternating colours
                if (row % 2 == 0)
                {
                    for (int col = 1; col <= 4; col++)
                    {
                        colourEvenRow(worksheet.Cells[row, col]);
                    }
                }
                else
                {
                    for (int col = 1; col <= 4; col++)
                    {
                        colourOddRow(worksheet.Cells[row, col]);
                    }
                }

                // if this is the last element in the list, apply the outside thick border
                if (i == staffList.Count - 1)
                {
                    for (int col = 1; col <= 4; col++)
                    {
                        applyBottomThickBorders(worksheet.Cells[row, col]);
                    }
                }

                row++;
            }
        }
    }

    public class ProductSpreadsheetExport : SpreadsheetExport
    {
        private List<IProduct> productList;

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events fired from the model
        private delegate void getProductsListDelegate(object sender, GetAllProductsEventArgs args);

        // ctor
        public ProductSpreadsheetExport(string exportType) : base(exportType)
        {
            headers = new string[] { "Product ID", "Product number", "Description", "Quantity available", "Price" };
            productList = new List<IProduct>();
        }

        // event handler for model events
        private void loadDataEventHandler(object sender, GetAllProductsEventArgs args)
        {
            productList = args.getList().ToList();
        }

        // fetch the data from the database and import it to this class
        public async override void retrieveData()
        {
            // subscribe to model events
            POS.Configuration.productOps.GetAllProducts += loadDataEventHandler;

            try
            {
                // ask the model for a list of products
                // run this operation in a separate thread
                await Task.Run(() =>
                {
                    productList = POS.Configuration.productOps.getAllProducts().ToList();
                    Debug.WriteLine("Retrieved " + productList.Count + " product records.");
                });
            }
            catch (Exception ex)
            {
                string retrieveDataErrorMessage = "Error retrieving data from database: " + ex.Message;
                MessageBox.Show(retrieveDataErrorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, retrieveDataErrorMessage);
                logger.Error("Stack Trace: " + ex.StackTrace);
            }
        }

        protected override void writeData()
        {
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            for (int i = 0; i < productList.Count; i++)
            {
                Product product = (Product)productList[i];
                worksheet.Cells[row, 1].Value = product.ProductID.ToString();
                worksheet.Cells[row, 2].Value = product.ProductIDNumber;
                worksheet.Cells[row, 3].Value = product.Description;
                worksheet.Cells[row, 4].Value = product.Quantity;
                worksheet.Cells[row, 5].Value = product.price.ToString();

                applyRightThickBorders(worksheet.Cells[row, 5]);
                // even and odd rows have alternating colours
                if (row % 2 == 0)
                {
                    for (int col = 1; col <= 5; col++)
                    {
                        colourEvenRow(worksheet.Cells[row, col]);
                    }
                }
                else
                {
                    for (int col = 1; col <= 5; col++)
                    {
                        colourOddRow(worksheet.Cells[row, col]);
                    }
                }

                // if this is the last element in the list, apply the outside thick border
                if (i == productList.Count - 1)
                {
                    for (int col = 1; col <= 5; col++)
                    {
                        applyBottomThickBorders(worksheet.Cells[row, col]);
                    }
                }

                row++;
            }
        }
    }

    public class TransactionSpreadsheetExport : SpreadsheetExport
    {
        public List<ITransaction> transactionList;

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events from the model
        private delegate void getTransactionsListDelegate(object sender, GetAllTransactionsEventArgs args);

        // ctor
        public TransactionSpreadsheetExport(string exportType) : base(exportType)
        {
            // TODO: add remaining fields
            headers = new string[] { "Transaction ID", "Timestamp", "Customer ID", "Customer name", "Salesperson ID", "Salesperson name",
                                          "Product ID", "Product number", "Product description", "Product price" };
            transactionList = new List<ITransaction>();
        }

        // event handler for model events
        private void loadDataEventHandler(object sender, GetAllTransactionsEventArgs args)
        {
            transactionList = args.getList().ToList();
        }

        // fetch the data from the database and import it to this class
        public async override void retrieveData()
        {
            // subscribe to model events
            POS.Configuration.transactionOps.GetAllTransactions += loadDataEventHandler;

            try
            {
                // ask the model for a list of transactions
                // run this operation in a separate thread
                await Task.Run(() =>
                {
                    transactionList = POS.Configuration.transactionOps.getAllTransactions().ToList();
                });
            }
            catch (Exception ex)
            {
                string retrieveDataErrorMessage = "Error retrieving data from database: " + ex.Message;
                MessageBox.Show(retrieveDataErrorMessage, "Retail POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(ex, retrieveDataErrorMessage);
                logger.Error("Stack Trace: " + ex.StackTrace);
            }
        }

        protected override void writeData()
        {
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            for (int i = 0; i < this.transactionList.Count; i++)
            {
                Transaction transaction = (Transaction)this.transactionList[i];
                // transaction data
                worksheet.Cells[row, 1].Value = transaction.TransactionID;
                worksheet.Cells[row, 2].Value = transaction.Timestamp;
                // customer data
                if (transaction.customer != null)
                {
                    worksheet.Cells[row, 3].Value = transaction.customer.CustomerID.ToString();
                    worksheet.Cells[row, 4].Value = transaction.customer.FullName;
                }
                // TODO: add remaining fields
                // staff data
                worksheet.Cells[row, 5].Value = transaction.staff.StaffID.ToString();
                worksheet.Cells[row, 6].Value = transaction.staff.FullName;
                // TODO: add remaining fields
                // product data
                worksheet.Cells[row, 7].Value = transaction.product.ProductID;
                worksheet.Cells[row, 8].Value = transaction.product.ProductIDNumber;
                worksheet.Cells[row, 9].Value = transaction.product.Description;
                worksheet.Cells[row, 10].Value = transaction.product.price.ToString();

                applyRightThickBorders(this.worksheet.Cells[row, 10]);
                // even and odd rows have alternating colours
                if (row % 2 == 0)
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