 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using Model.DataAccessLayer;

namespace Model.ServiceLayer
{
    public class TransactionOps
    {
        public void addTransaction(ValueTuple<int,int,Dictionary<string,int>> items)
        {
            // DAO
            TransactionDAO dao = new TransactionDAO();
            dao.addTransaction(items);

            // fire the event
            getAllTransactions();
        }

        public List<Transaction> getAllTransactions()
        {
            // DAO
            // retrieve from database
            TransactionDAO dao = new TransactionDAO();
            List<Transaction> transactionsList = dao.getAllTransactions();

            // fire the event
            GetAllTransactions(this, new GetAllTransactionsEventArgs(transactionsList));

            return transactionsList;
        }

        // event for getting all transactions
        public event EventHandler<GetAllTransactionsEventArgs> GetAllTransactions;
        protected virtual void OnGetAllTransactions (GetAllTransactionsEventArgs args)
        {
            GetAllTransactions?.Invoke(this, args);
        }
    }

    /// <summary>
    /// Event arguments class.
    /// </summary>
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
