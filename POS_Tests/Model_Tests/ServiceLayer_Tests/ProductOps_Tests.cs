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
            
        }

        [TestMethod]
        public void getAllProducts_Test()
        {
            // arrange
            // set up the data access fake
            var productDAO = A.Fake<Model.DataAccessLayer.IProductDAO>();


            // act

            // assert
        }

        [TestMethod]
        public void addProduct_Test()
        {
            // arrange
            // set up the data access fake
            int id = 1;
            string idNumber = "123";
            string description = "A product description";
            int quantity = 1;
            float price = 0;
            Product newProduct = new Product(id, idNumber, description, quantity, price);
            var productDAO = A.Fake<Model.DataAccessLayer.IProductDAO>();
            bool addProductMethodCalled = false;
            A.CallTo(() => productDAO.addProduct(newProduct)).Invokes(() => addProductMethodCalled = true);
            // set up the class under test
            this.productService = new ProductOps(productDAO);
            // subscribe to the event
            bool eventFired = false;
            productService.GetAllProducts += (sender, args) => { eventFired = true; };

            // act
            productService.addProduct(newProduct);

            // assert
            NUnit.Framework.Assert.IsTrue(eventFired);
            NUnit.Framework.Assert.IsTrue(addProductMethodCalled);
        }

        [TestMethod]
        public void updateProduct_Test()
        {
            // arrange
            // set up the data access fake
            int id = 1;
            string idNumber = "123";
            string description = "A product description";
            int quantity = 1;
            float price = 0;
            Product newProduct = new Product(id, idNumber, description, quantity, price);
            var productDAO = A.Fake<Model.DataAccessLayer.IProductDAO>();
            bool updateProductMethodCalled = false;
            A.CallTo(() => productDAO.updateProduct(newProduct)).Invokes(() => updateProductMethodCalled = true);
            // set up the class under test
            this.productService = new ProductOps(productDAO);
            // subscribe to the event
            bool eventFired = false;
            productService.GetAllProducts += (sender, args) => { eventFired = true; };

            // act
            productService.updateProduct(newProduct);

            // assert
            NUnit.Framework.Assert.IsTrue(eventFired);
            NUnit.Framework.Assert.IsTrue(updateProductMethodCalled);
        }

        [TestMethod]
        public void importUpdateProduct_Test()
        {
            // arrange
            // set up the data access fake
            int id = 1;
            string idNumber = "123";
            string description = "A product description";
            int quantity = 1;
            float price = 0;
            Product newProduct = new Product(id, idNumber, description, quantity, price);
            var productDAO = A.Fake<Model.DataAccessLayer.IProductDAO>();
            bool updateProductMethodCalled = false;
            A.CallTo(() => productDAO.importUpdateProduct(newProduct)).Invokes(() => updateProductMethodCalled = true);
            // set up the class under test
            this.productService = new ProductOps(productDAO);
            // subscribe to the event
            bool eventFired = false;
            productService.GetAllProducts += (sender, args) => { eventFired = true; };

            // act
            productService.importUpdateProduct(newProduct);

            // assert
            NUnit.Framework.Assert.IsTrue(eventFired);
            NUnit.Framework.Assert.IsTrue(updateProductMethodCalled);
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
            // initialise the class under test
            productService = new Model.ServiceLayer.ProductOps(productDAO);

            // act
            IProduct productThatExists = this.productService.getProduct(idNumber);
            IProduct productThatDoesNotExist = this.productService.getProduct("0");

            // assert
            NUnit.Framework.Assert.IsNull(productThatDoesNotExist);
            NUnit.Framework.Assert.AreEqual(id, productThatExists.ProductID);
            NUnit.Framework.Assert.AreEqual(idNumber, productThatExists.ProductIDNumber);
            NUnit.Framework.Assert.AreEqual(description, productThatExists.Description);
            NUnit.Framework.Assert.AreEqual(quantity, productThatExists.Quantity);
            NUnit.Framework.Assert.AreEqual(price, productThatExists.price);
        }

        [TestMethod]
        public void deleteProduct_Test()
        {
            // arrange
            // set up the data access fake
            string productIDNumber = "123";
            var productDAO = A.Fake<Model.DataAccessLayer.IProductDAO>();
            bool deleteProductMethodCalled = false;
            A.CallTo(() => productDAO.deleteProduct(productIDNumber)).Invokes(() => deleteProductMethodCalled = true);
            // initialise the class under test
            this.productService = new Model.ServiceLayer.ProductOps(productDAO);
            // subscribe to the event
            bool eventFired = false;
            productService.GetAllProducts += (sender, args) => { eventFired = true; };

            // act
            this.productService.deleteProduct(productIDNumber);

            // assert
            NUnit.Framework.Assert.IsTrue(deleteProductMethodCalled);
            NUnit.Framework.Assert.IsTrue(eventFired);
        }
    }
}
