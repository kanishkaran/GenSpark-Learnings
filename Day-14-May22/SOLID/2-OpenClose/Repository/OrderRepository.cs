


using _2_OpenClose.Models;

namespace _2_OpenClose.Repository
{
    public class OrderRepository
    {
        public void Save(Order order)
        {
            // Logic for saving it
            Console.WriteLine($"Order Saved {order.OrderDetails}");
        }
    }
}
