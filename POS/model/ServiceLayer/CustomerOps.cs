using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataAccessLayer;
using Model.ObjectModel;

namespace Model.ServiceLayer
{
    public static class CustomerOps
    {
        public static void addCustomer(Customer customer)
        {
            // DAO
            CustomerDAO dao = new CustomerDAO();
            dao.addCustomer(customer);

            // fire the event
            getAllCustomers();
        }

        public static void addCustomer(string FullName, string streetAddress, string phoneNumber, string Email, string City, string state, int postcode)
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

            // DAO
            CustomerDAO dao = new CustomerDAO();
            dao.addCustomer(newCustomer);

            // fire the event
            getAllCustomers();
        }

        public static void updateCustomer(Customer customer)
        {
            // strategy: find the customer record in the database with this ID. Update its remaining fields with these values.
            // DAO
            CustomerDAO dao = new CustomerDAO();
            dao.updateCustomer(customer);

            // fire the event
            getAllCustomers();
        }

        public static void deleteCustomer(int id)
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

        public static Customer getCustomer(int id)
        {
            // DAO
            CustomerDAO dao = new CustomerDAO();
            return dao.getCustomer(id);
        }

        public static List<Customer> getAllCustomers()
        {
            // DAO
            CustomerDAO dao = new CustomerDAO();
            List<Customer> allCustomers = (List<Customer>)dao.getAllCustomers();

            // fire the event
            OnGetAllCustomers(null, new GetAllCustomersEventArgs(allCustomers));

            return allCustomers;
        }

        // event for getting all customers
        public static event EventHandler<GetAllCustomersEventArgs> OnGetAllCustomers = delegate { };     
    }

    public class GetAllCustomersEventArgs
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
}
