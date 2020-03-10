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
        IList<Product> getAllProducts();
        int deleteProduct(Product product);
        int addProduct(Product product);
        void updateProduct(Product product);
    }
}
