using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
    class Orders
    {
        public int OrderID { get; set; }
        public int NumberOfOreders { get; set; }
        public int TotalCost { get; set; }
        public string ProductName { get; set; }

        public Orders(int orderID, int numberOfOreders, int totalCost, string productName)
        {
            OrderID = orderID;
            NumberOfOreders = numberOfOreders;
            TotalCost = totalCost;
            ProductName = productName;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.OrderID;
        }

        public override string ToString()
        {
            return $"Custumer: Order ID: {this.OrderID} Product Name: {this.ProductName}, Number Of Oreders: {this.NumberOfOreders}, Total Cost: {this.TotalCost}";
        }
    }
}
