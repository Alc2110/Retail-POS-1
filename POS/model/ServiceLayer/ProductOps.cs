using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using Model.DataAccessLayer;

namespace Model.ServiceLayer
{
    public class ProductOps
    {
        public void addProduct(Product newProduct)
        {
            // DAO
            ProductDAO dao = new ProductDAO();
            dao.addProduct(newProduct);

            // fire the event
            getAllProducts();
        }

        public void deleteProduct(string idNumber)
        {
            // DAO
            ProductDAO dao = new ProductDAO();
            dao.deleteProduct(idNumber);

            // fire the event
            getAllProducts();
        }

        public void updateProduct(Product product)
        {
            // DAO
            ProductDAO dao = new ProductDAO();
            dao.updateProduct(product);

            // fire the event
            getAllProducts();
        }

        public Product getProduct(string idNumber)
        {
            // DAO
            // retrieve from database
            ProductDAO dao = new ProductDAO();
            return dao.getProduct(idNumber);
        }

        public List<Product> getAllProducts()
        {
            // DAO
            // retrieve from database
            ProductDAO dao = new ProductDAO();
            List<Product> products = (List<Product>)dao.getAllProducts();

            // fire the event
            GetAllProducts(this, new GetAllProductsEventArgs(products));

            return products;
        }

        // event for getting all products
        public event EventHandler<GetAllProductsEventArgs> GetAllProducts;
        protected virtual void OnGetAllProducts (GetAllProductsEventArgs args)
        {
            GetAllProducts?.Invoke(this, args);
        }
    }

    /// <summary>
    /// Event arguments class
    /// </summary>
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
