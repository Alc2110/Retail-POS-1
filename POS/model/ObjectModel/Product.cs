using System;

namespace Model.ObjectModel
{
    public class Product
    {
        // default ctor
        public Product()
        {
        }

        // ctor
        public Product(long ProductID, string ProductIDNumber, string Description, int Quantity, float price)
        {
            this.ProductID = ProductID;
            this.ProductIDNumber = ProductIDNumber;
            this.Description = Description;
            this.Quantity = Quantity;
            this.price = price;
        }

        private long ProductID;
        private string ProductIDNumber;
        private string Description;
        private int Quantity;
        private float price;

        public void setProductID(long id)
        {
            this.ProductID = id;
        }

        public long getProductID()
        {
            return ProductID;
        }

        public void setProductIDNumber(string id)
        {
            this.ProductIDNumber = id;
        }

        public string getProductIDNumber()
        {
            return this.ProductIDNumber;
        }

        public void setQuantity(int quantity)
        {
            this.Quantity = quantity;
        }

        public int getQuantity()
        {
            return this.Quantity;
        }

        public void setPrice(float price)
        {
            this.price = price;
        }

        public float getPrice()
        {
            return price;
        }

        public void setDescription(string desc)
        {
            this.Description = desc;
        }

        public string getDescription()
        {
            return this.Description;
        }
    }
}
