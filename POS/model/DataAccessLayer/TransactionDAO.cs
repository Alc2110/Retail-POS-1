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
    public class TransactionDAO : ITransactionDAO
    {
        public IList<Transaction> getAllTransactions()
        {
            IList<Transaction> list = new List<Transaction>();

            return list;
        }

        // actually, may not want to delete transactions...
        public void deleteTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public int addTransaction(Transaction transaction)
        {
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
                using (SqlConnection conn = new SqlConnection(Configuration.connectionString))
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
        }
    }
}
