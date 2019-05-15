using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
    class Customer
    {
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public int Pass { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string CreditCard { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.CustomerID;
        }

        public override string ToString()
        {
            return $"Custumer: {this.CustomerID} {this.FirstName} {this.LastName}";
        }
    }
}
