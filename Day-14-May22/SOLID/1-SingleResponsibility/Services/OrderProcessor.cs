

using _1_SingleResponsibility.Models;
using _1_SingleResponsibility.Repository;

namespace _1_SingleResponsibility.Services
{
    public class OrderProcessor
    {
        private OrderRepository repo = new OrderRepository();                 
        private Notifier notifier = new Notifier();


        public void Process(Order order)
        {
            repo.Save(order);

            notifier.SendNotification(order);
        }


    }
}