

using _2_OpenClose.Interfaces;
using _2_OpenClose.Models;

namespace _2_OpenClose.Services
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