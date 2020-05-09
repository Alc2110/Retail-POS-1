using Model.ServiceLayer;
using System.Collections.Generic;
using System;
using Model.ObjectModel;

namespace Controller
{
    public class TransactionController
    {
        public ITransactionOps service { get; set; }

        // default constructor
        public TransactionController()
        {
            this.service = POS.Configuration.transactionOps;
        }

        public TransactionController(ITransactionOps service)
        {
            this.service = service;
        }

        public void addTransaction(ITransaction newTransaction)
        {
            service.addTransaction(newTransaction);
        }
    }
}