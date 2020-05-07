using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;

namespace Model.DataAccessLayer
{
    public interface IProductDAO
    {
        string connString { get; set; }

        IEnumerable<IProduct> getAllProducts();
        void deleteProduct(string idNumber);
        IProduct getProduct(string idNumber);
        void addProduct(IProduct product);
        void updateProduct(IProduct product);
        void importUpdateProduct(IProduct product);
    }
}
