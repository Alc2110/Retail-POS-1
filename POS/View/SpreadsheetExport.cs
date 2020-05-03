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
            writeData();
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
        private List<Customer> customerList;

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events fired from the model
        private delegate void getCustomersListDelegate(object sender, GetAllCustomersEventArgs args);

        // ctor
        public CustomerSpreadsheetExport(string exportType) : base(exportType)
        {
            this.headers = new string[] { "Customer ID", "Full name", "Address", "Phone number", "Email", "City", "State", "Postcode" };
            customerList = new List<Customer>();
        }

        // event handler for model events
        private void loadDataEventHandler(object sender, GetAllCustomersEventArgs args)
        {
            this.customerList = args.getList();
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
                    POS.Configuration.customerOps.getAllCustomers();
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
            for (int i = 0; i < this.customerList.Count; i++)
            {
                Customer currCustomer = this.customerList[i];
                this.worksheet.Cells[row, 1].Value = currCustomer.getID().ToString();
                this.worksheet.Cells[row, 2].Value = currCustomer.getName();
                this.worksheet.Cells[row, 3].Value = currCustomer.getAddress();
                this.worksheet.Cells[row, 4].Value = currCustomer.getPhoneNumber();
                this.worksheet.Cells[row, 5].Value = currCustomer.getEmail();
                this.worksheet.Cells[row, 6].Value = currCustomer.getCity();
                this.worksheet.Cells[row, 7].Value = currCustomer.getState().ToString();
                this.worksheet.Cells[row, 8].Value = currCustomer.getPostcode().ToString();
                applyRightThickBorders(this.worksheet.Cells[row, 8]);
                // even and odd rows have alternating colours
                if (row % 2 == 0)
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

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events fired from the model
        private delegate void getStafflistDelegate(object sender, GetAllStaffEventArgs args);

        // ctor
        public StaffSpreadsheetExport(string exportType) : base(exportType)
        {
            this.headers = new string[] { "Staff ID", "Full name", "Password hash", "Priveleges" };
            this.staffList = new List<Staff>();
        }

        // event handler for model events
        private void loadDataEventHandler(object sender, GetAllStaffEventArgs args)
        {
            this.staffList = args.getList();
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
                    POS.Configuration.staffOps.getAllStaff();
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
                Staff staff = this.staffList[i];
                this.worksheet.Cells[row, 1].Value = staff.getID().ToString();
                this.worksheet.Cells[row, 1].Style.Numberformat.Format = "0"; 
                this.worksheet.Cells[row, 2].Value = staff.getName();
                this.worksheet.Cells[row, 3].Value = staff.getPasswordHash();
                this.worksheet.Cells[row, 4].Value = staff.getPrivelege().ToString();
                applyRightThickBorders(this.worksheet.Cells[row, 4]);
                // even and odd rows have alternating colours
                if (row % 2 == 0)
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
                if (i == this.staffList.Count - 1)
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

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events fired from the model
        private delegate void getProductsListDelegate(object sender, GetAllProductsEventArgs args);

        // ctor
        public ProductSpreadsheetExport(string exportType) : base(exportType)
        {
            this.headers = new string[] { "Product ID", "Product number", "Description", "Quantity available", "Price" };
            this.productList = new List<Product>();
        }

        // event handler for model events
        private void loadDataEventHandler(object sender, GetAllProductsEventArgs args)
        {
            this.productList = args.getList();
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
                    POS.Configuration.productOps.getAllProducts();
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
                if (row % 2 == 0)
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

        // get an instance of the logger for this class
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // delegate for handling events from the model
        private delegate void getTransactionsListDelegate(object sender, GetAllTransactionsEventArgs args);

        // ctor
        public TransactionSpreadsheetExport(string exportType) : base(exportType)
        {
            // TODO: add remaining fields
            this.headers = new string[] { "Transaction ID", "Timestamp", "Customer ID", "Customer name", "Salesperson ID", "Salesperson name",
                                          "Product ID", "Product number", "Product description", "Product price" };
            this.transactionList = new List<Transaction>();
        }

        // event handler for model events
        private void loadDataEventHandler(object sender, GetAllTransactionsEventArgs args)
        {
            this.transactionList = args.getList();
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
                    POS.Configuration.transactionOps.getAllTransactions();
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