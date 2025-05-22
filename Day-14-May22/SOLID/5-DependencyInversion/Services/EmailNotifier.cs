


using _5_DependencyInversion.Interfaces;
using _5_DependencyInversion.Models;

namespace _5_DependencyInversion.Services
{
    public class EmailNotifier : INotifier
    {
        public void SendNotification(Order order)
        {
            //Send Email Notification
            Console.WriteLine($"Email Notification Sent for order: {order.OrderDetails}");
        }

    }
}