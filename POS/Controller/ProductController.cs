using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ProductController
    {
        private static ProductController instance;

        private ProductController()
        { }

        public static ProductController getInstance()
        {
            if (instance!=null)
            {
                instance = new ProductController();
            }

            return instance;
        }

        public void addProduct(long id, string description, int quantity, float price)
        { }

        public void deleteProduct(long id)
        { }
    }
}
