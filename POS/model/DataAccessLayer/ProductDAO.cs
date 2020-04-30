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
        /// <summary>
        /// Return a list of all products in the database.
        /// </summary>
        /// <returns>List of products</returns>
        public List<Product> getAllProducts()
        {
            Task<List<Product>> task = Task.Run<List<Product>>(async () => await retrieveAllProducts());

            return task.Result;
        }

        /// <summary>
        /// Retrieve a list of all products in the database.
        /// </summary>
        /// <returns>Task<List> of Product objects</returns>
        private async Task<List<Product>> retrieveAllProducts()
        {
            List<Product> products = new List<Product>();

            string queryGetAllProducts = "SELECT * FROM Products;";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(queryGetAllProducts, conn);

                // try a connection
                await conn.OpenAsync();

                // execute the query
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
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
  
            return products;
        }

        /// <summary>
        /// Return a product object from the database, based on its barcode number.
        /// </summary>
        /// <returns>Product object</returns>
        public Product getProduct(string idNumber)
        {
            Task<Product> task = Task.Run<Product>(async () => await retrieveProduct(idNumber));

            return task.Result;
        }

        /// <summary>
        /// Retrieve a product from the database, based on its barcode number.
        /// </summary>
        /// <param name="idNumber">barcode number string</param>
        /// <returns>Task<Product></returns>
        private async Task<Product> retrieveProduct(string idNumber)
        {
            Product product = new Product();

            string queryGetProduct = "SELECT * FROM Products " +
                                     "WHERE ProductIDNumber = @id;";


            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(queryGetProduct, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = idNumber;
                cmd.Parameters.Add(idParam);

                // attempt a connection
                await conn.OpenAsync();

                // execute the query
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

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
        public async void deleteProduct(Product product)
        {
            string queryDeleteProduct = "DELETE FROM Products " +
                                        "WHERE ProductID" +
                                        "" +
                                        " = @id;";
      
            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(queryDeleteProduct, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = product.getProductID();
                cmd.Parameters.Add(idParam);

                // attempt a connection
                await conn.OpenAsync();

                // execute the query
                await cmd.ExecuteNonQueryAsync(); 
            }
         
            return;
        }

        // this works
        /// <summary>
        /// Add a product to the database.
        /// </summary>
        /// <param name="product">Product object</param>
        public async void addProduct(Product product)
        {
            string queryAddProduct = "INSERT INTO Products (ProductIDNumber,Description_,Quantity,Price) " +
                                     "VALUES (@idNumber, @description, @quantity, @price);";

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
                await conn.OpenAsync();

                // execute the query
                await cmd.ExecuteNonQueryAsync();
            }

            return;
        }

        /// <summary>
        /// Update a product record in the database.
        /// </summary>
        /// <param name="product">Product object</param>
        public async void updateProduct(Product product)
        {
            string queryUpdateProduct = "UPDATE Products " +
                                        "SET ProductIDNumber = @idNumber, Description_ = @desc, Quantity = @quantity, Price = @price " +
                                        "WHERE ProductID = " + product.getProductID() + ";";

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
                await conn.OpenAsync();

                // execute the query
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
