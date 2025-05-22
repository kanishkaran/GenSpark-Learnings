

using _1_SingleResponsibility.Models;

namespace _1_SingleResponsibility.Services
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