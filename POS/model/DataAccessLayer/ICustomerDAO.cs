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
        IList<Customer> getAllCustomers();
        int deleteCustomer(Customer customer);
        void addCustomer(Customer customer);
        void updateCustomer(Customer customer);
    }
}
