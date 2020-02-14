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
        // delegate and event for retrieving all customer data
        public delegate void showCustomers(List<Customer> customers);
        public static event showCustomers QueryAllCustomersEvent;

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
        }

        public static void deleteCustomer(int id)
        {
            // object
            Customer customer = new Customer();
            customer.setID(id);

            // DAO
            CustomerDAO dao = new CustomerDAO();
            dao.deleteCustomer(customer);
        }

        public static void getAllCustomers()
        {
            // DAO
            CustomerDAO dao = new CustomerDAO();
            List<Customer> allCustomers = dao.getAllCustomers().ToList<Customer>();

            // fire the event
            QueryAllCustomersEvent(allCustomers);
        }
    }
}
