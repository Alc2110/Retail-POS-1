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
        public Staff salesperson;
        public Customer customer;

        public void addEntry(Transaction transaction)
        {
            // add it to the list
            transactions.Add(transaction);
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
}
