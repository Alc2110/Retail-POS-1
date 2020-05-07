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
        string connString { get; set; }

        IEnumerable<ICustomer> getAllCustomers();
        ICustomer getCustomer(int id);
        void deleteCustomer(int id);
        void addCustomer(ICustomer customer);
        void updateCustomer(ICustomer customer);
        void importUpdateCustomer(ICustomer customer);
    }
}
