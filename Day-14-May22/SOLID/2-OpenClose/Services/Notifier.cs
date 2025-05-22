

using _2_OpenClose.Models;

namespace _2_OpenClose.Services
{
    public class Notifier           // also  for  Open for Extension , Closed for Modification - Bad Design
    {
        public void SendNotification(Order order)
        {
            //Send Notification
            Console.WriteLine($"Normal Notification Sent for order: {order.OrderDetails}");
        }
    }
}