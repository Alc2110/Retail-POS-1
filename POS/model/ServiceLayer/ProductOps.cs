using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using Model.DataAccessLayer;
using Model.DataAccessLayer.SqlServerInterface;

namespace Model.ServiceLayer
{
    public class ProductOps : IProductOps
    {
        // data access layer dependency injection
        public IProductDAO dataAccessObj { get; set; }
        // default constructor
        // this still depends on a concrete implementation.
        // however, it is not as tightly-coupled a design as before
        public ProductOps()
        {
            dataAccessObj = new ProductDAO();
        }
        // test constructor
        public ProductOps(IProductDAO dataAccessObj)
        {
            this.dataAccessObj = dataAccessObj;
        }

        public void addProduct(IProduct newProduct)
        {
            dataAccessObj.addProduct(newProduct);

            // fire the event to update the view
            getAllProducts();
        }

        public void deleteProduct(string idNumber)
        {
            dataAccessObj.deleteProduct(idNumber);

            // fire the event to update the view
            getAllProducts();
        }

        public void updateProduct(IProduct product)
        {
            dataAccessObj.updateProduct(product);

            // fire the event to update the view
            getAllProducts();
        }

        public void importUpdateProduct(IProduct product)
        {
            dataAccessObj.importUpdateProduct(product);

            // fire the event to update the view
            getAllProducts();
        }

        public IProduct getProduct(string idNumber)
        {
            return dataAccessObj.getProduct(idNumber);
        }

        public IEnumerable<IProduct> getAllProducts()
        {
            IEnumerable<IProduct> products = dataAccessObj.getAllProducts();

            // fire the event to update the view
            GetAllProducts(this, new GetAllProductsEventArgs(products));

            return products;    
        }

        // event for getting all products
        public event EventHandler<GetAllProductsEventArgs> GetAllProducts;
        protected virtual void OnGetAllProducts (GetAllProductsEventArgs args)
        {
            EventHandler<GetAllProductsEventArgs> tmp = GetAllProducts;
            if (tmp != null)
            {
                GetAllProducts?.Invoke(this, args);
            }
        }
    }

    public interface IProductOps
    {
        void addProduct(IProduct newProduct);
        void deleteProduct(string idNumber);
        void updateProduct(IProduct product);
        void importUpdateProduct(IProduct product);
        IEnumerable<IProduct> getAllProducts();
    }

    /// <summary>
    /// Event arguments class
    /// </summary>
    public class GetAllProductsEventArgs : EventArgs
    {
        private IEnumerable<IProduct> productList;

        // constructor
        public GetAllProductsEventArgs(IEnumerable<IProduct> productList)
        {
            this.productList = productList;
        }

        public IEnumerable<IProduct> getList()
        {
            return productList;
        }
    }
}
