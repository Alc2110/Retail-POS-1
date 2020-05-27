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
    public class ProductController_Tests
    {
        // instance of the class under test
        protected ProductController productController;

        [SetUp]
        public void setup()
        { }
        
        [TestMethod]
        public void deleteProduct_Test()
        {
            // arrange
            string idNumber = "123";
            // set up the model fake
            var productService = A.Fake<Model.ServiceLayer.IProductOps>();
            bool deleteProductMethodCalled = false;
            A.CallTo(() => productService.deleteProduct(idNumber)).Invokes(() => deleteProductMethodCalled = true);
            // set up the class under test
            productController = new ProductController(productService);

            // act
            productController.deleteProduct(idNumber);

            // assert
            NUnit.Framework.Assert.IsTrue(deleteProductMethodCalled);
        }

        [TestMethod]
        public void addProduct_Test()
        {
            // arrange
            long id = 1;
            string idNumber = "123";
            string description = "Product description";
            int quantity = 1;
            float price = 1.0f;
            Product newProduct = new Product(id, idNumber, description, quantity, price);
            // set up the model fake
            var productService = A.Fake<Model.ServiceLayer.IProductOps>();
            bool addProductMethodCalled = false;
            A.CallTo(() => productService.addProduct(newProduct)).Invokes(() => addProductMethodCalled = true);
            // set up the class under test
            productController = new ProductController(productService);

            // act
            productController.addProduct(newProduct);

            // assert
            NUnit.Framework.Assert.IsTrue(addProductMethodCalled);
        }

        [TestMethod]
        public void updateProduct_Test()
        {
            // arrange
            long id = 1;
            string idNumber = "123";
            string description = "Product description";
            int quantity = 1;
            float price = 1.0f;
            Product newProduct = new Product(id, idNumber, description, quantity, price);
            // set up the model fake
            var productService = A.Fake<Model.ServiceLayer.IProductOps>();
            bool updateProductMethodCalled = false;
            A.CallTo(() => productService.updateProduct(newProduct)).Invokes(() => updateProductMethodCalled = true);
            // set up the class under test
            productController = new ProductController(productService);

            // act
            productController.updateProduct(newProduct);

            // assert
            NUnit.Framework.Assert.IsTrue(updateProductMethodCalled);
        }
    }
}
