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
    public class TransactionOps_Tests
    {
        // an instance of the class under test
        protected TransactionOps transactionService;

        [SetUp]
        public void setup()
        {
            // set up the class under test
            transactionService = new TransactionOps();
        }

        [TestMethod]
        public void getTransaction_Test()
        {
            // this should return either a null or a collection of transaction objects

            // arrange
            // set up the data access fake
            var transationDAO = A.Fake<Model.DataAccessLayer.ITransactionDAO>();
            A.CallTo(() => transationDAO.getAllTransactions()).Returns(new List<ITransaction> { new Transaction(1, "timestamp", null, null, null) });

            // act
            IEnumerable<ITransaction> collectionOfTransactions = transationDAO.getAllTransactions();

            // assert
            foreach (ITransaction trans in collectionOfTransactions)
            {
                NUnit.Framework.Assert.AreEqual(1, trans.TransactionID);
            }
        }
    }
}
