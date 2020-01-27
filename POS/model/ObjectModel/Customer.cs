using System;
using System.Collections;
using System.Collections.Generic;

namespace Model.ObjectModel
{
    public class Customer : IActor
    {
        // default ctor
        public Customer()
        {
        }

        // ctor
        public Customer(int CustomerID, string FullName, string Address, string phoneNumber, string Email, string City, States state, int Postcode, List<Transaction> Transactions)
        {
            this.CustomerID = CustomerID;
            this.FullName = FullName;
            this.Address = Address;
            this.PhoneNumber = phoneNumber;
            this.Email = Email;
            this.City = City;
            this.state = state;
            this.Postcode = Postcode;
            this.Transactions = Transactions;
        }

        public enum States
        {
            NSW,
            Qld,
            Tas,
            ACT,
            Vic,
            SA,
            WA,
            NT,
            Other
        }

        private int CustomerID;
        private string FullName;
        private string Address;
        private string PhoneNumber;
        private string Email;
        private string City;
        private States state;
        private int Postcode;
        private IList<Transaction> Transactions;

        public int getID()
        {
            return CustomerID;
        }

        public void setID(int id)
        {
            this.CustomerID = id;
        }

        public string getName()
        {
            return FullName;
        }

        public void setName(string name)
        {
            this.FullName = name;
        }

        public string getAddress()
        {
            return Address;
        }

        public void setAddress(string address)
        {
            this.Address = address;
        }

        public string getPhoneNumber()
        {
            return PhoneNumber;
        }

        public void setPhoneNumber(string number)
        {
            this.PhoneNumber = number;
        }

        public string getEmail()
        {
            return this.Email;
        }

        public void setEmail(string email)
        {
            this.Email = email;
        }

        public string getCity()
        {
            return this.City;
        }

        public void setCity(string city)
        {
            this.City = city;
        }

        public States getState()
        {
            return this.state;
        }

        public void setState(States state)
        {
            this.state = state;
        }

        public int getPostcode()
        {
            return this.Postcode;
        }

        public void setPostcode(int postcode)
        {
            this.Postcode = postcode;
        }

        public List<Transaction> getTransactions()
        {
            return (List<Transaction>)this.Transactions;
        }

        public void setTransactions(List<Transaction> transactions)
        {
            this.Transactions = transactions;
        }
    }
}