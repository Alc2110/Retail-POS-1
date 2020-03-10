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
        public static void addProduct(Product newProduct)
        {
            // DAO
            ProductDAO dao = new ProductDAO();
            dao.addProduct(newProduct);

            // fire the event
            getAllProducts();
        }

        public static void updateProduct(Product product)
        {
            // DAO
            ProductDAO dao = new ProductDAO();
            dao.updateProduct(Convert.ToInt32(product.getProductID()), product.getProductIDNumber(), product.getDescription(), product.getQuantity(), product.getPrice());

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
