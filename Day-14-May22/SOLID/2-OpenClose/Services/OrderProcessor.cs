

using _2_OpenClose.Interfaces;
using _2_OpenClose.Models;
using _2_OpenClose.Repository;

namespace _2_OpenClose.Services
{
    public class OrderProcessor
    {
        private OrderRepository repo = new OrderRepository();                 

        public void Process(Order order)
        {
            repo.Save(order);
            List<INotifier> notifiers = new List<INotifier> // Open For Extension, Closed For Modification - Good Design
            {
                new EmailNotifier(),
                new SmsNotifier()
            };

            foreach (var n in notifiers)
            {
                n.SendNotification(order);
            }
        }


    }
}