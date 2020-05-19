using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using POS;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Model.DataAccessLayer.SqlServerInterface
{
    // this class might be faked for testing other classes, but won't be tested itself
    public class ProductDAO : IProductDAO
    {
        // connection string
        public string connString { get; set; }

        // default constructor
        // loads the connection string
        public ProductDAO()
        {
            this.connString = Configuration.CONNECTION_STRING;
        }

        // constructor with parameter
        public ProductDAO(string connString)
        {
            this.connString = connString;
        }

        /// <summary>
        /// Retrieve a list of all products in the database.
        /// </summary>
        /// <returns>Collection of product objects</returns>
        public IEnumerable<IProduct> getAllProducts()
        {
            string queryGetAllProducts = "SELECT * FROM Products;";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                SqlCommand cmd = new SqlCommand(queryGetAllProducts, conn);

                // try a connection
                conn.Open();

                // execute the query
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IProduct product = new Product();
                        product.ProductID = reader.GetInt32(0);
                        product.ProductIDNumber = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.Quantity = reader.GetInt32(3);
                        // a SQL float is a .NET double
                        double dprice = reader.GetDouble(4);
                        float fprice = Convert.ToSingle(dprice);
                        product.price = fprice;

                        yield return product;
                    }
                }
                else
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// Retrieve a product from the database, based on its barcode number.
        /// </summary>
        /// <param name="idNumber">barcode number string</param>
        /// <returns>Product interface</returns>
        public IProduct getProduct(string idNumber)
        {
            IProduct product = new Product();

            string queryGetProduct = "SELECT * FROM Products " +
                                     "WHERE ProductIDNumber = @id;";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                SqlCommand cmd = new SqlCommand(queryGetProduct, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = idNumber;
                cmd.Parameters.Add(idParam);

                SqlDataReader reader;

                // attempt a connection
                conn.Open();

                // execute the query
                reader = cmd.ExecuteReader();

                // check if results exist
                if (reader.HasRows)
                {
                    // results exist
                    while (reader.Read())
                    {
                        product.ProductID = reader.GetInt32(0);
                        product.ProductIDNumber = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.Quantity = reader.GetInt32(3);
                        // an SQL float is a .NET double
                        double dprice = reader.GetDouble(4);
                        float fprice = Convert.ToSingle(dprice);
                        product.price = fprice;
                    }
                }
                else
                {
                    product = null;
                }

                return product;
            }
        }

        // this works
        /// <summary>
        /// Delete a product from the database.
        /// </summary>
        /// <param name="product">Product object</param>
        public void deleteProduct(string idNumber)
        {
            string queryDeleteProduct = "DELETE FROM Products " +
                                        "WHERE ProductIDNumber" +
                                        "" +
                                        " = @idNumber;";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                SqlCommand cmd = new SqlCommand(queryDeleteProduct, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@idNumber";
                idParam.Value = idNumber;
                cmd.Parameters.Add(idParam);

                // attempt a connection
                conn.Open();

                // execute the query
                cmd.ExecuteNonQuery();
            }

            return;
        }

        // this works
        /// <summary>
        /// Add a product to the database.
        /// </summary>
        /// <param name="product">Product object</param>
        public void addProduct(IProduct product)
        {
            string queryAddProduct = "INSERT INTO Products (ProductIDNumber,Description_,Quantity,Price) " +
                                     "VALUES (@idNumber, @description, @quantity, @price);";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                SqlCommand cmd = new SqlCommand(queryAddProduct, conn);

                // parameterise
                SqlParameter idNumberParam = new SqlParameter();
                idNumberParam.ParameterName = "@idNumber";
                idNumberParam.Value = product.ProductID;
                cmd.Parameters.Add(idNumberParam);

                SqlParameter descParam = new SqlParameter();
                descParam.ParameterName = "@description";
                descParam.Value = product.Description;
                cmd.Parameters.Add(descParam);

                SqlParameter quantityParam = new SqlParameter();
                quantityParam.ParameterName = "@quantity";
                quantityParam.Value = product.Quantity;
                cmd.Parameters.Add(quantityParam);

                SqlParameter priceParam = new SqlParameter();
                priceParam.ParameterName = "@price";
                priceParam.Value = product.price;
                cmd.Parameters.Add(priceParam);

                // attempt a connection
                conn.Open();

                // execute the query
                cmd.ExecuteNonQuery();
            }

            return;
        }

        /// <summary>
        /// Update a product record in the database.
        /// </summary>
        /// <param name="product">Product object</param>
        public void updateProduct(IProduct product)
        {
            string queryUpdateProduct = "UPDATE Products " +
                                        "SET ProductIDNumber = @idNumber, Description_ = @desc, Quantity = @quantity, Price = @price " +
                                        "WHERE ProductID = " + product.ProductID + ";";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                SqlCommand cmd = new SqlCommand(queryUpdateProduct, conn);

                // paramterise
                SqlParameter idNumParam = new SqlParameter();
                idNumParam.ParameterName = "@idNumber";
                idNumParam.Value = product.ProductIDNumber;
                cmd.Parameters.Add(idNumParam);

                SqlParameter descParam = new SqlParameter();
                descParam.ParameterName = "@desc";
                descParam.Value = product.Description;
                cmd.Parameters.Add(descParam);

                SqlParameter quantParam = new SqlParameter();
                quantParam.ParameterName = "@quantity";
                quantParam.Value = product.Quantity;
                cmd.Parameters.Add(quantParam);

                SqlParameter priceParam = new SqlParameter();
                priceParam.ParameterName = "@price";
                priceParam.Value = product.price;
                cmd.Parameters.Add(priceParam);

                // attempt a connection
                conn.Open();

                // execute the query
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Performs the import-update operation.
        /// </summary>
        /// <param name="product">Product interface</param>
        public void importUpdateProduct(IProduct product)
        {
            if (getProduct(product.ProductIDNumber) == null)
            {
                addProduct(product);
            }
            else
            {
                updateProduct(product);
            }
        }
    }
}