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
           
        }
        
        [TestMethod]
        public void getTransaction_Test()
        {
            // this should return either a null or a collection of transaction objects

            // arrange
            // set up the data access fake
            var transationDAO = A.Fake<Model.DataAccessLayer.ITransactionDAO>();
            A.CallTo(() => transationDAO.getAllTransactions()).Returns(new List<ITransaction> { new Transaction(1, "timestamp", null, null, null) });
            // set up the class under test
            transactionService = new TransactionOps(transationDAO);
            // subscribe to the event
            bool eventFired = false;
            this.transactionService.GetAllTransactions += (sender, args) => { eventFired = true; };

            // act
            IEnumerable<ITransaction> collectionOfTransactions = this.transactionService.getAllTransactions();

            // assert
            NUnit.Framework.Assert.IsTrue(eventFired);
            foreach (ITransaction trans in collectionOfTransactions)
            {
                NUnit.Framework.Assert.AreEqual(1, trans.TransactionID);
            }
        }

        [TestMethod]
        public void addTransaction_Test()
        {
            // arrange
            // set up the data access fake
            var transationDAO = A.Fake<Model.DataAccessLayer.ITransactionDAO>();
            bool addTransactionMethodCalled = false;
            Transaction newTransaction = new Transaction(1, "timestamp", null, null, null);
            A.CallTo(() => transationDAO.addTransaction(newTransaction)).Invokes(() => addTransactionMethodCalled = true);
            // set up the class under test
            transactionService = new TransactionOps(transationDAO);
            // subscribe to event
            bool eventFired = false;
            this.transactionService.GetAllTransactions += (sender, args) => { eventFired = true; };

            // act
            this.transactionService.addTransaction(newTransaction);

            // assert
            NUnit.Framework.Assert.IsTrue(addTransactionMethodCalled);
            NUnit.Framework.Assert.IsTrue(eventFired);
        }
    }
}
