using System;

namespace Model.ObjectModel
{
    public class Product : IProduct
    {
        // default constructor
        public Product()
        {
        }

        // constructor with parameters
        public Product(long ProductID, string ProductIDNumber, string Description, int Quantity, float price)
        {
            this.ProductID = ProductID;
            this.ProductIDNumber = ProductIDNumber;
            this.Description = Description;
            this.Quantity = Quantity;
            this.price = price;
        }

        public long ProductID { get; set; }
        public string ProductIDNumber { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float price { get; set; }
    }

    public interface IProduct
    {
        long ProductID { get; set; }
        string ProductIDNumber { get; set; }
        string Description { get; set; }
        int Quantity { get; set; }
        float price { get; set; }
    }
}
