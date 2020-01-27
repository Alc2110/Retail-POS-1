using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace UnitTests
{
    [TestClass]
    public class addCustomerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            Customer customer = new Customer(0, "Alex Chlabicz", "11 Coleman Ave", "0406147234","alexchlabicz@gmail.com", "Homebush", Customer.States.NSW, 2140, null);
            CustomerDAO dao = new CustomerDAO();

            // act
            int result = dao.addCustomer(customer);

            // assert
            Assert.AreEqual(1, result);
        }
    }
}
