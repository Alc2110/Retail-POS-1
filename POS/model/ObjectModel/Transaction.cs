using System;
using System.Collections;
using System.Collections.Generic;

namespace Model.ObjectModel
{
    public class Transaction : ITransaction
    {
        // default constructor
        public Transaction()
        {
            Timestamp = null;
        }

        // constructor with parameters
        public Transaction(int TransactionID, string Timestamp, ICustomer customer, IStaff staff, IProduct Product)
        {
            this.TransactionID = TransactionID;
            this.Timestamp = Timestamp;
            this.customer = customer;
            this.staff = staff;
            this.product = Product;
        }

        public int TransactionID { get; set; }
        public string Timestamp { get; set; }
        public ICustomer customer { get; set; }
        public IStaff staff { get; set; }
        public IProduct product { get; set; }
    }

    public interface ITransaction
    {
        int TransactionID { get; set; }
        string Timestamp { get; set; }
        ICustomer customer { get; set; }
        IStaff staff { get; set; }
        IProduct product { get; set; }
    }
}