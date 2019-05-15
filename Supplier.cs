using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
    class Supplier
    {
        public int SupplierID { get; set; }
        public string UserName { get; set; }
        public int Pass { get; set; }
        public string Compeny { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.SupplierID;
        }

        public override string ToString()
        {
            return $"Supplier: {SupplierID}, {Compeny}";
        }
    }
}
