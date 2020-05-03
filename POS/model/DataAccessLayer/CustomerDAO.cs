using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using System.Data.Sql;
using System.Data.SqlClient;
using POS;
using System.Diagnostics;

namespace Model.DataAccessLayer
{
    public class CustomerDAO : ICustomerDAO
    { 
        /// <summary>
        /// Retrieve a customer record from the database.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>Customer object</returns>
        public Customer getCustomer(int id)
        {
            Customer customer = new Customer();

            string queryGetCustomer = "SELECT * From Customers " +
                                      "WHERE CustomerID = @id;";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                // define the command object
                SqlCommand cmd = new SqlCommand(queryGetCustomer, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = id;
                cmd.Parameters.Add(idParam);

                // try a connection
                conn.Open();

                // execute the query
                SqlDataReader reader = cmd.ExecuteReader();
 
                // check if results exist
                if (reader.HasRows)
                {
                    // results exist
                    while (reader.Read())
                    {
                        customer.setID(reader.GetInt32(0));
                        customer.setName(reader.GetString(1));
                        customer.setAddress(reader.GetString(2));
                        customer.setPhoneNumber(reader.GetString(3));
                        customer.setEmail(reader.GetString(4));
                        customer.setCity(reader.GetString(5));

                        switch (reader.GetString(6))
                        {
                            case "NSW":
                                customer.setState(Customer.States.NSW);
                                break;
                            case "Qld":
                                customer.setState(Customer.States.Qld);
                                break;
                            case "Tas":
                                customer.setState(Customer.States.Tas);
                                break;
                            case "ACT":
                                customer.setState(Customer.States.ACT);
                                break;
                            case "Vic":
                                customer.setState(Customer.States.Vic);
                                break;
                            case "SA":
                                customer.setState(Customer.States.SA);
                                break;
                            case "WA":
                                customer.setState(Customer.States.WA);
                                break;
                            case "NT":
                                customer.setState(Customer.States.NT);
                                break;
                            case "Other":
                                customer.setState(Customer.States.Other);
                                break;
                            default:
                                // this shouldn't happen
                                // TODO: handle this properly
                                throw new Exception("Invalid data in database");
                        }

                        customer.setPostcode(reader.GetInt32(7));
                    }
                }
                else
                {
                    customer = null;
                }

                return customer;
            }
        }

        // this works
        /// <summary>
        /// Retrieves a list of all customer records in the database.
        /// </summary>
        /// <returns>Task List of customer objects.</returns>
        public List<Customer> getAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            string queryGetAllCustomers = "SELECT * From Customers;";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(queryGetAllCustomers, conn);

   
                    // try a connection
                    conn.Open();


                // execute the query
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.setID(reader.GetInt32(0));
                    customer.setName(reader.GetString(1));
                    customer.setAddress(reader.GetString(2));
                    customer.setPhoneNumber(reader.GetString(3));
                    customer.setEmail(reader.GetString(4));
                    customer.setCity(reader.GetString(5));

                    switch (reader.GetString(6))
                    {
                        case "NSW":
                            customer.setState(Customer.States.NSW);
                            break;
                        case "Qld":
                            customer.setState(Customer.States.Qld);
                            break;
                        case "Tas":
                            customer.setState(Customer.States.Tas);
                            break;
                        case "ACT":
                            customer.setState(Customer.States.ACT);
                            break;
                        case "Vic":
                            customer.setState(Customer.States.Vic);
                            break;
                        case "SA":
                            customer.setState(Customer.States.SA);
                            break;
                        case "WA":
                            customer.setState(Customer.States.WA);
                            break;
                        case "NT":
                            customer.setState(Customer.States.NT);
                            break;
                        case "Other":
                            customer.setState(Customer.States.Other);
                            break;
                        default:
                            // this shouldn't happen
                            throw new Exception("Invalid data in database");
                    }
                    customer.setPostcode(reader.GetInt32(7));

                    customers.Add(customer);
                }
            }

            return customers;
        }

        // TODO: change this to require an ID only
        /// <summary>
        /// Delete a customer record from the database.
        /// </summary>
        /// <param name="customer">Customer object</param>
        public void deleteCustomer(Customer customer)
        {
            // CustomerID in the database is the PK
            int id = customer.getID();
            // TODO: parameterise this!
            string queryDeleteCustomer = "DELETE FROM Customers WHERE CustomerID = " + id + ";";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(queryDeleteCustomer, conn);

             
                    // try a connection
                    conn.Open();

                    // execute the query
                    cmd.ExecuteNonQuery();
                
              
            }
            
            return;
        }

        /// <summary>
        /// Add a customer record to the database.
        /// </summary>
        /// <param name="customer">Customer object.</param>
        public void addCustomer(Customer customer)
        {
            // CustomerID in the database is PK and AI
            string queryAddCustomer = "INSERT INTO Customers (FullName, StreetAddress, PhoneNumber, Email, City, State_, Postcode) " +
                                      "VALUES (@name, @address, @number, @email, @city, @state, @postcode);";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(queryAddCustomer, conn);

                // parameterise
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = customer.getName();
                cmd.Parameters.Add(nameParam);

                SqlParameter addressParam = new SqlParameter();
                addressParam.ParameterName = "@address";
                addressParam.Value = customer.getAddress();
                cmd.Parameters.Add(addressParam);

                SqlParameter numberParam = new SqlParameter();
                numberParam.ParameterName = "@number";
                numberParam.Value = customer.getPhoneNumber();
                cmd.Parameters.Add(numberParam);

                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";
                emailParam.Value = customer.getEmail();
                cmd.Parameters.Add(emailParam);

                SqlParameter cityParam = new SqlParameter();
                cityParam.ParameterName = "@city";
                cityParam.Value = customer.getCity();
                cmd.Parameters.Add(cityParam);

                SqlParameter stateParam = new SqlParameter();
                stateParam.ParameterName = "@state";
                stateParam.Value = customer.getState().ToString();
                cmd.Parameters.Add(stateParam);

                SqlParameter postcodeParam = new SqlParameter();
                postcodeParam.ParameterName = "@postcode";
                postcodeParam.Value = customer.getPostcode();
                cmd.Parameters.Add(postcodeParam);

               
                    // try a connection
                    conn.Open();

                    // execute the query
                    cmd.ExecuteNonQuery();
               
            }
        }

        // this works
        /// <summary>
        /// Update a customer record in the database.
        /// </summary>
        /// <param name="customer"></param>
        public void updateCustomer(Customer customer)
        {
            // CustomerID in the database is PK and AI
            string queryUpdateCustomer = "UPDATE Customers " +
                                         "SET FullName = @name, StreetAddress = @address, PhoneNumber = @number, Email = @email, City = @city, State_ = @state, Postcode = @Postcode " +
                                         "WHERE CustomerID = @id;";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(queryUpdateCustomer, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = customer.getID();
                cmd.Parameters.Add(idParam);

                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = customer.getName();
                cmd.Parameters.Add(nameParam);

                SqlParameter addressParam = new SqlParameter();
                addressParam.ParameterName = "@address";
                addressParam.Value = customer.getAddress();
                cmd.Parameters.Add(addressParam);

                SqlParameter numberParam = new SqlParameter();
                numberParam.ParameterName = "@number";
                numberParam.Value = customer.getPhoneNumber();
                cmd.Parameters.Add(numberParam);

                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";
                emailParam.Value = customer.getEmail();
                cmd.Parameters.Add(emailParam);

                SqlParameter cityParam = new SqlParameter();
                cityParam.ParameterName = "@city";
                cityParam.Value = customer.getCity();
                cmd.Parameters.Add(cityParam);

                SqlParameter stateParam = new SqlParameter();
                stateParam.ParameterName = "@state";
                stateParam.Value = customer.getState().ToString();
                cmd.Parameters.Add(stateParam);

                SqlParameter postcodeParam = new SqlParameter();
                postcodeParam.ParameterName = "@postcode";
                postcodeParam.Value = customer.getPostcode();
                cmd.Parameters.Add(postcodeParam);

                 // try a connection
                    conn.Open();

                    // execute the query
                   cmd.ExecuteNonQuery();
               
            }
        }
    }
}
