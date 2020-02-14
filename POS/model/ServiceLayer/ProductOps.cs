using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using Model.DataAccessLayer;

namespace Model.ServiceLayer
{
    public static class ProductOps
    {
        public static void addProduct(long id, string description, int quantity, float price)
        {
            // object
            Product newProduct = new Product();
            newProduct.setProductID(id);
            newProduct.setDescription(description);
            newProduct.setQuantity(quantity);
            newProduct.setPrice(price);

            // DAO
            ProductDAO dao = new ProductDAO();
            dao.addProduct(newProduct);
        }
    }
}
