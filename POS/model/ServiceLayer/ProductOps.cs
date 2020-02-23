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
        public static void addProduct(string idNumber, string description, int quantity, float price)
        {
            // object
            Product newProduct = new Product();
            //newProduct.setProductID(id);
            newProduct.setProductIDNumber(idNumber);
            newProduct.setDescription(description);
            newProduct.setQuantity(quantity);
            newProduct.setPrice(price);

            // DAO
            ProductDAO dao = new ProductDAO();
            dao.addProduct(newProduct);

            // fire the event
            getAllProducts();
        }

        public static Product getProduct(string idNumber)
        {
            // DAO
            // retrieve from database
            ProductDAO dao = new ProductDAO();
            return dao.getProduct(idNumber);
        }

        public static void decrementQuantity(string idNumber)
        {
            // DAO
            ProductDAO dao = new ProductDAO();
            
        }

        public static List<Product> getAllProducts()
        {
            // DAO
            // retrieve from database
            ProductDAO dao = new ProductDAO();
            List<Product> products = (List<Product>)dao.getAllProducts();

            // fire the event
            OnGetAllProducts(null, new GetAllProductsEventArgs(products));

            return products;
        }

        // event for getting all products
        public static event EventHandler<GetAllProductsEventArgs> OnGetAllProducts = delegate { };
    }

    public class GetAllProductsEventArgs : EventArgs
    {
        private List<Product> list;

        // ctor
        public GetAllProductsEventArgs(List<Product> productList)
        {
            this.list = productList;
        }

        public List<Product> getList()
        {
            return this.list;
        }
    }
}
