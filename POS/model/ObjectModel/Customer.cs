using System;
using System.Collections;
using System.Collections.Generic;

namespace Model.ObjectModel
{
    public class Customer : ICustomer
    {
        // default constructor
        public Customer()
        {
        }

        // constructor with parameters
        public Customer(int CustomerID, string FullName, string Address, string phoneNumber, string Email, string City, 
                        Model.ObjectModel.States state, int Postcode, IEnumerable<ITransaction> Transactions)
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

        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public Model.ObjectModel.States state { get; set; }
        public int Postcode { get; set; }
        public IEnumerable<ITransaction> Transactions { get; set; }
    }

    public interface ICustomer
    {
        int CustomerID { get; set; }
        string FullName { get; set; }
        string Address { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        string City { get; set; }
        Model.ObjectModel.States state { get; set; }
        int Postcode { get; set; }
        IEnumerable<ITransaction> Transactions { get; set; }
    }
}