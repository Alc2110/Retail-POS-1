using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;

namespace Model.DataAccessLayer
{
    public interface ITransactionDAO
    {
        string connString { get; set; }

        IEnumerable<ITransaction> getAllTransactions();
        void addTransaction(ITransaction transaction);
    }
}
