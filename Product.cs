using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
    class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int Cost { get; set; }
        public int Supply { get; set; }

        public Product(int productID, string productName, int supplierID, int cost, int supply)
        {
            ProductID = productID;
            ProductName = productName;
            SupplierID = supplierID;
            Cost = cost;
            Supply = supply;
        }

        public Product(int productID, string productName, int cost, int supply)
        {
            ProductID = productID;
            ProductName = productName;            
            Cost = cost;
            Supply = supply;
        }

        public Product(string productName, int cost, int supply) : this(productName, cost)
        {
            Supply = supply;
        }

        public Product(string productName,int supply)
        {            
            ProductName = productName;           
            Supply = supply;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.ProductID;
        }

        public override string ToString()
        {
            return $"Product: {this.ProductID} {this.ProductName} {this.SupplierID} {this.Cost} {this.Supply}";
        }
    }
}
