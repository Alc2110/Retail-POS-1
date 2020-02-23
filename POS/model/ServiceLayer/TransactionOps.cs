using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using Model.DataAccessLayer;

namespace Model.ServiceLayer
{
    public static class TransactionOps
    {
        public static void addTransaction(ValueTuple<int,int,Dictionary<string,int>> items)
        {
            // DAO
            TransactionDAO dao = new TransactionDAO();
            dao.addTransaction(items);

            // fire the event
            getAllTransactions();
        }

        public static List<Transaction> getAllTransactions()
        {
            // DAO
            // retrieve from database
            TransactionDAO dao = new TransactionDAO();
            List<Transaction> transactionsList = (List<Transaction>)dao.getAllTransactions();

            // fire the event
            OnGetAllTransactions(null, new GetAllTransactionsEventArgs(transactionsList));

            return transactionsList;
        }

        // event for getting all transactions
        public static event EventHandler<GetAllTransactionsEventArgs> OnGetAllTransactions = delegate { };
    }

    public class GetAllTransactionsEventArgs : EventArgs
    {
        private List<Transaction> list;

        // ctor
        public GetAllTransactionsEventArgs(List<Transaction> list)
        {
            this.list = list;
        }

        public List<Transaction> getList()
        {
            return this.list;
        }
    }
}
