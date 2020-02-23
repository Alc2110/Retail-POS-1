using Model.ServiceLayer;
using System.Collections.Generic;
using System;

namespace Controller
{
    public class TransactionController
    {
        // singleton
        private static TransactionController instance;

        // ctor
        private TransactionController()
        {
        }

        // singleton
        public static TransactionController getInstance()
        {
            if (instance==null)
            {
                instance = new TransactionController();
            }

            return instance;
        }

        /// <summary>
        /// Create a product transaction. Updates all necessary tables in the database.
        /// </summary>
        /// <param name="items">A tuple of (staff id, customer, id Dictionary(product id, number of this product))</param>
        public void addTransaction(ValueTuple<int, int,Dictionary<string,int>> items)
        {
            TransactionOps.addTransaction(items);
        }
    }
}