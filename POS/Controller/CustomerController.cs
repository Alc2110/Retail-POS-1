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
        // default ctor
        public CustomerController()
        { }

        public void addCustomer(string FullName, string streetAddress, string phoneNumber, string Email, string City, string state, int postcode)
        {
            POS.Configuration.customerOps.addCustomer(FullName, streetAddress, phoneNumber, Email, City, state, postcode);
        }

        public void deleteCustomer(int id)
        {
            POS.Configuration.customerOps.deleteCustomer(id);
        }

        public void updateCustomer(Customer toUpdate)
        {
            POS.Configuration.customerOps.updateCustomer(toUpdate);
        }
    }
}
