using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace Model.ServiceLayer
{
    //public static class CustomerOps
    public class CustomerOps
    {
        public void addCustomer(Customer customer)
        {
            // DAO
            CustomerDAO dao = new CustomerDAO();
            dao.addCustomer(customer);

            // fire the event
            getAllCustomers();
        }

        public void addCustomer(string FullName, string streetAddress, string phoneNumber, string Email, string City, string state, int postcode)
        {
            // object
            Customer newCustomer = new Customer();
            newCustomer.setName(FullName);
            newCustomer.setAddress(streetAddress);
            newCustomer.setPhoneNumber(phoneNumber);
            newCustomer.setEmail(Email);
            newCustomer.setCity(City);
            newCustomer.setPostcode(postcode);
            switch (state)
            {
                case "NSW":
                    newCustomer.setState(Customer.States.NSW);
                    break;
                case "Qld":
                    newCustomer.setState(Customer.States.Qld);
                    break;
                case "Vic":
                    newCustomer.setState(Customer.States.Vic);
                    break;
                case "ACT":
                    newCustomer.setState(Customer.States.ACT);
                    break;
                case "Tas":
                    newCustomer.setState(Customer.States.Tas);
                    break;
                case "SA":
                    newCustomer.setState(Customer.States.SA);
                    break;
                case "WA":
                    newCustomer.setState(Customer.States.WA);
                    break;
                case "NT":
                    newCustomer.setState(Customer.States.NT);
                    break;
                case "Other":
                    break;
                default:
                    // this shouldn't happen
                    throw new Exception("Invalid customer data");
            }

            // prepare and execute the data access object  
            CustomerDAO dao = new CustomerDAO();
            dao.addCustomer(newCustomer);

            // fire the event to re-fetch data
            getAllCustomers();
        }

        public void updateCustomer(Customer customer)
        {
            // strategy: find the customer record in the database with this ID. Update its remaining fields with these values.
            // DAO
            CustomerDAO dao = new CustomerDAO();
            dao.updateCustomer(customer);

            // fire the event
            getAllCustomers();
        }

        public void importUpdateCustomer(Customer customer)
        {
            // DAO
            CustomerDAO dao = new CustomerDAO();
            dao.importUpdateCustomer(customer);
        }

        public void deleteCustomer(int id)
        {
            // object
            Customer customer = new Customer();
            customer.setID(id);

            // DAO
            CustomerDAO dao = new CustomerDAO();
            dao.deleteCustomer(customer);

            // fire the event
            getAllCustomers();
        }

        public Customer getCustomer(int id)
        {
            // DAO
            CustomerDAO dao = new CustomerDAO();
            return dao.getCustomer(id);
        }

        public List<Customer> getAllCustomers()
        {
            // DAO
            CustomerDAO dao = new CustomerDAO();
            List<Customer> allCustomers = (List<Customer>)(dao.getAllCustomers());

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

    /// <summary>
    /// Event arguments class.
    /// </summary>
    public class GetAllCustomersEventArgs : EventArgs
    {
        private List<Customer> customerList;

        // ctor
        public GetAllCustomersEventArgs(List<Customer> customerList)
        {
            this.customerList = customerList;
        }

        public List<Customer> getList()
        {
            return this.customerList;
        }
    }
    //GetAllCustomersEventArgs
}
