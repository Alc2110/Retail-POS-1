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
        // data access layer dependency injection
        public ITransactionDAO dataAccessObj { get; set; }
        // default constructor
        // this still depends on a concrete implementation.
        // however, it is not as tightly-coupled a design as before
        public TransactionOps()
        {
            dataAccessObj = new TransactionDAO();
        }
        // test constructor

        // this is shit
        public void addTransaction(ValueTuple<int,int,Dictionary<string,int>> items)
        {
            dataAccessObj.addTransaction(items);

            // fire the event to update the view
            getAllTransactions();
        }

        public IEnumerable<ITransaction> getAllTransactions()
        {
            IEnumerable<ITransaction> transactionsList = dataAccessObj.getAllTransactions();

            // fire the event to update the view
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
        private IEnumerable<ITransaction> list;

        // ctor
        public GetAllTransactionsEventArgs(IEnumerable<ITransaction> list)
        {
            this.list = list;
        }

        public IEnumerable<ITransaction> getList()
        {
            return list;
        }
    }
}
