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
        IList<Transaction> getAllTransactions();
        void deleteTransaction(Transaction transaction);
        int addTransaction(Transaction transaction);
    }
}
