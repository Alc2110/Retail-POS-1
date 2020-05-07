using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace Model.ServiceLayer
{
    /// <summary>
    /// Interacts with the data access layer.
    /// The data access object could be replaced with another implementing that interface.
    /// </summary>
    public class CustomerOps : ICustomerOps
    {
        // data access layer dependency injection
        public ICustomerDAO dataAccessObj { get; set; }
        
        // default constructor
        // this still depends on a concrete implementation.
        // however, it is not as tightly-coupled a design as before
        public CustomerOps()
        {
            dataAccessObj = new CustomerDAO();
        }
        // test constructor
        public CustomerOps(ICustomerDAO dataAccessObj)
        {
            this.dataAccessObj = dataAccessObj;
        }
  
        public void addCustomer(ICustomer newCustomer)
        {
            dataAccessObj.addCustomer(newCustomer);

            // fire the event to update the view
            getAllCustomers();
        }

        public void updateCustomer(ICustomer customer)
        {
            dataAccessObj.updateCustomer(customer);

            // fire the event to update the view
            getAllCustomers();
        }

        public void importUpdateCustomer(ICustomer customer)
        {
            dataAccessObj.importUpdateCustomer(customer);
        }

        public void deleteCustomer(int id)
        {
            dataAccessObj.deleteCustomer(id);

            // fire the event to update the view 
            getAllCustomers();
        }

        public ICustomer getCustomer(int id)
        {
            return dataAccessObj.getCustomer(id);
        }

        public IEnumerable<ICustomer> getAllCustomers()
        {
            IEnumerable<ICustomer> allCustomers = dataAccessObj.getAllCustomers();

            // fire the event
            GetAllCustomers(this, new GetAllCustomersEventArgs(allCustomers));

            return allCustomers;
        }

        // event for getting all customers 
        public event EventHandler<GetAllCustomersEventArgs> GetAllCustomers;
        protected virtual void OnGetAllCustomers (GetAllCustomersEventArgs e)
        {
            GetAllCustomers?.Invoke(this, e);
        }
    }

    public interface ICustomerOps
    {
        void addCustomer(ICustomer customer);
        void updateCustomer(ICustomer customer);
        void importUpdateCustomer(ICustomer customer);
        void deleteCustomer(int id);
        ICustomer getCustomer(int id);
        IEnumerable<ICustomer> getAllCustomers();
    }

    /// <summary>
    /// Event arguments class.
    /// </summary>
    public class GetAllCustomersEventArgs : EventArgs
    {
        private IEnumerable<ICustomer> customerList;

        // ctor
        public GetAllCustomersEventArgs(IEnumerable<ICustomer> customerList)
        {
            this.customerList = customerList;
        }

        public IEnumerable<ICustomer> getList()
        {
            return this.customerList;
        }
    }
    //GetAllCustomersEventArgs
}
