using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linq.Classes
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CompanyName { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public List<Order>? Orders { get; set; }
    }
}