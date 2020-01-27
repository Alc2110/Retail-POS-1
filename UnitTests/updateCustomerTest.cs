using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace UnitTests
{
    [TestClass]
    public class updateCustomerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            Customer oldCustomer = new Customer(3, "Jimmy Smith", "111 Parkwood North Road", "234128", "jimmyrocks@gmail.com", "Parkwood", Customer.States.NSW, 2245,null);
            Customer newCustomer = new Customer(3, "Jimmy Smith", "33 Alien St", "234128", "jimmyrocks@gmail.com", "Lunar Park", Customer.States.NSW, 2249, null);
            CustomerDAO dao = new CustomerDAO();

            // act
            int result = dao.updateCustomer(newCustomer, oldCustomer);

            // assert
            Assert.AreEqual(1, result);
        }
    }
}
