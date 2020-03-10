using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using Model.ServiceLayer;
using OfficeOpenXml;
using System.IO;

namespace POS.Controller
{
    // TODO: continue factoring out common code
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

    public abstract class SpreadsheetImport
    {
        public string importType;
        protected string[] headers;

        // spreadsheet objects
        protected string filePath;
        protected ExcelPackage spreadsheet;
        protected ExcelWorksheet worksheet;

        // ctor
        public SpreadsheetImport(string importType)
        {

        }

        // spreadsheet handling methods
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

        public override void importUpdate()
        {
            // ask the model for all customers in database
            List<Customer> databaseCustomers = CustomerOps.getAllCustomers();

            // check for any Customer records in the spreadsheet that do not exist in the database (import)
            List<Customer> spreadsheetCustomers = new List<Customer>();
            // read the spreadsheet
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            while (spreadsheetCellsHaveData(this.worksheet.Cells[row,2]))
            {
                Customer currCustomerRecord = new Customer();
                if ((this.worksheet.Cells[row, 1].Value != null) && !(this.worksheet.Cells[row, 1].Value.ToString().Equals(string.Empty)))
                {
                    currCustomerRecord.setID(Int32.Parse(this.worksheet.Cells[row, 1].Value.ToString()));
                }
                else
                {
                    currCustomerRecord.setID(0);
                }
                currCustomerRecord.setName(this.worksheet.Cells[row, 2].Value.ToString());
                currCustomerRecord.setAddress(this.worksheet.Cells[row, 3].Value.ToString());
                currCustomerRecord.setPhoneNumber(this.worksheet.Cells[row, 4].Value.ToString());
                currCustomerRecord.setEmail(this.worksheet.Cells[row, 5].Value.ToString());
                currCustomerRecord.setCity(this.worksheet.Cells[row, 6].Value.ToString());
                switch (this.worksheet.Cells[row, 7].Value.ToString())
                {
                    case "NSW":
                        currCustomerRecord.setState(Customer.States.NSW);
                        break;
                    case "Vic":
                        currCustomerRecord.setState(Customer.States.Vic);
                        break;
                    case "Qld":
                        currCustomerRecord.setState(Customer.States.Qld);
                        break;
                    case "ACT":
                        currCustomerRecord.setState(Customer.States.ACT);
                        break;
                    case "Tas":
                        currCustomerRecord.setState(Customer.States.Tas);
                        break;
                    case "SA":
                        currCustomerRecord.setState(Customer.States.SA);
                        break;
                    case "NT":
                        currCustomerRecord.setState(Customer.States.NT);
                        break;
                    case "Other":
                        currCustomerRecord.setState(Customer.States.Other);
                        break;
                    default:
                        // shouldn't happen
                        throw new Exception("Invalid data in database");
                }
                currCustomerRecord.setPostcode(Int32.Parse(this.worksheet.Cells[row, 8].Value.ToString()));

                spreadsheetCustomers.Add(currCustomerRecord);

                row++;
            }

            foreach (Customer spreadsheetCustomerRecord in spreadsheetCustomers)
            {
                if (!databaseCustomers.Any(c => c.getID()==spreadsheetCustomerRecord.getID()))
                {
                    // no customer exists in database with this ID
                    // insert the record
                    CustomerOps.addCustomer(spreadsheetCustomerRecord);

                    break;
                }

                // customer record already exists
                // update it
                CustomerOps.updateCustomer(spreadsheetCustomerRecord);
            }
        }
    }

    public class StaffSpreadsheetImport : SpreadsheetImport
    {
        public StaffSpreadsheetImport(string importType) : base(importType)
        {

        }

        public override void importUpdate()
        {
            // ask model for all staff records in database
            List<Staff> databaseStaff = StaffOps.getAllStaff();

            // check for any Staff records in the spreadsheet that do not exist in the database (import)
            List<Staff> spreadsheetStaff = new List<Staff>();
            // read the spreadsheet
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            while (spreadsheetCellsHaveData(this.worksheet.Cells[row,2]))
            {
                Staff currStaffRecord = new Staff();
                if ((this.worksheet.Cells[row, 1].Value != null) && !(this.worksheet.Cells[row, 1].Value.ToString().Equals(string.Empty)))
                {
                    currStaffRecord.setID(Int32.Parse(this.worksheet.Cells[row, 1].Value.ToString()));
                }
                else
                {
                    currStaffRecord.setID(0);
                }
                currStaffRecord.setName(this.worksheet.Cells[row, 2].Value.ToString());
                currStaffRecord.setPasswordHash(this.worksheet.Cells[row, 3].Value.ToString());
                switch (this.worksheet.Cells[row,4].Value.ToString())
                {
                    case "Admin":
                        currStaffRecord.setPrivelege(Staff.Privelege.Admin);
                        break;
                    case "Normal":
                        currStaffRecord.setPrivelege(Staff.Privelege.Normal);
                        break;
                    default:
                        // shouldn't happen
                        throw new Exception("Invalid data in spreadsheet");
                }

                spreadsheetStaff.Add(currStaffRecord);

                row++;
            }

            foreach (Staff spreadsheetStaffRecord in spreadsheetStaff)
            {
                if (!spreadsheetStaff.Any(s => s.getID()==spreadsheetStaffRecord.getID()))
                {
                    // no staff record exists in database with this ID
                    // insert the record
                    StaffOps.addStaff(spreadsheetStaffRecord);

                    break;
                }

                // staff record already exists
                // update it
                StaffOps.updateStaff(spreadsheetStaffRecord);
            }
        }
    }

    public class ProductSpreadsheetImport : SpreadsheetImport
    {
        public ProductSpreadsheetImport(string importType) : base(importType)
        {

        }

        public override void importUpdate()
        {
            // ask the model for all product records in database
            List<Product> databaseProducts = ProductOps.getAllProducts();

            // check for any Product records in the spreadsheet that do not exist in the database (import)
            List<Product> spreadsheetProducts = new List<Product>();
            // read the spreadsheet
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            while (spreadsheetCellsHaveData(this.worksheet.Cells[row, 2]))
            {
                Product currProductRecord = new Product();
                if ((this.worksheet.Cells[row, 1].Value != null) && !(this.worksheet.Cells[row, 1].Value.ToString().Equals(string.Empty)))
                {
                    currProductRecord.setProductID(Int32.Parse(this.worksheet.Cells[row, 1].Value.ToString()));
                }
                else
                {
                    currProductRecord.setProductID(0);
                }
                currProductRecord.setProductIDNumber(this.worksheet.Cells[row, 2].Value.ToString());
                currProductRecord.setDescription(this.worksheet.Cells[row, 3].Value.ToString());
                currProductRecord.setQuantity(Int32.Parse(this.worksheet.Cells[row, 4].Value.ToString()));
                currProductRecord.setPrice(float.Parse(this.worksheet.Cells[row, 5].Value.ToString()));

                spreadsheetProducts.Add(currProductRecord);

                row++;
            }

            foreach (Product spreadsheetProductRecord in spreadsheetProducts)
            {
                if (!databaseProducts.Any(c => c.getProductID()==spreadsheetProductRecord.getProductID()))
                {
                    // no product exists in the database with this ID
                    // insert the record
                    ProductOps.addProduct(spreadsheetProductRecord);

                    break;
                }

                // product record already exists
                // update it
                ProductOps.updateProduct(spreadsheetProductRecord);
            }
        }
    }
}
