using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linq.Classes
{
    public class Product
    {
        public string? ProductName { get; set; }
        public int UnitsInStock { get; set; }

        public decimal UnitPrice { get; set; }

        public override string ToString()
        {
            return $"\n[ProductName: {ProductName}, Units in Stock: {UnitsInStock}, Unit Price: {UnitPrice}]";
        }
    }
}