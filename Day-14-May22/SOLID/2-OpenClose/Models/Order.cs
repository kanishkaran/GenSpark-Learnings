using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2_OpenClose.Models
{
    public class Order
    {
        public string OrderDetails { get; set; } = string.Empty;

        public Order(string orderDetails)
        {
            OrderDetails = orderDetails;
        }
    }
}