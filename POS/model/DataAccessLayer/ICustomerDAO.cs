using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;

namespace Model.DataAccessLayer
{
    public interface ICustomerDAO
    {
        List<Customer> getAllCustomers();
        void deleteCustomer(Customer customer);
        void addCustomer(Customer customer);
        void updateCustomer(Customer customer);
    }
}
