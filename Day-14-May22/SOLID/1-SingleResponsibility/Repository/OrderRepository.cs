


using _1_SingleResponsibility.Models;

namespace _1_SingleResponsibility.Repository
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
