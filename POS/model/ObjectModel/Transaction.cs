using System;
using System.Collections;
using System.Collections.Generic;

namespace Model.ObjectModel
{
    public class Transaction
    {
        // default ctor
        public Transaction()
        {
            Timestamp = null;
        }

        // ctor
        public Transaction(int TransactionID, string Timestamp, Customer customer, Staff staff, Product Product)
        {
            this.TransactionID = TransactionID;
            this.Timestamp = Timestamp;
            this.customer = customer;
            this.staff = staff;
            this.Product = Product;
        }

        private int TransactionID;
        private string Timestamp;
        private Customer customer;
        private Staff staff;
        private Product Product;

        public int getTransactionID()
        {
            return TransactionID;
        }

        public void setTransactionID(int id)
        {
            this.TransactionID = id;
        }

        public string getTimestamp()
        {
            return Timestamp;
        }

        public void setTimestamp(string timestamp)
        {
            this.Timestamp = timestamp;
        }

        public void setCustomer(Customer customer)
        {
            this.customer = customer;
        }

        public Customer getCustomer()
        {
            return this.customer;
        }

        public void setStaff(Staff staff)
        {
            this.staff = staff;
        }

        public Staff getStaff()
        {
            return this.staff;
        }

        public void setProduct(Product product)
        {
            this.Product = product;
        }

        public Product getProduct()
        {
            return Product;
        }
    }
}