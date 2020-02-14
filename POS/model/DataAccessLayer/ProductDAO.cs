using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using POS;
using System.Data.SqlClient;

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
                using (SqlConnection conn = new SqlConnection(Configuration.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(queryGetAllProducts, conn);

                    // try a connection
                    conn.Open();

                    // execute the query
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.setProductID(reader.GetInt64(1));
                        product.setDescription(reader.GetString(2));
                        product.setQuantity(reader.GetInt32(3));
                        product.setPrice(reader.GetFloat(4));

                        products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }

            return products;
        }

        public Product getProduct(long id)
        {
            Product product = new Product();

            string queryGetProduct = "SELECT * FROM Products " +
                                     "WHERE ProductIDNumber = @id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(queryGetProduct, conn);

                    // parameterise
                    SqlParameter idParam = new SqlParameter();
                    idParam.ParameterName = "@id";
                    idParam.Value = id;
                    cmd.Parameters.Add(idParam);

                    // attempt a connection
                    conn.Open();

                    // execute the query
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        long productID = reader.GetInt64(1);
                        string description = reader.GetString(2);
                        int quantity = reader.GetInt32(3);
                        float price = reader.GetFloat(4);

                        product = new Product(productID, description, quantity, price);
                    }

                    return product;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
        }

        // this works
        public int deleteProduct(Product product)
        {
            string queryDeleteProduct = "DELETE FROM Products " +
                                        "WHERE ProductIDNumber = @id;";
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.connectionString))
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
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }

            return result;
        }

        // this works
        public int addProduct(Product product)
        {
            string queryAddProduct = "INSERT INTO Products (ProductIDNumber,Description_,Quantity,Price) " +
                                     "VALUES (@id, @description, @quantity, @price);";
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(queryAddProduct, conn);

                    // parameterise
                    SqlParameter idParam = new SqlParameter();
                    idParam.ParameterName = "@id";
                    idParam.Value = product.getProductID();
                    cmd.Parameters.Add(idParam);

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

        public int updateProduct(Product newProduct, Product oldProduct)
        {
            return 0;
        }
    }
}
