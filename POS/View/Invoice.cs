﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using PdfSharp;
using OfficeOpenXml;
using Root.Reports;
using System.IO;

namespace POS.View
{
    public abstract class Invoice : Report, IInvoice
    {
        public List<Transaction> transactions;
        public Staff salesperson;
        public Customer customer;

        public void addEntry(Transaction transaction)
        {
            // add it to the list
            transactions.Add(transaction);
        }

        public void removeEntry(Transaction transaction)
        {
            // remove the transaction object
            transactions.Remove(transaction);
        }

        // save the file in one of the specified formats
        public abstract void save();
    }

    public class PDFInvoice : Invoice
    {
        private static double rPosBottom = 278;
        private static double rPosTop = 24;
        private static double rPosLeft = 20;
        private static double rPosRight = 195;

        FontDef fd_text;

        public void generateDocument()
        {
            /*
            // create the report object
            Report invoice = new Report(new PdfFormatter());
            fd_text = new FontDef(invoice, "Arial");
            FontDef fd_header = new FontDef(invoice, "Arial");

            FontProp fontProp_text = new FontProp(fd_text, 1.9);
            FontProp fontProp_Header = new FontPropMM(fd_header, 2.5);
            fontProp_Header.bBold = true;

            // create the table
            TableLayoutManager tlm;
            using (tlm = new TableLayoutManager(fontProp_Header))
            {
                // set the height of the table
                tlm.rContainerHeightMM = rPosBottom - rPosTop;

                // set vertical alignment of all header cells
                tlm.tlmCellDef_Header.rAlignV = RepObj.rAlignCenter;

                // set the bottom line for all cells
                tlm.tlmCellDef_Default.penProp_LineBottom = new PenProp(this, 0.05, System.Drawing.Color.LightGray);
                tlm.tlmHeightMode = TlmHeightMode.AdjustLast;
                tlm.eNewContainer += new TableLayoutManager.NewContainerEventHandler(Tlm_NewContainer);

                // define columns
                TlmColumn col;
                col = new TlmColumnMM(tlm, "ID", 13);

                col = new TlmColumn(tlm, "Transaction ID", 10);

                col = new TlmColumn(tlm, "Timestamp", 20);

                col = new TlmColumn(tlm, "Customer ID", 10);

                col = new TlmColumn(tlm, "Customer name", 30);

                col = new TlmColumn(tlm, "Salesperson ID", 10);

                col = new TlmColumn(tlm, "Product ID", 10);

                col = new TlmColumn(tlm, "Product number", 20);

                col = new TlmColumn(tlm, "Product description", 30);

                col = new TlmColumn(tlm, "Product price", 20);

                // open data set
                System.Data.DataSet dataSet = new System.Data.DataSet();
                using (System.Stream stream_transactions = GetType().Assembly.GetManifestResourceStream())
            }
            */
        }

        public void Tlm_NewContainer(object sender, TableLayoutManager.NewContainerEventArgs ea)
        {
            /*
            new Page(this);

            // first page with caption
            if (page_Cur.iPageNo == 1)
            {
                FontProp fontProp_Title = new FontPropMM(fd_text, 7, System.Drawing.Color.Black);
                fontProp_Title.bBold = true;
                page_Cur.AddCT_MM(rPosLeft + (rPosRight - rPosLeft) / 2, rPosTop, new RepString(fontProp_Title, "Customer List"));
                ea.container.rHeightMM -= fontProp_Title.rLineFeedMM;  // reduce height of table container for the first page
            }

            // the new container must be added to the current page
            page_Cur.AddMM(rPosLeft, rPosBottom - ea.container.rHeightMM, ea.container);
            */
        }

        public override void save()
        {
            // save the PDF file
        }
    }

    public class ExcelInvoice : Invoice
    {
        // spreadsheet objects
        protected ExcelPackage invoiceSpreadsheet;
        protected ExcelWorksheet invoiceWorksheet;
        protected FileInfo fi;

        // data, metadata, headers
        protected string title = Configuration.STORE_NAME;
        protected string invoiceHeader = "Invoice";
        protected string dateHeader = "Date: ";
        protected string[] headers = new string[] { "Timestamp", "Customer ID", "Customer name", "Salesperson ID", "Salesperson name",
                                                    "Product ID", "Product number", "Product description", "Product price" };

        // cell colours
        protected System.Drawing.Color dataColour1 = System.Drawing.Color.LightSkyBlue;
        protected System.Drawing.Color dataColour2 = System.Drawing.Color.PaleTurquoise;
        protected System.Drawing.Color headerColour = System.Drawing.Color.MediumSlateBlue;
        protected System.Drawing.Color metaColour = System.Drawing.Color.MediumSeaGreen;

        public void generateSpreadsheet()
        {
            // create spreadsheet and worksheet
            // create the file path
            // get the path of the executing assembly
            string assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string invoiceFilePath = assemblyPath + @"\invoices\Invoice " + System.DateTime.Now.ToString("F").Replace(':', '-') + ".xlsx";
            fi = new FileInfo(invoiceFilePath);
            invoiceSpreadsheet = new ExcelPackage(fi);
            invoiceWorksheet = this.invoiceSpreadsheet.Workbook.Worksheets.Add("Invoice");
            
            // write metadata
            invoiceWorksheet.Cells["A1:B1"].Merge = true;
            invoiceWorksheet.Cells["A1"].Value = title;
            colourMeta(invoiceWorksheet.Cells["A1:B1"]);
            applyThickAllBorders(invoiceWorksheet.Cells["A1:B1"]);

            invoiceWorksheet.Cells["A2:B2"].Merge = true;
            invoiceWorksheet.Cells["A2"].Value = invoiceHeader;
            colourMeta(invoiceWorksheet.Cells["A2:B2"]);
            applyThickAllBorders(invoiceWorksheet.Cells["A2:B2"]);

            invoiceWorksheet.Cells["A3"].Value = dateHeader;
            invoiceWorksheet.Cells["B3"].Value = System.DateTime.Now.ToString("F");
            colourMeta(invoiceWorksheet.Cells["A3:B3"]);
            applyThickAllBorders(invoiceWorksheet.Cells["A3:B3"]);

            // write headers
            for (int i = 0; i < headers.Length; i++)
            {
                invoiceWorksheet.Cells[Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, i + 1].Value = headers[i];
            }
            applyThickAllBorders(this.invoiceWorksheet.Cells[Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, 1, Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, headers.Length]);
            colourHeader(this.invoiceWorksheet.Cells[Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, 1, Configuration.SpreadsheetConstants.SPREADSHEET_HEADER_ROW, headers.Length]);

            // write data
            int row = Configuration.SpreadsheetConstants.SPREADHSEET_ROW_OFFSET;
            foreach (Transaction trans in transactions)
            {
                invoiceWorksheet.Cells[row, 1].Value = trans.Timestamp;
                if (trans.customer!=null)
                {
                    invoiceWorksheet.Cells[row, 2].Value = trans.customer.CustomerID.ToString();
                    invoiceWorksheet.Cells[row, 3].Value = trans.customer.FullName;
                }
                invoiceWorksheet.Cells[row, 4].Value = trans.staff.StaffID;
                invoiceWorksheet.Cells[row, 5].Value = trans.staff.FullName;

                invoiceWorksheet.Cells[row, 6].Value = trans.product.ProductID;
                invoiceWorksheet.Cells[row, 7].Value = trans.product.ProductIDNumber;
                invoiceWorksheet.Cells[row, 8].Value = trans.product.Description;
                invoiceWorksheet.Cells[row, 9].Value = trans.product.price;

                // apply appropriate styling
                if (row%2==0)
                {
                    invoiceWorksheet.Cells[row,1,row,9].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    invoiceWorksheet.Cells[row, 1, row, 9].Style.Fill.BackgroundColor.SetColor(dataColour1);
                }
                else
                {
                    invoiceWorksheet.Cells[row, 1, row, 9].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    invoiceWorksheet.Cells[row, 1, row, 9].Style.Fill.BackgroundColor.SetColor(dataColour2);
                }

                // next row
                row++;
            }

            // protect worksheet
            invoiceWorksheet.Protection.IsProtected = true;
            invoiceWorksheet.Protection.AllowFormatColumns = true;
        }

        public override void save()
        {
            // save the spreadsheet file
            try
            {
                // try to save the spreadsheet file
                // create the "invoices" directory if it does not exist
                System.IO.Directory.CreateDirectory("invoices");
                invoiceSpreadsheet.SaveAs(fi);
                invoiceSpreadsheet.Dispose();
            }
            catch (Exception e)
            {
                // it failed
                // pass it up
                throw;
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
}
