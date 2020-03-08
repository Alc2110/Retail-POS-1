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
    public abstract class SpreadsheetImport
    {
        // spreadsheet row constants
        // TODO: these are also in SpreadsheetExport. Must factor out.
        protected int SPREADSHEET_HEADER_ROW = 6;
        protected int SPREADSHEET_ROW_OFFSET = 7;

        // spreadsheet objects
        protected string filePath;
        protected ExcelPackage spreadsheet;
        protected ExcelWorksheet worksheet;

        // spreadsheet handling methods
        public abstract void openSpreadsheet();
        public abstract void import();
        public abstract void importUpdate();
        protected bool isSpreadsheetValidated()
        {
            // validate spreadsheet
            FileInfo fi = new FileInfo(filePath);
            spreadsheet = new ExcelPackage(fi);
            worksheet = spreadsheet.Workbook.Worksheets[1];
            // TODO: improve this validation
            //if (!((worksheet.Cells["A1"].Value.Equals(Configuration.STORE_NAME)) && (worksheet.Cells["A2"].Value.Equals("Database Export"))))
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
        // check if cells are null or contain empty string
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

    public class ProductsSpreadsheetImport : SpreadsheetImport
    {
        private List<Product> spreadsheetProductList = new List<Product>();
        private List<Product> databaseProductList = new List<Product>();

        public override void openSpreadsheet()
        {
            filePath = string.Empty;

            try
            {
                // open the file with a dialog
                using (System.Windows.Forms.OpenFileDialog openSpreadsheetDialog = new System.Windows.Forms.OpenFileDialog())
                {
                    openSpreadsheetDialog.Filter = "Excel spreadsheet |*.xlsx";

                    if (openSpreadsheetDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                    {
                        // get the path of the file
                        filePath = openSpreadsheetDialog.FileName;
                    }
                }

                // create the spreadsheet objects
                FileInfo fi = new FileInfo(filePath);
                this.spreadsheet = new ExcelPackage(fi);
                this.worksheet = this.spreadsheet.Workbook.Worksheets[1];
            }
            catch (Exception ex)
            {
                // it failed
                // pass it up
                throw;
            }
        }

        public override void import()
        {
            throw new NotImplementedException();
        }

        public override void importUpdate()
        {
            // at this point, the spreadsheet is assumed to have been validated
            // if anything is wrong, throw an error

            // import data
            // read spreadsheet
            int row = SPREADSHEET_ROW_OFFSET;
            while (spreadsheetCellsHaveData(this.worksheet.Cells[row,1]))
            {
                Product currRowProduct = new Product();
                currRowProduct.setProductID(Int32.Parse(this.worksheet.Cells[row, 1].Value.ToString()));
                currRowProduct.setProductIDNumber(this.worksheet.Cells[row, 2].Value.ToString());
                currRowProduct.setDescription(this.worksheet.Cells[row, 3].Value.ToString());
                currRowProduct.setQuantity(Int32.Parse(this.worksheet.Cells[row, 4].Value.ToString()));
                currRowProduct.setPrice(float.Parse(this.worksheet.Cells[row, 5].Value.ToString()));
               
                this.spreadsheetProductList.Add(currRowProduct);

                row++;
            }
            // database
            databaseProductList = ProductOps.getAllProducts();

            // do the comparison
            // for existing products in database (unique id's are product number and description, update them).
            foreach (Product spreadsheetProduct in this.spreadsheetProductList)
            {
                foreach (Product databaseProduct in this.databaseProductList)
                {
                    if (spreadsheetProduct.Equals(databaseProduct))
                    {
                        // product number (barcode) and description match
                        // update
                        databaseProduct.setQuantity(spreadsheetProduct.getQuantity());
                        databaseProduct.setPrice(spreadsheetProduct.getPrice());
                    }
                }
            }

            // send the imported/updated data to the model and database
            foreach (Product databaseProduct in this.databaseProductList)
            {
                ProductOps.updateProduct(databaseProduct);
            }
        }
    }
}
