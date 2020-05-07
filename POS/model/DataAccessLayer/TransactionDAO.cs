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
    // TODO: this is shit - refactor it properly
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
        /// Return all transactions in the database.
        /// </summary>
        /// <returns>List of Transaction objects</returns>
        //public List<Transaction> getAllTransactions()
        public IEnumerable<ITransaction> getAllTransactions()
        {
            Task<List<Transaction>> task = Task.Run<List<Transaction>>(async () => await retrieveAllTransactions());

            return task.Result;
        }

        /// <summary>
        /// Retrieve all transactions in the database.
        /// </summary>
        /// <returns>Task List of Transactions</returns>
        private async Task<List<Transaction>> retrieveAllTransactions()
        {
            List<Transaction> transList = new List<Transaction>();

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

                try
                {
                    // try a connection
                    await conn.OpenAsync();
                }
                catch (Exception ae)
                {
                    var caught = ae.InnerException;
                    if (caught is SqlException)
                        throw caught;
                    return null;
                }

                // execute the query
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

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

                    transList.Add(transaction);
                }
            }

            return transList;
        }

        // actually, may not want to delete transactions...
        public void deleteTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a transaction record to the database.
        /// </summary>
        /// <param name="items">Items</param>
        public async void addTransaction(ValueTuple<int, int, Dictionary<string, int>> items)
        {
            // extract staff and customer information
            int staffID = items.Item1;
            int customerID = items.Item2;

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                try
                {
                    // try a connection
                    await conn.OpenAsync();
                }
                catch (Exception ae)
                {
                    var caught = ae.InnerException;
                    if (caught is SqlException)
                        throw caught;
                    return;
                }

                // create command object
                SqlCommand cmd = conn.CreateCommand();

                // parameterise
                SqlParameter customerIDparam = new SqlParameter();
                customerIDparam.ParameterName = "@customerID";
                cmd.Parameters.Add(customerIDparam);

                SqlParameter productIDparam = new SqlParameter();
                productIDparam.ParameterName = "@productID";
                cmd.Parameters.Add(productIDparam);

                SqlParameter staffIDparam = new SqlParameter();
                staffIDparam.ParameterName = "@staffID";
                cmd.Parameters.Add(staffIDparam);

                SqlParameter quantityParam = new SqlParameter();
                quantityParam.ParameterName = "@quantity";
                cmd.Parameters.Add(quantityParam);

                foreach (KeyValuePair<string, int> keyVal in items.Item3)
                {
                    // extract product information
                    string productIDnumber = keyVal.Key;
                    int productQuantity = keyVal.Value;
                    ProductDAO productDAO = new ProductDAO();
                    Product currProduct = (Product)productDAO.getProduct(productIDnumber);
                    //long productID = currProduct.getProductID();
                    //int productInStock = currProduct.getQuantity();
                    long productID = currProduct.ProductID;
                    int productInStock = currProduct.Quantity;

                    // assign values to parameters
                    customerIDparam.Value = customerID;
                    productIDparam.Value = productID;
                    staffIDparam.Value = staffID;
                    quantityParam.Value = productInStock - productQuantity;

                    for (int i = 1; i <= productQuantity; i++)
                    {
                        // start transaction
                        SqlTransaction transaction = conn.BeginTransaction("Transaction");

                        try
                        {
                            // must assign both transaction object and connection
                            // to command object for a pending local transaction
                            cmd.Connection = conn;
                            cmd.Transaction = transaction;

                            // update Transactions table
                            if (customerID != 0)
                            {
                                /*
                                cmd.Parameters.AddWithValue("@customerID", customerID);
                                cmd.Parameters.AddWithValue("@productID", productID);
                                cmd.Parameters.AddWithValue("@staffID", staffID);
                                cmd.Parameters.AddWithValue("@quantity", (productInStock - productQuantity));
                                */
                                cmd.CommandText = "INSERT INTO Transactions (Timestamp_, CustomerID, StaffID, ProductID)" +
                                                  "VALUES (SYSDATETIME(),@customerID,@staffID,@productID);";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                /*
                                cmd.Parameters.AddWithValue("@productID", productID);
                                cmd.Parameters.AddWithValue("@staffID", staffID);
                                cmd.Parameters.AddWithValue("@quantity", (productInStock - productQuantity));
                                 */
                                cmd.CommandText = "INSERT INTO Transactions (Timestamp_, StaffID, ProductID)" +
                                                  "VALUES (SYSDATETIME(),@staffID,@productID);";
                                cmd.ExecuteNonQuery();
                            }

                            // update Products table (inventory)
                            cmd.CommandText = "UPDATE Products SET Quantity = @quantity WHERE ProductID = " + productID + ";";
                            cmd.ExecuteNonQuery();

                            // commit transaction
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // rollback transaction and pass the error to the top level
                            transaction.Rollback();

                            throw;
                        }
                    }
                }
            }
        }

        // TODO: use this method instead of the other one, but this does not work 
        public void addTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
            /*
            // one-one relationship between transaction and product
            // transaction may or may not have a customer (member) associated with it.
            string createTransactionQuery = null;
            
            if (transaction.getCustomer()==null)
            {
                // non-member transaction
                createTransactionQuery = "BEGIN TRANSACTION Trans " +
                                            "  BEGIN TRY " +
                                            " " +
                                            "      --New record in Transactions table " +
                                            "      INSERT INTO Transactions (Timestamp_, CustomerID, StaffID, ProductID) " +
                                            "      VALUES (SYSDATETIME(),@varCustomerID,@varStaffID,@varProductID); " +
                                            " " +
                                            "      --Update inventory (Products table)" +
                                            "      --First retrieve the current quantity of this product" +
                                            "      DECLARE @varCurrQuantity int; " +
                                            "      SET @varCurrQuantity = (SELECT Quantity FROM Products WHERE ProductID=@varProductID); " +
                                            "      --and update it" +
                                            "      UPDATE Products " +
                                            "      SET Quantity = @varQuantity-1 " +
                                            "      WHERE ProductID = @varProductID; " +
                                            "      COMMIT TRANSACTION Trans " +  
                                            "  END TRY " +
                                            "  BEGIN CATCH " +
                                            "      ROLLBACK TRANSACTION Trans " +
                                            "       THROW; " +
                                            " END CATCH";
            }
            else
            {
                // member transaction
            }
    
            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
                    // define the command object
                    SqlCommand cmd = new SqlCommand(createTransactionQuery, conn);

                    // parameterise
                    SqlParameter customerParam = new SqlParameter();
                    customerParam.ParameterName = "@varCustomerID";
                    if (transaction.getCustomer()!=null)
                    {
                        customerParam.Value = transaction.getCustomer().getID();
                    }
                    else
                    {
                        customerParam.Value = null;
                    }
                    cmd.Parameters.Add(customerParam);

                    SqlParameter staffParam = new SqlParameter();
                    staffParam.ParameterName = "@varStaffID";
                    staffParam.Value = transaction.getStaff().getID();
                    cmd.Parameters.Add(staffParam);

                    SqlParameter productParam = new SqlParameter();
                    productParam.ParameterName = "@varProductID";
                    productParam.Value = transaction.getProduct().getProductID();
                    cmd.Parameters.Add(productParam);

                    // attempt a connection
                    conn.Open();

                    // execute it
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // pass it to the top level.
                throw;
            
            }
            */
        }
        
    }
}
