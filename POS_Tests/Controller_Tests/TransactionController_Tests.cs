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
    public class TransactionController_Tests
    {
        // instance of the class under test
        protected TransactionController transactionController;

        [SetUp]
        public void setup()
        { }

        [TestMethod]
        public void addTransaction_Test()
        {
            // arrange
            int id = 1;
            string timestamp = "timestamp";
            Transaction newTransaction = new Transaction(id, timestamp, null, null, null);
            // set up the model fake
            var transactionService = A.Fake<Model.ServiceLayer.ITransactionOps>();
            bool addTransactionMethodCalled = false;
            A.CallTo(() => transactionService.addTransaction(newTransaction)).Invokes(() => addTransactionMethodCalled = true);
            // set up the class under test
            transactionController = new TransactionController(transactionService);

            // act
            transactionController.addTransaction(newTransaction);

            // assert
            NUnit.Framework.Assert.IsTrue(addTransactionMethodCalled);
        }
    }
}
