using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using Model.ServiceLayer;
using OfficeOpenXml;
using System.IO;
using System.Diagnostics;

namespace POS.Controller
{
    // TODO: continue factoring out common code
    /// <summary>
    /// Generates a controller for importing any type of records.
    /// </summary>
    public class SpreadsheetImportFactory
    {
        public SpreadsheetImport getImportSpreadsheetController(string importType)
        {
            switch (importType)
            {
                case "Staff":
                    return new StaffSpreadsheetImport(importType);
                case "Customer":
                    return new CustomerSpreadsheetImport(importType);
                case "Product":
                    return new ProductSpreadsheetImport(importType);
                default:
                    // shouldn't be allowed to happen
                    throw new Exception("Invalid spreadsheet import type");
            }
        }
    }

    /// <summary>
    /// Base class for a spreadsheet import controller.
    /// </summary>
    public abstract class SpreadsheetImport
    {
        public string importType;
        protected string[] headers;

        // spreadsheet objects
        protected string filePath;
        protected ExcelPackage spreadsheet;
        protected ExcelWorksheet worksheet;

        // get an instance of the logger for this class
        protected static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // ctor
        public SpreadsheetImport(string importType)
        {
        }

        // spreadsheet handling methods
        /// <summary>
        /// Open a spreadsheet file and load it.
        /// </summary>
        public void openSpreadsheet()
        {
            try
            {
                // import the spreadsheet
                // create an open file dialog
                System.Windows.Forms.OpenFileDialog openSpreadsheetDialog = new System.Windows.Forms.OpenFileDialog();
                openSpreadsheetDialog.Filter = "Excel spreadsheet |*.xlsx";
                openSpreadsheetDialog.Title = "Import " + importType + " list as spreadsheet";

                // check if user clicked open button
                if (openSpreadsheetDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                {
                    // get the file info
                    FileInfo fi = new FileInfo(openSpreadsheetDialog.FileName);

                    // get the spreadsheet
                    spreadsheet = new ExcelPackage(fi);
                    worksheet = spreadsheet.Workbook.Worksheets[1];
                }
            }
            catch (Exception ex)
            {
                // it failed
                // pass it up
                throw;
            }
        }
        public abstract void importUpdate();
        protected bool isSpreadsheetValidated()
        {
            // validate spreadsheet
            FileInfo fi = new FileInfo(filePath);
            spreadsheet = new ExcelPackage(fi);
            worksheet = spreadsheet.Workbook.Worksheets[1];
            // TODO: improve this validation
            if ((spreadsheetCellsHaveData(worksheet.Cells["A1"])) && (spreadsheetCellsHaveData(worksheet.Cells["A2"])))
            {
                if ((readSpreadsheetCellData(worksheet.Cells["A1"]).Equals(Configuration.STORE_NAME)) && (readSpreadsheetCellData(worksheet.Cells["A2"]).Equals("Database Export")))
                {
                    // not a valid import
                    return false;
                }
            }

            return true;
        }
        // check if cells are null or contain empty strings
        protected bool spreadsheetCellsHaveData(ExcelRange cells)
        {
            if ((cells.Value != null) && (!(cells.Value.Equals(string.Empty))))
                return true;

            return false;
        }
        // gets the cell value as a string
        protected string readSpreadsheetCellData(ExcelRange cells)
        {
            return cells.Value.ToString();
        }
    }

    public class CustomerSpreadsheetImport : SpreadsheetImport
    {
        public CustomerSpreadsheetImport(string importType) : base(importType)
        {
        }

        public async override void importUpdate()
        {
            // iterate through all the data rows and perform the import/update operation on each
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            while (this.worksheet.Cells[row,2].Value!=null)
            {
                // grab the data
                int id = 0;
                if (this.worksheet.Cells[row,1].Value!=null)
                {
                    id = int.Parse(this.worksheet.Cells[row, 1].Value.ToString());
                }
                string fullName = this.worksheet.Cells[row, 2].Value.ToString();
                string address = this.worksheet.Cells[row, 3].Value.ToString();
                string phoneNumber = this.worksheet.Cells[row, 4].Value.ToString();
                string email = this.worksheet.Cells[row, 5].Value.ToString();
                string city = this.worksheet.Cells[row, 6].Value.ToString();
                string sState = this.worksheet.Cells[row, 7].Value.ToString();
                int postcode = int.Parse(this.worksheet.Cells[row, 8].Value.ToString());

                // build the customer object
                Customer toUpdate = new Customer();
                toUpdate.CustomerID = id;
                toUpdate.FullName = fullName;
                toUpdate.Address = address;
                toUpdate.PhoneNumber = phoneNumber;
                toUpdate.Email = email;
                toUpdate.City = city;
                States state;
                if (Enum.TryParse(sState, out state))
                {
                    toUpdate.state = state;
                }
                else
                {
                    // invalid "State" entry in spreadsheet
                    throw new InvalidDataException("Found invalid entry for field 'State'. Row: " + row);
                }
                toUpdate.Postcode = postcode;

                try
                {
                    // perform this task in a separate thread
                   await Task.Run(() =>
                   {
                       Configuration.customerOps.importUpdateCustomer(toUpdate);
                   });
                }
                catch (Exception ex)
                {
                    // it failed
                    // tell the user and the logger
                    string importFailedMsg = "Error importing data: " + ex.Message;
                    System.Windows.Forms.MessageBox.Show(importFailedMsg, "Retail POS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    logger.Error(ex, importFailedMsg);
                    logger.Error("Stack Trace: " + ex.StackTrace);
                }

                row++;
            }
        }
    }

    public class StaffSpreadsheetImport : SpreadsheetImport
    {
        public StaffSpreadsheetImport(string importType) : base(importType)
        {
        }

        public async override void importUpdate()
        {
            // iterate through all the data rows and perform the import/update operation on each 
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            while (worksheet.Cells[row,2].Value!=null)
            {
                // grab the data
                int id = 0;
                if (worksheet.Cells[row, 1].Value != null)
                {
                    id = Int32.Parse(worksheet.Cells[row, 1].Value.ToString());
                }
                string fullName = worksheet.Cells[row, 2].Value.ToString();
                string passwordHash = worksheet.Cells[row, 3].Value.ToString();
                string privelege = worksheet.Cells[row, 4].Value.ToString();
                Staff staff = new Staff();
                staff.StaffID = id;
                staff.FullName = fullName;
                staff.PasswordHash = passwordHash;
                switch (privelege)
                {
                    case "Admin":
                        staff.privelege = Staff.Privelege.Admin;
                        break;
                    case "Normal":
                        staff.privelege = Staff.Privelege.Normal;
                        break;
                    default:
                        // invalid data
                        throw new InvalidDataException("Found Invalid staff privelege level. Row: " + row);
                }

                try
                {
                    await Task.Run(() =>
                    {
                        // perform the operation on a separate thread
                        Configuration.staffOps.importUpdateStaff(staff);
                    });
                }
                catch (Exception ex)
                {
                    // it failed
                    // tell the user and the logger
                    string importFailedMsg = "Error importing data: " + ex.Message;
                    System.Windows.Forms.MessageBox.Show(importFailedMsg, "Retail POS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    logger.Error(ex, importFailedMsg);
                    logger.Error("Stack Trace: " + ex.StackTrace);
                }

                row++;
            }
        }
    }

    public class ProductSpreadsheetImport : SpreadsheetImport
    {
        public ProductSpreadsheetImport(string importType) : base(importType)
        {
        }

        public async override void importUpdate()
        {
            // iterate through all the data rows and perform the import/update operation on each
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            while (this.worksheet.Cells[row, 2].Value != null)
            {
                // prepare the data
                int id = 0;
                if (worksheet.Cells[row, 1].Value!=null)
                {
                    id = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                }
                string productNumber = worksheet.Cells[row, 2].Value.ToString();
                string description = worksheet.Cells[row, 3].Value.ToString();
                int quantity = Int32.Parse(worksheet.Cells[row, 4].Value.ToString());
                float price = float.Parse(worksheet.Cells[row, 5].Value.ToString());
                Product toUpdate = new Product();
                toUpdate.ProductID = id;
                toUpdate.ProductIDNumber = productNumber;
                toUpdate.Description = description;
                toUpdate.Quantity = quantity;
                toUpdate.price = price;

                try
                {
                    // run this task in a separate thread
                    await Task.Run(() =>
                    {
                        Configuration.productOps.importUpdateProduct(toUpdate);
                    });
                }
                catch (Exception ex)
                {
                    // it failed
                    // tell the user and the logger
                    string importFailedMsg = "Error importing data: " + ex.Message;
                    System.Windows.Forms.MessageBox.Show(importFailedMsg, "Retail POS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    logger.Error(ex, importFailedMsg);
                    logger.Error("Stack Trace: " + ex.StackTrace);
                }

                row++;
            }
        }
    }
}
