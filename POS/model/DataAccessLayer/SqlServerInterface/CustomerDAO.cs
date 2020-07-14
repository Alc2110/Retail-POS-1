using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using System.Data.Sql;
using System.Data.SqlClient;
using POS;
using System.Diagnostics;

namespace Model.DataAccessLayer.SqlServerInterface
{
    // this class might be faked for testing other classes, but won't be tested itself
    public class CustomerDAO : ICustomerDAO
    {
        // connection string
        public string connString { get; set; }

        // default constructor
        // loads the connection string
        public CustomerDAO()
        {
            this.connString = Configuration.CONNECTION_STRING;
        }

        // constructor with parameter
        public CustomerDAO(string connString)
        {
            this.connString = connString;
        }

        /// <summary>
        /// Retrieve a customer record from the database.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>Customer object</returns>
        public ICustomer getCustomer(int id)
        {
            ICustomer customer = new Customer();

            string queryGetCustomer = "SELECT * From Customers " +
                                      "WHERE CustomerID = @id;";

            using (SqlConnection conn = new SqlConnection(this.connString))
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
                        customer.CustomerID = reader.GetInt32(0);
                        customer.FullName = reader.GetString(1);
                        customer.Address = reader.GetString(2);
                        customer.PhoneNumber = reader.GetString(3);
                        customer.Email = reader.GetString(4);
                        customer.City = reader.GetString(5);
                        States state;
                        if (Enum.TryParse(reader.GetString(6), out state))
                        {
                            customer.state = state;
                        }
                        else
                        {
                            // should never happen
                            throw new InvalidDataException("Invalid customer data");
                        }

                        customer.Postcode = reader.GetInt32(7);
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
        public IEnumerable<ICustomer> getAllCustomers()
        {
            string queryGetAllCustomers = "SELECT * From Customers;";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                SqlCommand cmd = new SqlCommand(queryGetAllCustomers, conn);

                // try a connection
                conn.Open();

                // execute the query
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ICustomer customer = new Customer();
                        customer.CustomerID = reader.GetInt32(0);
                        customer.FullName = reader.GetString(1);
                        customer.Address = reader.GetString(2);
                        customer.PhoneNumber = reader.GetString(3);
                        customer.Email = reader.GetString(4);
                        customer.City = reader.GetString(5);
                        States state;
                        if (Enum.TryParse(reader.GetString(6), out state))
                        {
                            customer.state = state;
                        }
                        else
                        {
                            // should never happen
                            throw new InvalidDataException("Invalid customer data");
                        }
                        customer.Postcode = reader.GetInt32(7);

                        yield return customer;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a customer record from the database.
        /// </summary>
        /// <param name="customer">Customer object</param>
        public void deleteCustomer(int id)
        {
            // CustomerID in the database is the PK
            // TODO: parameterise this!
            string queryDeleteCustomer = "DELETE FROM Customers WHERE CustomerID = " + id + ";";

            using (SqlConnection conn = new SqlConnection(this.connString))
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
        public void addCustomer(ICustomer customer)
        {
            // CustomerID in the database is PK and AI
            string queryAddCustomer = "INSERT INTO Customers (FullName, StreetAddress, PhoneNumber, Email, City, State_, Postcode) " +
                                      "VALUES (@name, @address, @number, @email, @city, @state, @postcode);";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                SqlCommand cmd = new SqlCommand(queryAddCustomer, conn);

                // parameterise
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = customer.FullName;
                cmd.Parameters.Add(nameParam);

                SqlParameter addressParam = new SqlParameter();
                addressParam.ParameterName = "@address";
                addressParam.Value = customer.Address;
                cmd.Parameters.Add(addressParam);

                SqlParameter numberParam = new SqlParameter();
                numberParam.ParameterName = "@number";
                numberParam.Value = customer.PhoneNumber;
                cmd.Parameters.Add(numberParam);

                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";
                emailParam.Value = customer.Email;
                cmd.Parameters.Add(emailParam);

                SqlParameter cityParam = new SqlParameter();
                cityParam.ParameterName = "@city";
                cityParam.Value = customer.City;
                cmd.Parameters.Add(cityParam);

                SqlParameter stateParam = new SqlParameter();
                stateParam.ParameterName = "@state";
                stateParam.Value = customer.state.ToString();
                cmd.Parameters.Add(stateParam);

                SqlParameter postcodeParam = new SqlParameter();
                postcodeParam.ParameterName = "@postcode";
                postcodeParam.Value = customer.Postcode;
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
        public void updateCustomer(ICustomer customer)
        {
            // CustomerID in the database is PK and AI
            string queryUpdateCustomer = "UPDATE Customers " +
                                         "SET FullName = @name, StreetAddress = @address, PhoneNumber = @number, Email = @email, City = @city, State_ = @state, Postcode = @Postcode " +
                                         "WHERE CustomerID = @id;";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                SqlCommand cmd = new SqlCommand(queryUpdateCustomer, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = customer.CustomerID;
                cmd.Parameters.Add(idParam);

                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = customer.FullName;
                cmd.Parameters.Add(nameParam);

                SqlParameter addressParam = new SqlParameter();
                addressParam.ParameterName = "@address";
                addressParam.Value = customer.Address;
                cmd.Parameters.Add(addressParam);

                SqlParameter numberParam = new SqlParameter();
                numberParam.ParameterName = "@number";
                numberParam.Value = customer.PhoneNumber;
                cmd.Parameters.Add(numberParam);

                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";
                emailParam.Value = customer.Email;
                cmd.Parameters.Add(emailParam);

                SqlParameter cityParam = new SqlParameter();
                cityParam.ParameterName = "@city";
                cityParam.Value = customer.City;
                cmd.Parameters.Add(cityParam);

                SqlParameter stateParam = new SqlParameter();
                stateParam.ParameterName = "@state";
                stateParam.Value = customer.state.ToString();
                cmd.Parameters.Add(stateParam);

                SqlParameter postcodeParam = new SqlParameter();
                postcodeParam.ParameterName = "@postcode";
                postcodeParam.Value = customer.Postcode.ToString();
                cmd.Parameters.Add(postcodeParam);

                // try a connection
                conn.Open();

                // execute the query
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Performs the import/update operation.
        /// </summary>
        /// <param name="customer">Customer interface</param>
        public void importUpdateCustomer(ICustomer customer)
        {
            if (getCustomer(customer.CustomerID) == null)
            {
                addCustomer(customer);
            }
            else
            {
                updateCustomer(customer);
            }
        }
    }
}
