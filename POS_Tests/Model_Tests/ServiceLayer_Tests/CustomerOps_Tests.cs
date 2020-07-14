using System;
using System.Collections.Generic;
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
            States state = States.NSW;
            int postcode = 2000;
            System.Collections.Generic.List<ITransaction> transactions = null;
            var customerDAO = A.Fake<Model.DataAccessLayer.ICustomerDAO>();
            A.CallTo(() => customerDAO.getCustomer(1)).Returns(new Customer(id, fullName, address, phoneNumber, email, city, state, postcode, transactions));
            A.CallTo(() => customerDAO.getCustomer(0)).Returns(null);
            // set up the class under test
            this.customerService = new Model.ServiceLayer.CustomerOps(customerDAO);

            // act
            ICustomer customerThatExists = this.customerService.getCustomer(1);
            ICustomer customerThatDoesNotExist = this.customerService.getCustomer(0);

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

        [TestMethod]
        public void addCustomer_Test()
        {
            // arrange
            // set up the data access fake
            int id = 1;
            string fullName = "Full Name";
            string address = "Address";
            string phoneNumber = "12345";
            string email = "email@email.com";
            string city = "Somewhere";
            States state = States.NSW;
            int postcode = 2000;
            System.Collections.Generic.List<ITransaction> transactions = null;
            Customer newCustomer = new Customer(id, fullName, address, phoneNumber, email, city, state, postcode, transactions);
            var customerDAO = A.Fake<Model.DataAccessLayer.ICustomerDAO>();
            bool addCustomerMethodCalled = false;
            A.CallTo(() => customerDAO.addCustomer(newCustomer)).Invokes(() => addCustomerMethodCalled = true);
            // set up the class under test
            this.customerService = new Model.ServiceLayer.CustomerOps(customerDAO);
            // subscribe to the event
            bool eventFired = false;
            this.customerService.GetAllCustomers += (sender, args) => { eventFired = true; };

            // act
            this.customerService.addCustomer(newCustomer);

            // assert
            NUnit.Framework.Assert.IsTrue(eventFired);
            NUnit.Framework.Assert.IsTrue(addCustomerMethodCalled);
        }

        [TestMethod]
        public void deleteCustomer_Test()
        {
            // arrange
            // set up the data access fake
            int id = 1;
            var customerDAO = A.Fake<Model.DataAccessLayer.ICustomerDAO>();
            bool deleteCustomerMethodCalled = false;
            A.CallTo(() => customerDAO.deleteCustomer(id)).Invokes(() => deleteCustomerMethodCalled = true);
            // set up the class under test
            this.customerService = new Model.ServiceLayer.CustomerOps(customerDAO);
            // subscribe to the event
            bool eventFired = false;
            this.customerService.GetAllCustomers += (sender, args) => { eventFired = true; };

            // act
            this.customerService.deleteCustomer(id);

            // assert
            NUnit.Framework.Assert.IsTrue(eventFired);
            NUnit.Framework.Assert.IsTrue(deleteCustomerMethodCalled);
        }

        [TestMethod]
        public void updateCustomer_Test()
        {
            // arrange
            // set up the data access fake
            var customerDAO = A.Fake<Model.DataAccessLayer.ICustomerDAO>();
            bool updateCustomerMethodCalled = false;
            int id = 1;
            string fullName = "Full Name";
            string address = "Address";
            string phoneNumber = "12345";
            string email = "email@email.com";
            string city = "Somewhere";
            States state = States.NSW;
            int postcode = 2000;
            System.Collections.Generic.List<ITransaction> transactions = null;
            Customer newCustomer = new Customer(id, fullName, address, phoneNumber, email, city, state, postcode, transactions);
            A.CallTo(() => customerDAO.updateCustomer(newCustomer)).Invokes(() => updateCustomerMethodCalled=true);
            // set up the class under test
            this.customerService = new Model.ServiceLayer.CustomerOps(customerDAO);
            // subscribe to the event
            bool eventFired = false;
            this.customerService.GetAllCustomers += (sender, args) => { eventFired = true; };

            // act
            this.customerService.updateCustomer(newCustomer);

            // assert
            NUnit.Framework.Assert.IsTrue(eventFired);
            NUnit.Framework.Assert.IsTrue(updateCustomerMethodCalled);
        }

        [TestMethod]
        public void importUpdateCustomer_Test()
        {
            /*
            // arrange
            // set up the data access fake
            var customerDAO = A.Fake<Model.DataAccessLayer.ICustomerDAO>();
            bool importUpdateCustomerMethodCalled = false;
            int id = 1;
            string fullName = "Full Name";
            string address = "Address";
            string phoneNumber = "12345";
            string email = "email@email.com";
            string city = "Somewhere";
            Customer.States state = Customer.States.NSW;
            int postcode = 2000;
            System.Collections.Generic.List<ITransaction> transactions = null;
            Customer newCustomer = new Customer(id, fullName, address, phoneNumber, email, city, state, postcode, transactions);
            A.CallTo(() => customerDAO.importUpdateCustomer(newCustomer)).Invokes(() => importUpdateCustomerMethodCalled = true);
            // set up the class under test
            // subscribe to the event
            bool eventFired = false;
            this.customerService.GetAllCustomers += (sender, args) => { eventFired = true; };

            // act
            this.customerService.importUpdateCustomer(newCustomer);

            // assert
            NUnit.Framework.Assert.IsTrue(eventFired);
            NUnit.Framework.Assert.IsTrue(importUpdateCustomerMethodCalled);
            */
        }
        
        [TestMethod]
        public void getAllCustomers_Test()
        {
            // arrange
            // set up the data access fake
            int id = 1;
            string fullName = "Full Name";
            string address = "Address";
            string phoneNumber = "12345";
            string email = "email@email.com";
            string city = "Somewhere";
            States state = States.NSW;
            int postcode = 2000;
            System.Collections.Generic.List<ITransaction> transactions = null;
            var customerDAO = A.Fake<Model.DataAccessLayer.ICustomerDAO>();
            A.CallTo(() => customerDAO.getAllCustomers()).Returns(new System.Collections.Generic.List<ICustomer> { new Customer(id, fullName, address, phoneNumber, email, city, state, postcode, transactions) });
            // set up the class under test
            this.customerService = new Model.ServiceLayer.CustomerOps(customerDAO);
            // subscribe to the event
            bool eventFired = false;
            this.customerService.GetAllCustomers += (sender, args) => { eventFired = true; };

            // act
            IEnumerable<ICustomer> collectionOfCustomers = this.customerService.getAllCustomers();

            // assert
            NUnit.Framework.Assert.IsTrue(eventFired);
            foreach (ICustomer customer in collectionOfCustomers)
            {
                NUnit.Framework.Assert.AreEqual(id, customer.CustomerID);
            }
        }
    }
}
