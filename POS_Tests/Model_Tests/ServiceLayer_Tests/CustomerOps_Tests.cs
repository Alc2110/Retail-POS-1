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
            // arrange
            // set up the data access fake
            int id = 1;
            var customerDAO = A.Fake<Model.DataAccessLayer.ICustomerDAO>();
            A.CallTo(() => customerDAO.getCustomer(id)).Returns(new Customer(id, "Full Name", "Address", "12345", "email@email.com", "Somewhere", Customer.States.NSW, 2000, null));

            // act

            // assert
        }
    }
}
