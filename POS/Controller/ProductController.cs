using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ServiceLayer;
using Model.ObjectModel;

namespace Controller
{
    public class ProductController
    {
        private static ProductController instance;

        private ProductController()
        { }

        public static ProductController getInstance()
        {
            if (instance==null)
            {
                instance = new ProductController();
            }

            return instance;
        }

        //public void addProduct(long id, string idNumber, string description, int quantity, float price)
        public void addProduct(string idNumber, string description, int quantity, float price)
        {
            Product newProduct = new Product();
            newProduct.setProductIDNumber(idNumber);
            newProduct.setDescription(description);
            newProduct.setQuantity(quantity);
            newProduct.setPrice(price);

            ProductOps.addProduct(newProduct);
        }

        public void deleteProduct(long id)
        { }
    }
}
