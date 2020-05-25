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
    public class ProductOps_Tests
    {
        // an instance of the class under test
        protected ProductOps productService;

        [SetUp]
        public void setup()
        {
            // initialise the class under test
            productService = new Model.ServiceLayer.ProductOps();
        }

        [TestMethod]
        public void getProduct_Test()
        {
            // should return either a null or a product object

            // arrange
            // set up the data access fake
            int id = 1;
            string idNumber = "123";
            string description = "A product description";
            int quantity = 1;
            float price = 0;
            var productDAO = A.Fake<Model.DataAccessLayer.IProductDAO>();
            A.CallTo(() => productDAO.getProduct(idNumber)).Returns(new Product(id, idNumber, description, quantity, price));
            A.CallTo(() => productDAO.getProduct("0")).Returns(null);

            // act
            IProduct productThatExists = productDAO.getProduct(idNumber);
            IProduct productThatDoesNotExist = productDAO.getProduct("0");

            // assert
            NUnit.Framework.Assert.IsNull(productThatDoesNotExist);
            NUnit.Framework.Assert.AreEqual(id, productThatExists.ProductID);
            NUnit.Framework.Assert.AreEqual(idNumber, productThatExists.ProductIDNumber);
            NUnit.Framework.Assert.AreEqual(description, productThatExists.Description);
            NUnit.Framework.Assert.AreEqual(quantity, productThatExists.Quantity);
            NUnit.Framework.Assert.AreEqual(price, productThatExists.price);
        }
    }
}
