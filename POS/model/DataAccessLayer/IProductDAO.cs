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
        List<Product> getAllProducts();
        void deleteProduct(Product product);
        void addProduct(Product product);
        void updateProduct(Product product);
    }
}
