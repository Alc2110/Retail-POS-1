using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;
using Model.ObjectModel;
using Model.ServiceLayer;
using FakeItEasy;
using Controller;

namespace POS_Tests.Controller_Tests
{
    [TestClass]
    public class CustomerController_Tests
    {
        // instance of the class under test
        protected CustomerController customerController;

        [SetUp]
        public void setup()
        { }

        [TestMethod]
        public void addCustomer_Test()
        {
            // arrange
            int id = 1;
            string fullName = "Full Name";
            string address = "1 Example St";
            string phoneNumber = "1234";
            string email = "email@email.com";
            string city = "Somewhere";
            int postcode = 2000;
            States state = States.NSW;
            Customer newCustomer = new Customer(id, fullName, address, phoneNumber, email, city, state, postcode, null);
            // set up the model fake
            var customerService = A.Fake<Model.ServiceLayer.ICustomerOps>();
            bool addCustomerMethodCalled = false;
            A.CallTo(() => customerService.addCustomer(newCustomer)).Invokes(() => { addCustomerMethodCalled = true; });
            // set up the class under test
            customerController = new CustomerController(customerService);

            // act
            customerController.addCustomer(newCustomer);

            // assert
            NUnit.Framework.Assert.IsTrue(addCustomerMethodCalled);
        }

        [TestMethod]
        public void updateCustomer_Test()
        {
            // arrange
            int id = 1;
            string fullName = "Full Name";
            string address = "1 Example St";
            string phoneNumber = "1234";
            string email = "email@email.com";
            string city = "Somewhere";
            int postcode = 2000;
            States state = States.NSW;
            Customer newCustomer = new Customer(id, fullName, address, phoneNumber, email, city, state, postcode, null);
            // set up the model fake
            var customerService = A.Fake<Model.ServiceLayer.ICustomerOps>();
            bool updateCustomerMethodCalled = false;
            A.CallTo(() => customerService.updateCustomer(newCustomer)).Invokes(() => { updateCustomerMethodCalled = true; });
            // set up the class under test
            customerController = new CustomerController(customerService);

            // act
            customerController.updateCustomer(newCustomer);

            // assert
            NUnit.Framework.Assert.IsTrue(updateCustomerMethodCalled);
        }

        [TestMethod]
        public void importUpdateCustomer_Test()
        {
            // arrange
            int id = 1;
            string fullName = "Full Name";
            string address = "1 Example St";
            string phoneNumber = "1234";
            string email = "email@email.com";
            string city = "Somewhere";
            int postcode = 2000;
            States state = States.NSW;
            Customer newCustomer = new Customer(id, fullName, address, phoneNumber, email, city, state, postcode, null);
            // set up the model fake
            var customerService = A.Fake<Model.ServiceLayer.ICustomerOps>();
            bool updateCustomerMethodCalled = false;
            A.CallTo(() => customerService.importUpdateCustomer(newCustomer)).Invokes(() => { updateCustomerMethodCalled = true; });
            // set up the class under test
            customerController = new CustomerController(customerService);

            // act
            customerController.importUpdateCustomer(newCustomer);

            // assert
            NUnit.Framework.Assert.IsTrue(updateCustomerMethodCalled);
        }

        [TestMethod]
        public void deleteCustomer_Test()
        {
            // arrange
            int id = 1;
            // set up the model fake
            var customerService = A.Fake<Model.ServiceLayer.ICustomerOps>();
            bool deleteCustomerMethodCalled = false;
            A.CallTo(() => customerService.deleteCustomer(id)).Invokes(() => { deleteCustomerMethodCalled = true; });
            // set up the class under test
            this.customerController = new CustomerController(customerService);

            // act
            customerController.deleteCustomer(id);

            // assert
            NUnit.Framework.Assert.IsTrue(deleteCustomerMethodCalled);
        }
    }
}
