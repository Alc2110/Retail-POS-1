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
        // default ctor
        public ProductController()
        { }

        public void addProduct(string idNumber, string description, int quantity, float price)
        {
            Product newProduct = new Product();
            newProduct.setProductIDNumber(idNumber);
            newProduct.setDescription(description);
            newProduct.setQuantity(quantity);
            newProduct.setPrice(price);

            POS.Configuration.productOps.addProduct(newProduct);
        }

        public void updateProduct(Product toUpdate)
        {
            POS.Configuration.productOps.updateProduct(toUpdate);
        }

        public void deleteProduct(string idNumber)
        {
            POS.Configuration.productOps.deleteProduct(idNumber);
        }
    }
}
