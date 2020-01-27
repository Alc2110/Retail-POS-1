using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS;
using Model.ObjectModel;
using Model.DataAccessLayer;

namespace UnitTests
{
    [TestClass]
    public class createTransactionTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            Product testProduct = new Product(9300607410075, "Men's Regaine Extra Strength 4 Months' Value Pack", 49, 50);
            Staff testStaff = new Staff(1, "John Simpson", "209531139914632287718712813122115111523311059024539672002451891383116012173156226155", Staff.Privelege.Admin);
            Transaction testTransaction = new Transaction(0, null, null, testStaff, testProduct);

            // act 
            TransactionDAO testTransactionObj = new TransactionDAO();
            int result = testTransactionObj.addTransaction(testTransaction);

            // assert
            Assert.AreEqual(2, result);
        }
    }
}
