using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using POS;

namespace Model.DataAccessLayer
{
    // this class might be faked for testing, but won't be tested itself
    public class TransactionDAO : ITransactionDAO
    {
        // connection string
        public string connString { get; set; }

        // default constructor
        // loads the connection string
        public TransactionDAO()
        {
            this.connString = Configuration.CONNECTION_STRING;
        }

        // constructor with parameter
        public TransactionDAO(string connString)
        {
            this.connString = connString;
        }

        /// <summary>
        /// Retrieve all transactions in the database.
        /// </summary>
        /// <returns>Task List of Transactions</returns>
        public IEnumerable<ITransaction> getAllTransactions()
        {
            string getAllTransactionsQuery = "SELECT Transactions.TransactionID, Transactions.Timestamp_," +
                                             "Customers.CustomerID, Customers.FullName, Customers.PhoneNumber, Customers.Email, Customers.StreetAddress, Customers.State_, Customers.Postcode," +
                                             "Staff.StaffID, Staff.FullName, Staff.PasswordHash, Staff.Privelege, " +
                                             "Products.ProductID, Products.ProductIDNumber, Products.Description_, Products.Price " +
                                             "FROM(((Transactions " +
                                              " FULL JOIN Customers ON Transactions.CustomerID = Customers.CustomerID)" +
                                              " INNER JOIN Products ON Transactions.ProductID = Products.ProductID)" +
                                              " INNER JOIN Staff ON Transactions.StaffID = Staff.StaffID);";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                // prepare the command
                SqlCommand cmd = new SqlCommand(getAllTransactionsQuery, conn);

                // try a connection
                conn.Open();

                // execute the query
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.TransactionID = reader.GetInt32(0);
                    transaction.Timestamp = reader.GetDateTime(1).ToString();

                    Product transactionProduct = new Product();
                    transactionProduct.ProductID = reader.GetInt32(13);
                    transactionProduct.ProductIDNumber = reader.GetString(14);
                    transactionProduct.Description = reader.GetString(15);

                    // an SQL float is a .NET double
                    double productPrice = reader.GetDouble(16);
                    //transactionProduct.setPrice(Convert.ToSingle(productPrice));
                    transactionProduct.price = Convert.ToSingle(productPrice);

                    Staff transactionStaff = new Staff();
                    transactionStaff.StaffID = reader.GetInt32(9);
                    transactionStaff.FullName = reader.GetString(10);
                    transactionStaff.PasswordHash = reader.GetString(11);
                    switch (reader.GetString(12))
                    {
                        case "Admin":
                            transactionStaff.privelege = Staff.Privelege.Admin;

                            break;

                        case "Normal":
                            transactionStaff.privelege = Staff.Privelege.Normal;

                            break;

                        default:
                            // this shouldn't happen
                            throw new Exception("Invalid data in database");
                    }

                    Customer transactionCustomer = new Customer();
                    // check if Customer exists for this transaction
                    if (!reader.IsDBNull(2))
                    {
                        transactionCustomer.CustomerID = reader.GetInt32(2);
                        transactionCustomer.FullName = reader.GetString(3);
                        transactionCustomer.PhoneNumber = reader.GetString(4);
                        transactionCustomer.Email = reader.GetString(5);
                        transactionCustomer.Address = reader.GetString(6);
                        switch (reader.GetString(7))
                        {
                            case "NSW":
                                transactionCustomer.state = Customer.States.NSW;
                                break;
                            case "ACT":
                                transactionCustomer.state = Customer.States.ACT;
                                break;
                            case "NT":
                                transactionCustomer.state = Customer.States.NT;
                                break;
                            case "Qld":
                                transactionCustomer.state = Customer.States.Qld;
                                break;
                            case "SA":
                                transactionCustomer.state = Customer.States.SA;
                                break;
                            case "Vic":
                                transactionCustomer.state = Customer.States.Vic;
                                break;
                            case "Tas":
                                transactionCustomer.state = Customer.States.Tas;
                                break;
                            case "WA":
                                transactionCustomer.state = Customer.States.WA;
                                break;
                            case "Other":
                                transactionCustomer.state = Customer.States.Other;
                                break;
                            default:
                                // this shouldn't happen, but handle it anyway
                                throw new Exception("Invalid data in database");
                        }
                    }
                    else
                    {
                        // no customer
                        transactionCustomer = null;
                    }

                    transaction.staff = transactionStaff;
                    transaction.product = transactionProduct;
                    transaction.customer = transactionCustomer;

                    yield return transaction;
                }
            }
        }

        /// <summary>
        /// Add a transaction to the database.
        /// </summary>
        /// <param name="transaction">Transaction interface.</param>
        public void addTransaction(ITransaction transaction)
        {
            // TODO: implement it as a transaction
            // TODO: figure out a way of putting the timestamp from the transaction object into the query
            // prepare the query
            string query = "INSERT INTO Transactions (Timestamp_, CustomerID, StaffID, ProductID)" +
                           "VALUES (SYSDATETIME(), @customerID, @staffID, @productID);" +
                           "\n" +
                           "DECLARE @Quantity int;" +
                           "SET @Quantity = ((SELECT Products.Quantity FROM Products WHERE Products.ProductID = @productID) - 1);" +
                           "UPDATE Products SET Products.Quantity = @Quantity WHERE Products.ProductID = @productID;";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                // prepare the command
                SqlCommand cmd = new SqlCommand(query, conn);

                // parameterise
                SqlParameter customerIDParam = new SqlParameter();
                if (transaction.customer != null)
                {
                    customerIDParam.Value = transaction.customer.CustomerID; // TODO: fix null reference exception when no customer
                }
                else
                {
                    customerIDParam.Value = DBNull.Value;
                }
                customerIDParam.ParameterName = "@customerID";
                cmd.Parameters.Add(customerIDParam);

                SqlParameter staffIDParam = new SqlParameter();
                staffIDParam.Value = transaction.staff.StaffID;
                staffIDParam.ParameterName = "@staffID";
                cmd.Parameters.Add(staffIDParam);

                SqlParameter productIDParam = new SqlParameter();
                productIDParam.Value = transaction.product.ProductID;
                productIDParam.ParameterName = "@productID";
                cmd.Parameters.Add(productIDParam);

                // try a connection
                conn.Open();

                // execute the query
                cmd.ExecuteNonQuery();
            }
        }
    }
}
