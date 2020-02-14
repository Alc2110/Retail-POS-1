using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ServiceLayer;

namespace Controller
{
    // singleton
    public class CustomerController
    {
        private static CustomerController instance;

        private CustomerController()
        { }

        public static CustomerController getInstance()
        {
            if (instance==null)
            {
                instance = new CustomerController();
            }

            return instance;
        }

        public void addCustomer(string FullName, string streetAddress, string phoneNumber, string Email, string City, string state, int postcode)
        {
            CustomerOps.addCustomer(FullName, streetAddress, phoneNumber, Email, City, state, postcode);
        }

        public void deleteCustomer(int id)
        {
            CustomerOps.deleteCustomer(id);
        }

        public void updateCustomer()
        { }
    }
}
