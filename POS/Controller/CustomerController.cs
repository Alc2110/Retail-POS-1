using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ServiceLayer;
using Model.ObjectModel;

namespace Controller
{
    public class CustomerController
    {
        public ICustomerOps service { get; set; }

        // default constructor
        public CustomerController()
        {
            service = POS.Configuration.customerOps;
        }

        // test constructor
        public CustomerController(ICustomerOps service)
        {
            this.service = service;
        }

        public void addCustomer(ICustomer newcustomer)
        {
            service.addCustomer(newcustomer);
        }

        public void deleteCustomer(int id)
        {
            service.deleteCustomer(id);
        }

        public void updateCustomer(ICustomer toUpdate)
        {
            service.updateCustomer(toUpdate);
        }

        // TODO: redundant method. Deal with this.
        public void importUpdateCustomer(ICustomer toUpdate)
        {
            service.importUpdateCustomer(toUpdate);
        }
    }
}
