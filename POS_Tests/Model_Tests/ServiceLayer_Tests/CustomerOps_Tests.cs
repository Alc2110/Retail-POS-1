using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit;
using NUnit.Framework;
using Model.ObjectModel;
using Model.ServiceLayer;
using FakeItEasy;

namespace POS_Tests.Model_Tests.ServiceLayer_Tests
{
    [TestClass]
    public class CustomerOps_Tests
    {
        // an instance of the class under test
        protected CustomerOps customerService;

        [SetUp]
        public void setup()
        {
            // set up the class under test
            this.customerService = new Model.ServiceLayer.CustomerOps();  
        }

        [TestMethod]
        public void getCustomer_Test()
        {
            // should return either a null or a customer object

            // arrange
            // set up the data access fake
            int id = 1;
            string fullName = "Full Name";
            string address = "Address";
            string phoneNumber = "12345";
            string email = "email@email.com";
            string city = "Somewhere";
            Customer.States state = Customer.States.NSW;
            int postcode = 2000;
            System.Collections.Generic.List<ITransaction> transactions = null;
            var customerDAO = A.Fake<Model.DataAccessLayer.ICustomerDAO>();
            A.CallTo(() => customerDAO.getCustomer(1)).Returns(new Customer(id, fullName, address, phoneNumber, email, city, state, postcode, transactions));
            A.CallTo(() => customerDAO.getCustomer(0)).Returns(null);

            // act
            ICustomer customerThatExists = customerDAO.getCustomer(1);
            ICustomer customerThatDoesNotExist = customerDAO.getCustomer(0);

            // assert
            NUnit.Framework.Assert.IsNull(customerThatDoesNotExist);
            NUnit.Framework.Assert.AreEqual(id, customerThatExists.CustomerID);
            NUnit.Framework.Assert.AreEqual(fullName, customerThatExists.FullName);
            NUnit.Framework.Assert.AreEqual(address, customerThatExists.Address);
            NUnit.Framework.Assert.AreEqual(phoneNumber, customerThatExists.PhoneNumber);
            NUnit.Framework.Assert.AreEqual(email, customerThatExists.Email);
            NUnit.Framework.Assert.AreEqual(city, customerThatExists.City);
            NUnit.Framework.Assert.AreEqual(state, customerThatExists.state);
            NUnit.Framework.Assert.AreEqual(postcode, customerThatExists.Postcode);
            NUnit.Framework.Assert.AreEqual(transactions, customerThatExists.Transactions);
        }
    }
}
