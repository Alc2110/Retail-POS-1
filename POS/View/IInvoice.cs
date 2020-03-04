using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;

namespace POS.View
{
    public interface IInvoice
    {
        void addEntry(Transaction transaction);
        void removeEntry();
        void save();
    }
}
