


using _5_DependencyInversion.Interfaces;
using _5_DependencyInversion.Models;

namespace _5_DependencyInversion.Services
{
    public class SmsNotifier : INotifier
    {
        public void SendNotification(Order order)
        {
            //Send Sms Notification
            Console.WriteLine($"Sms Notification Sent for order: {order.OrderDetails}");
        }
    }
}