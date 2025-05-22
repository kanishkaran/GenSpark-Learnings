
using _2_OpenClose.Interfaces;
using _2_OpenClose.Models;

namespace _2_OpenClose.Services
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