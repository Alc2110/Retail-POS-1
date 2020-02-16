using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ServiceLayer;

namespace Controller
{
    public class ProductController
    {
        private static ProductController instance;

        private ProductController()
        { }

        public static ProductController getInstance()
        {
            if (instance==null)
            {
                instance = new ProductController();
            }

            return instance;
        }

        //public void addProduct(long id, string idNumber, string description, int quantity, float price)
        public void addProduct(string idNumber, string description, int quantity, float price)
        {
            ProductOps.addProduct(idNumber, description, quantity, price);
        }

        public void deleteProduct(long id)
        { }
    }
}
