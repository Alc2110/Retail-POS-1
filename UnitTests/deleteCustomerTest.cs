using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS;
using Model.ObjectModel;
using Model.DataAccessLayer;

namespace UnitTests
{
    [TestClass]
    public class deleteCustomerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange 
            Customer customer = new Customer();
            customer.setID(1002);

            // act 
            CustomerDAO dao = new CustomerDAO();
            int result = dao.deleteCustomer(customer);

            // assert
            Assert.AreEqual(1, result);
        }
    }
}
