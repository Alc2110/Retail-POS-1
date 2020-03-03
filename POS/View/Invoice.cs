using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;

namespace POS.View
{
    public abstract class Invoice : IInvoice
    {
        public List<Transaction> transactions;

        public void addEntry()
        {
            // ask the model to retrieve the transaction object,
            // based on the supplied information in the parameters
            Transaction trans = new Transaction();
            // TODO: implement this

            // add it to the list
            transactions.Add(trans);
        }

        public void removeEntry()
        {
            // remove the transaction object
        }

        // save the file in one of the specified formats
        public abstract void save();
    }

    public class PDFInvoice : Invoice
    {
        public override void save()
        {
            // save the PDF file
        }
    }

    public class ExcelInvoice : Invoice
    {
        public override void save()
        {
            // save the spreadsheet file
        }
    }

    public class CSVInvoice : Invoice
    {
        public override void save()
        {
            // save the CSV file
        }
    }

    public class PlainTextInvoice : Invoice
    {
        public override void save()
        {
            // save the text file
        }
    }

    public class JSONInvoice : Invoice
    {
        public override void save()
        {
            // save the JSON file
        }
    }

    public class XMLInvoice : Invoice
    {
        public override void save()
        {
            // save the XML file
        }
    }
}
