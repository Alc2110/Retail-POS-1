using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace UnitTests
{
    [TestClass]
    public class addProductTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange 
            Product newProduct = new Product(6970634010383,"Mini DSO DS212 Digital Storage Oscilloscope",75,55.95f);
            ProductDAO dao = new ProductDAO();

            // act
            int result = dao.addProduct(newProduct);

            // assert
            Assert.AreEqual(1, result);
        }
    }
}
