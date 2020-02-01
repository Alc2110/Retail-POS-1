using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        public void deleteCustomer(int id)
        { }

        public void updateCustomer()
        { }
    }
}
