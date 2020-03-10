using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using POS;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Model.DataAccessLayer
{
    public class ProductDAO : IProductDAO
    {
        public IList<Product> getAllProducts()
        {
            IList<Product> products = new List<Product>();

            string queryGetAllProducts = "SELECT * FROM Products;";

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
                    SqlCommand cmd = new SqlCommand(queryGetAllProducts, conn);

                    // try a connection
                    conn.Open();

                    // execute the query
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.setProductID(reader.GetInt32(0));
                        product.setProductIDNumber(reader.GetString(1));
                        product.setDescription(reader.GetString(2));
                        product.setQuantity(reader.GetInt32(3));
                        // a SQL float is a .NET double
                        double dprice = reader.GetDouble(4);
                        float fprice = Convert.ToSingle(dprice);
                        product.setPrice(fprice);

                        products.Add(product);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return products;
        }

        public Product getProduct(string idNumber)
        {
            Product product = new Product();

            string queryGetProduct = "SELECT * FROM Products " +
                                     "WHERE ProductIDNumber = @id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
                    SqlCommand cmd = new SqlCommand(queryGetProduct, conn);

                    // parameterise
                    SqlParameter idParam = new SqlParameter();
                    idParam.ParameterName = "@id";
                    idParam.Value = idNumber;
                    cmd.Parameters.Add(idParam);

                    // attempt a connection
                    conn.Open();

                    // execute the query
                    SqlDataReader reader = cmd.ExecuteReader();

                    // check if results exist
                    if (reader.HasRows)
                    {
                        // results exist
                        while (reader.Read())
                        {

                            product.setProductID(reader.GetInt32(0));
                            product.setProductIDNumber(reader.GetString(1));
                            product.setDescription(reader.GetString(2));
                            product.setQuantity(reader.GetInt32(3));
                            // an SQL float is a .NET double
                            double dprice = reader.GetDouble(4);
                            float fprice = Convert.ToSingle(dprice);
                            product.setPrice(fprice);
                        }
                    }
                    else
                    {
                        return null;
                    }

                    return product;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        // this works
        public int deleteProduct(Product product)
        {
            string queryDeleteProduct = "DELETE FROM Products " +
                                        "WHERE ProductID" +
                                        "" +
                                        " = @id;";
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
                    SqlCommand cmd = new SqlCommand(queryDeleteProduct, conn);

                    // parameterise
                    SqlParameter idParam = new SqlParameter();
                    idParam.ParameterName = "@id";
                    idParam.Value = product.getProductID();
                    cmd.Parameters.Add(idParam);

                    // attempt a connection
                    conn.Open();

                    // execute the query
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        // this works
        public int addProduct(Product product)
        {
            string queryAddProduct = "INSERT INTO Products (ProductIDNumber,Description_,Quantity,Price) " +
                                     "VALUES (@idNumber, @description, @quantity, @price);";
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
                    SqlCommand cmd = new SqlCommand(queryAddProduct, conn);

                    // parameterise
                    SqlParameter idNumberParam = new SqlParameter();
                    idNumberParam.ParameterName = "@idNumber";
                    idNumberParam.Value = product.getProductIDNumber();
                    cmd.Parameters.Add(idNumberParam);

                    SqlParameter descParam = new SqlParameter();
                    descParam.ParameterName = "@description";
                    descParam.Value = product.getDescription();
                    cmd.Parameters.Add(descParam);

                    SqlParameter quantityParam = new SqlParameter();
                    quantityParam.ParameterName = "@quantity";
                    quantityParam.Value = product.getQuantity();
                    cmd.Parameters.Add(quantityParam);

                    SqlParameter priceParam = new SqlParameter();
                    priceParam.ParameterName = "@price";
                    priceParam.Value = product.getPrice();
                    cmd.Parameters.Add(priceParam);

                    // attempt a connection
                    conn.Open();

                    // execute the query
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        public void decrementQuantity(string productID)
        {
            // may not be needed
        }

        public void setQuantity(string productID, int quantity)
        {
            // possibly useful
        }

        //public void updateProduct(int id, string productIdNumber, string description, int quantity, float price)
        public void updateProduct(Product product)
        {
            string queryUpdateProduct = "UPDATE Products " +
                                        "SET ProductIDNumber = @idNumber, Description_ = @desc, Quantity = @quantity, Price = @price " +
                                        "WHERE ProductID = " + product.getProductID() + ";";

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
                    SqlCommand cmd = new SqlCommand(queryUpdateProduct, conn);

                    // paramterise
                    SqlParameter idNumParam = new SqlParameter();
                    idNumParam.ParameterName = "@idNumber";
                    idNumParam.Value = product.getProductIDNumber();
                    cmd.Parameters.Add(idNumParam);

                    SqlParameter descParam = new SqlParameter();
                    descParam.ParameterName = "@desc";
                    descParam.Value = product.getDescription();
                    cmd.Parameters.Add(descParam);

                    SqlParameter quantParam = new SqlParameter();
                    quantParam.ParameterName = "@quantity";
                    quantParam.Value = product.getQuantity();
                    cmd.Parameters.Add(quantParam);

                    SqlParameter priceParam = new SqlParameter();
                    priceParam.ParameterName = "@price";
                    priceParam.Value = product.getPrice();
                    cmd.Parameters.Add(priceParam);

                    // attempt a connection
                    conn.Open();

                    // execute the query
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
