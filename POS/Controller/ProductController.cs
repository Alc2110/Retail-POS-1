using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ServiceLayer;
using Model.ObjectModel;

namespace Controller
{
    public class ProductController
    {
        public IProductOps service { get; set; }

        // default constructor
        public ProductController()
        {
            service = POS.Configuration.productOps;
        }

        // test constructor
        public ProductController(IProductOps service)
        {
            this.service = service;
        }

        public void addProduct(IProduct newProduct)
        {
            service.addProduct(newProduct);
        }

        // TODO: currently a redundant method. Deal with this.
        public void updateProduct(IProduct toUpdate)
        {
            service.updateProduct(toUpdate);
        }

        public void deleteProduct(string idNumber)
        {
            service.deleteProduct(idNumber);
        }
    }
}
