using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace UnitTests
{
    [TestClass]
    public class getProductTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            long productID = 6970634010383;
            ProductDAO dao = new ProductDAO();

            // act
            Product product = dao.getProduct(productID);

            // assert
            Assert.AreEqual(product.getProductID(), 6970634010383);
        }
    }
}
